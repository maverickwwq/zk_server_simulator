using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DispatchServer;

namespace svrSimu
{
    public static class GlobalVarForApp        //存放程序的全局变量
    {
        public static string currentUserStr = "";
        public static bool networkStatusBool = false;
        public static int server_port = 0;         //服务器ip及端口号
        public static string server_ip = "";
        public static string client_type = "";
        public static Queue<RSData> receiveMessageQueue = new Queue<RSData>();         //消息接收队列
        public static Queue<string> sendMessageQueue = new Queue<string>();            //消息发送队列
        public static List<OrderInfo> tbh_ordersInfo = new List<OrderInfo>();                             //存放未处理完成的所有调度令的信息
    }
}
