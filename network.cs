using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using svrSimu;
using System.Timers;
using Newtonsoft.Json;
using System.Threading;


namespace DispatchServer
{
    class network
    {
            static private int svr_port = 0;        //服务器端口号
            static private string svr_ip = "";      //服务器ip地址
            static private Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);   //tcp连接socket
            static private Socket acceptSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);   //
            static private UTF8Encoding u8 =new UTF8Encoding();
            static private ThreadStart listenOnPortThreadDelegate = new ThreadStart(network.receiveDataProc);   //接收数据函数
            static private ThreadStart sendDataThreadDelegate = new ThreadStart(network.sendRSDataProc);      //发送数据函数
            static public Thread receiveDataThread = new Thread(listenOnPortThreadDelegate);                             //接收数据线程
            static private Thread sendDataThread = new Thread(sendDataThreadDelegate);                                      //发送数据线程
            //static private Form1 form1tmp = new Form1();
            static private JsonSerializerSettings setting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            static public bool networkInitialize_server()  //初始化网络
            {
                bool state = true;
                try
                {
                    listenSocket.Bind(new IPEndPoint(IPAddress.Any, 8188));
                    listenSocket.Listen(10);
                    Console.WriteLine("Listen on port");
                    acceptSocket = listenSocket.Accept();
                    Console.WriteLine("One client +");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    state = false;
                    //appLog.exceptionRecord("网络初始化异常");
                }
                finally
                {
                    GlobalVarForApp.networkStatusBool = state;
                }
                if (state == true)       //网络正常，监听线程开启
                {
                    receiveDataThread.Start();
                    sendDataThread.Start();
                }
                return state;
            }

            static public bool networkInitialize(object f)  //初始化网络
            {
                //form1tmp = (Form1)f;
                svr_port = GlobalVarForApp.server_port;     //获取配置信息
                svr_ip = GlobalVarForApp.server_ip;

              /* System.Timers.Timer aTimer = new System.Timers.Timer(2000);
                aTimer.AutoReset = true;
                aTimer.Enabled = true;*/
                bool state = true;
                try
                {
                    listenSocket.Connect(new IPEndPoint(IPAddress.Parse(svr_ip), svr_port));//连接服务器
                }
                catch (Exception e)
                {
                    state = false;
                    //appLog.exceptionRecord("网络初始化异常");
                }
                finally
                {
                    GlobalVarForApp.networkStatusBool = state;                   
                }
                if(state ==true )       //网络正常，监听线程开启
                {
                    receiveDataThread.Start();
                }
                return state;
            }

            private static void OnTimedEvent(Object source, ElapsedEventArgs e)
            {
                Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                                  e.SignalTime);
            }

            public static void receiveDataProc()            //在listenSocket 上监听
            {
                int messageCount=0;
                byte[] messageBuf = new byte[10000];
                string message = "";
                int a = -1;
                while(GlobalVarForApp.networkStatusBool)
                {
                    messageCount = 0;
                    a = -1;
                    try{
                        messageCount = acceptSocket.Receive(messageBuf);        //将接收数据放入缓冲区
                    }
                    catch (SocketException exc)
                    {
                        GlobalVarForApp.networkStatusBool = false;
                        Console.WriteLine("网络中断"+exc.Message);
                        appLog.exceptionRecord("网络中断" + exc.Message);
                        return;
                    }  
                    message = message.Trim()+u8.GetString(messageBuf,0,messageCount).Trim();
                    a = message.IndexOf("DataEnd");                                         //数据是否有DataEnd
                    if (a != -1)                       //数据中存在DataEnd
                    {
                        try
                        {
                            GlobalVarForApp.receiveMessageQueue.Enqueue(JsonConvert.DeserializeObject<RSData>(message.Substring(0, a)));       //获取有效数据
                            message = message.Substring(a + 7);
                            //form1tmp.messageHandle();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                return;
            }

            public static void sendData()
            {
                //sendDataThread is running ?
                while(sendDataThread.ThreadState  == ThreadState.Running)  //is running
                {
                    Thread.Sleep(100);
                    Console.WriteLine("rotate");
                    Console.WriteLine(sendDataThread.ThreadState);
                }
                sendDataThread.Interrupt();
            }

            public static void sendRSDataProc()
            {
                string tmp = "";
                byte[] send_buf = new byte[10000];
                int count = 0;
                while (GlobalVarForApp.networkStatusBool)       //  networkstatus normal ??
                {
                    while(GlobalVarForApp.sendMessageQueue.Count() != 0)
                    {
                        tmp = GlobalVarForApp.sendMessageQueue.Dequeue()+"DataEnd";
                        try
                        {
                            Console.WriteLine(tmp);
                            send_buf = u8.GetBytes(tmp);
                            count = acceptSocket.Send(send_buf,2000, SocketFlags.None);
                            Console.WriteLine("{0} bytes send", count);
                        }
                        catch (SocketException e)
                        {
                            Console.WriteLine("网络故障：发送数据失败" + e.Message);
                            appLog.exceptionRecord("发送数据失败" + e.Message);
                        }
                    }
                    try
                    {
                        Thread.Sleep(Timeout.Infinite);//发送数据队列为空，线程被阻止
                    }
                    catch(Exception e){
                        //do nothing
                    }
                }
                return;
            }


            //        public bool sendData(Socket 

            public static void thread(Object f)
            {
                networkInitialize(f);
                try
                {//程序在这里循环 监听 网络消息
                    while (true)
                    {
                        //接收服务器发送的消息
                        //判断服务器发送的消息类型
                        //对不同类型的消息进行分类处理
                        Console.WriteLine("listen");
                        network.receiveDataProc();

                    }
                }
                catch (Exception e)
                {
                    //MessageBox.Show("Network error!");
                }
                finally
                {
                    //System.Environment.Exit(0);
                }
            }
        }
}
