using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using DispatchServer;
using Dispatch.BaseUtil;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.IO;

//git test for different commit
//调度令收发系统模拟服务器
namespace svrSimu
{
    public class OrderInfo              //调度单信息
    {
        public Order orderInfo = new Order();
        public List<OrderOp> orderInstructionList = new List<OrderOp>();
        public OrderStatus orderStatus = OrderStatus.unknown;
        public OrderInfo()
        {
        }
    }

    public enum OrderStatus
    {
        unknown, unconfirmed, confirmed_noFeedback, feedback
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            network.networkInitialize_server();  //initial network,listen on port 8188 on another thread
            //JsonSerializerSettings setting = new JsonSerializerSettings {};
            JsonSerializerSettings setting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            //-------send      
            //down_order_reply          机房客户端接收反馈答复
            //feedback_order_reply      机房值班员反馈答复
            //receive_order_reply       机房值班员确认接收答复
     
            //-------receive
            //down_order_request
 
            
            string s="";
            while (true)
            {
                s=Console.ReadLine();
                if (s.Contains("a"))       //receive_order_reply 
                {
                    RSData  d = new RSData ();
                    d.CommType = "RECEIVE_ORDER_REPLY";
                    d.CommTime = System.DateTime.Now.ToUniversalTime().ToString();
                    d.CommDept = "7832";
                    d.order = new Order(28362023, 2020, "0154041", "正常调度令", "待下发",
                        "2020/05/20 12:29:30", "董晓萌", "2020/05/20 12:27:30", "郝佳", "无线局调度中心",
                        "王伟强", "2020/05/20 12:32:00", "校对人", "校对时间", "备注", "", "", "", "", "", "", "", "", 0);
                    d.infoReturn = "当班人员已接收";
                    string a = Newtonsoft.Json.JsonConvert.SerializeObject(d, Formatting.Indented, setting) ;
                    GlobalVarForApp.sendMessageQueue.Enqueue(a);
                    network.sendData();
                    Console.WriteLine("case a");
                    Console.WriteLine("send {0}",  a);
                    
                }
                if (s.Contains("b"))                //feed_back_reply
                {
                    RSData d = new RSData();
                    d.CommType = "FEED_BACK_REPLY";
                    d.CommTime = System.DateTime.Now.ToUniversalTime().ToString();
                    d.CommDept = "7832";
                    d.order = new Order(28361019, 2020, "0149395", "正常调度令", "待下发",
                        "2020/05/15 12:29:30", "董晓萌", "2020/05/15 12:27:30", "郝佳", "无线局调度中心",
                        "王伟强", "2020/05/15 12:32:00", "校对人", "校对时间", "备注", "", "", "", "", "", "", "", "", 0);
                    d.orderOpList = new List<OrderOp>(new OrderOp[] {
                        new OrderOp(28362023, 28361019, 2, "正常调度令", 7, 11, "2020/05/15 0:0:0", "2020/10/27 0:0:0", "08:00:00", "10:00:00",
                6230, 450, 1, "369", "", "HR2/0.5/4", "1", 0, "西藏", "1234567", "", "1", "", "", "", "", "", "", "", "", "", "", ""),
                new OrderOp(28362024, 28361019, 1, "正常调度令", 1, 1, "2020/05/15 0:0:0", "2020/10/27 0:0:0", "08:00:00", "10:00:00",
                6230, 100,1, "313", "", "HR2/0.5/4", "1", 0, "西藏", "1234567", "", "1", "", "", "", "", "", "", "", "", "", "", "") });
                    d.orderRecordList = new List<OrderRecord>(new OrderRecord[] { new OrderRecord() });
                    string a = Newtonsoft.Json.JsonConvert.SerializeObject(d, Formatting.Indented, setting);
                    GlobalVarForApp.sendMessageQueue.Enqueue(a);
                    network.sendData();
                    Console.WriteLine("case b");
                    Console.WriteLine("send {0} ",a);
                }
                if (s.Contains("c"))            //DOWN_ORDER_REPLY
                {
                    Console.WriteLine("case c");
                    RSData tmp = new RSData();
                    tmp.CommType = "DOWN_ORDER_REPLY";
                    tmp.CommTime = System.DateTime.Now.ToString();
                    tmp.CommDept = "7852";
                    tmp.order = new Order(28362023, 2020, "0154041", "正常调度令", "待下发",
                        "2020/05/20 12:29:30", "董晓萌", "2020/05/20 12:27:30", "郝佳", "无线局调度中心",
                        "王伟强", "2020/05/20 12:32:00", "校对人", "校对时间", "备注", "", "", "", "", "", "", "", "", 0);
                    tmp.infoReturn = "已下发";
                    GlobalVarForApp.sendMessageQueue.Enqueue(JsonConvert.SerializeObject(tmp,Formatting.Indented, setting));
                    network.sendData();
                }
                if (s.Contains("d"))            //DOWN_ORDER_REPLY
                {
                    Console.WriteLine("case d");
                    RSData tmp = new RSData();
                    tmp.CommType = "RECEIVE_ORDER_REPLY";
                    tmp.CommTime = System.DateTime.Now.ToString();
                    tmp.CommDept = "7852";
                    tmp.order = new Order(28362023, 2020, "0154041", "正常调度令", "待下发",
                        "2020/05/20 12:29:30", "董晓萌", "2020/05/20 12:27:30", "郝佳", "无线局调度中心",
                        "王伟强", "2020/05/20 12:32:00", "校对人", "校对时间", "备注", "", "", "", "", "", "", "", "", 0);
                    tmp.infoReturn = "已接收";
                    GlobalVarForApp.sendMessageQueue.Enqueue(JsonConvert.SerializeObject(tmp, Formatting.Indented, setting));
                    network.sendData();
                }
                if (s.Contains("e"))
                {
                    Console.WriteLine("case e");
                    Console.WriteLine("{0}", network.receiveDataThread.ThreadState);
                }
            }
        }
    }
    /*
    class netthread
    {
        static public void thread()
        {
            Console.WriteLine("thread start");
            string s = "";
            UTF8Encoding u8 = new UTF8Encoding();
            while (true)
            {
                ab:
                    Console.WriteLine("Loop");
                    try
                    {
                        Program.acceptSocket.Receive(Program.recBuf);
                        s = u8.GetString(Program.recBuf);
                        Console.WriteLine(s);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Thread.Sleep(2000);
                        Program.acceptSocket = Program.listenSocket.Accept();
                    }
            }
        }
    }*/
}
