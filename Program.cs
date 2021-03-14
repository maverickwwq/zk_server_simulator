using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.IO;

//调度令收发系统模拟服务器
namespace svrSimu
{
    class Program
    {
        static void Main(string[] args)
        {
            GlobalVarForApp.messageHandle_thread = new Thread(netandorder.HandleTheMessageReceive);
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            network.networkInitialize_server();  //initial network,listen on port 8188 on another thread
            GlobalVarForApp.messageHandle_thread.Start();
            JsonSerializerSettings setting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            string s="";
            
            while (true)
            {
                //string a = @"{""CommType"": ""DOWN_ORDER"",""CommTime"": ""2021/1/14 16:49:31"",""CommDept"": ""7830"",  ""order"": {    ""orderId"": 68895,    ""orderCode"": ""广无调单字【2021】012430号"",    ""orderOpList"": [      {        ""ID"": 260864,        ""orderId"": 68895,        ""orderYear"": 2021,        ""orderCode"": ""广无调单字【2021】012430号"",        ""sourceType"": ""正常任务"",        ""orderStatus"": ""50"",        ""orderStatusDate"": ""2021/1/14 15:02:00"",        ""sender"": ""沈如梅"",        ""sendDate"": ""2021/1/14 15:01:57"",        ""sendAccessor"": ""无线局运行调度中心"",        ""sendDept"": ""频调处"",        ""receiver"": ""系统"",        ""receiveDate"": ""2021/1/14 15:02:00"",        ""corrector"": """",        ""correctDate"": """",        ""orderRmks"": """",        ""createType"": ""N"",        ""orderFormat"": ""ORDR"",        ""flagReply"": ""N"",        ""replyDate"": """",        ""insteadFlag"": ""N"",        ""insteadId"": -1,        ""orderNumId"": 102210,        ""num"": 4,        ""orderType"": ""实验"",        ""transCode"": ""A01"",        ""antennaCode"": ""109"",        ""startDate"": ""2021/1/14 0:00:00"",        ""endDate"": ""2021/1/31 0:00:00"",        ""startTime"": ""1980/1/1 15:00:00"",        ""endTime"": ""1980/1/1 16:00:00"",        ""freq"": 10160,        ""power"": 100,        ""programCode"": ""CNR00100000"",        ""programName"": ""中一"",        ""azimuthM"": ""27"",        ""azimuthDe"": """",        ""operate"": ""开"",        ""target"": -1,        ""servArea"": ""15"",        ""days"": ""4"",        ""msgNum"": """",        ""channel"": ""1"",        ""season"": """",        ""mod"": """",        ""opRmks"": """",        ""opStatus"": ""80"",        ""opStatusDate"": ""2021/1/14 15:02:02"",        ""inexeReasonCode"": """",        ""inexePerson"": """",        ""antProg"": ""146"",        ""broadcastTime"": """",        ""innerTaskInsteadInfo"": """",        ""insteadStationCode"": """",        ""insteadTransUsedCode"": """",        ""updateTime"": ""2021/1/14 15:02:02"",        ""dealed"": 1,        ""operation"": ""U""      },      {        ""ID"": 260874,        ""orderId"": 68897,        ""orderYear"": 2021,        ""orderCode"": ""广无调单字【2021】012430号"",        ""sourceType"": ""正常任务"",        ""orderStatus"": ""50"",        ""orderStatusDate"": ""2021/1/14 15:02:51"",        ""sender"": ""乔雨丝"",        ""sendDate"": ""2021/1/14 15:02:40"",        ""sendAccessor"": ""无线局运行调度中心"",        ""sendDept"": ""频调处"",        ""receiver"": ""系统"",        ""receiveDate"": ""2021/1/14 15:02:50"",        ""corrector"": """",        ""correctDate"": """",        ""orderRmks"": """",        ""createType"": ""N"",        ""orderFormat"": ""ORDR"",        ""flagReply"": ""N"",        ""replyDate"": """",        ""insteadFlag"": ""N"",        ""insteadId"": -1,        ""orderNumId"": 102212,        ""num"": 3,        ""orderType"": ""实验"",        ""transCode"": ""A03"",        ""antennaCode"": ""107"",        ""startDate"": ""2021/1/14 0:00:00"",        ""endDate"": ""2021/1/14 0:00:00"",        ""startTime"": ""1980/1/1 15:00:00"",        ""endTime"": ""1980/1/1 16:00:00"",        ""freq"": 11170,        ""power"": 100,        ""programCode"": ""CNR00100000"",        ""programName"": ""中一"",        ""azimuthM"": ""15"",        ""azimuthDe"": """",        ""operate"": ""开"",        ""target"": -1,        ""servArea"": ""15"",        ""days"": ""4"",        ""msgNum"": """",        ""channel"": ""1"",        ""season"": """",        ""mod"": """",        ""opRmks"": """",        ""opStatus"": ""80"",        ""opStatusDate"": ""2021/1/14 15:02:52"",        ""inexeReasonCode"": """",        ""inexePerson"": """",        ""antProg"": ""216"",        ""broadcastTime"": """",        ""innerTaskInsteadInfo"": """",        ""insteadStationCode"": """",        ""insteadTransUsedCode"": """",        ""updateTime"": ""2021/1/14 15:02:52"",        ""dealed"": 1,        ""operation"": ""U""      }    ],    ""orderRecordList"": [      null    ]  },  ""ifRequestSucess"": false}";
                //a = Regex.Replace(a, @"\s", "");
                //Console.WriteLine(a.Length);
                s=Console.ReadLine();
                if (s.Contains("a"))       //down order
                {
                    RSData rsdTmp = new RSData();
                    rsdTmp.CommType = "DOWN_ORDER";
                    rsdTmp.CommDept = "7830";
                    rsdTmp.CommTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    Order orTmp = new Order();
                    orTmp.orderId = 68895;
                    orTmp.orderCode = "广无调单字【2021】012430号";
                    OrderOp opTmp = new OrderOp(260864, 68895, 2021, "广无调单字【2021】012430号", "正常任务", "50", "2021/1/14 15:02:00", "沈如梅","2021/1/14 15:01:57", "无线局运行调度中心", "频调处", "系统", "2021/1/14 15:02:00", "", "", "", "N", "ORDR", "N", "", "N", -1, 102210,1,"实验","A01","109", "2021/1/14 0:00:00", "2021/1/14 0:00:00", "1980/1/1 15:00:00", "1980/1/1 16:00:00", 10160, 100, "CNR00100000", "中一", "27","","1",-1,"15",
                        "4", "", "1", "", "", "", "80", "2021/1/14 15:02:02", "", "", "146", "", "", "", "", "2021/1/14 15:02:02",1,"U");
                    orTmp.orderOpList = new List<OrderOp>();
                    orTmp.orderOpList.Add(opTmp);
                    rsdTmp.order = orTmp;
                    Console.WriteLine(JsonConvert.SerializeObject(rsdTmp, Formatting.Indented, setting)) ;
                    network.sendData(rsdTmp);


                    Console.WriteLine("case a");
                }
                if (s.Contains("b"))                //feed_back_reply
                {
                    /*
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
                    d.orderRecordList = new List<OrderRecord>(new OrderRecord[] { new OrderRecord() });*/
                    //string a = Newtonsoft.Json.JsonConvert.SerializeObject(d, Formatting.Indented, setting);

                    //string a = @"asdfdbjfkbjtr/sgrigjprjgg'""";
                    //Console.WriteLine(a);
                    //GlobalVarForApp.sendMessageQueue.Enqueue(a);
                    //network.sendData();
                    Console.WriteLine("case b");
                   // Console.WriteLine("send {0} ",a);
                }
                if (s.Contains("c"))            //DOWN_ORDER_REPLY
                {
 
                }
                if (s.Contains("d"))            //DOWN_ORDER_REPLY
                {
                    //GlobalVarForApp.sendMessageQueue.Enqueue(JsonConvert.SerializeObject(tmp, Formatting.Indented, setting));
                }
                if (s.Contains("e"))
                {
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
