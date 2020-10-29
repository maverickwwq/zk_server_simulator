using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch.BaseUtil
{
    class CommUtil
    {
        //通讯操作类型
        public static readonly string COMM_TYPE_DOWN_ORDER = "DOWN_ORDER"; //下发调令
        public static readonly string COMM_TYPE_CONFIRM_ORDER = "CONFIRM_ORDER";//确认接收调令
        public static readonly string COMM_TYPE_FEEDBACK_ORDER = "FEEDBACK_ORDER";//反馈调令
        public static readonly string COMM_TYPE_QUERY_ORDER = "QUERY_ORDER";//查询单个调令综合信息
        public static readonly string COMM_TYPE_QUERY_ORDERS = "QUERY_ORDERS";//查询批量调令
        public static readonly string COMM_TYPE_QUERY_ORDER_RESULT = "QUERY_ORDER_RESULT";//单个调令查询结果
        public static readonly string COMM_TYPE_QUERY_ORDERS_RESULT = "QUERY_ORDERS_RESULT";//批量调令查询结果

        //机房代码定义
        public static readonly string DEPT_JIA = "7830";//甲机房的机房代码
        public static readonly string DEPT_YI = "7831";//乙机房的机房代码
        public static readonly string DEPT_CENTER = "7850";//中控机房的机房代码

        //调令状态
        public static readonly string ORDER_STATUS_WAIT_DOWN = "待下发";
        public static readonly string ORDER_STATUS_WAIT_RECEIVE = "待接收";
        public static readonly string ORDER_STATUS_WAIT_FEEDBACK = "待反馈";
        public static readonly string ORDER_STATUS_ALREADY_FEEDBACK = "已反馈";

        //常量对应字典
        public static Dictionary<string, string> dicDept = new Dictionary<string, string>();
        public static Dictionary<string, string> dicTR = new Dictionary<string, string>();
        public static Dictionary<string, string> dicAN = new Dictionary<string, string>();
        public static Dictionary<string, string> dicSourceType = new Dictionary<string, string>();
        public static Dictionary<string, string> dicIP = new Dictionary<string, string>();
        public static Dictionary<string, string> dicOrderType = new Dictionary<string, string>();
        public static Dictionary<string, string> dicOmsOrderOpStatus = new Dictionary<string, string>();
        public static void Init()
        {
            //初始化操作
            dicDept.Add(CommUtil.DEPT_JIA, "甲机房");
            dicDept.Add(CommUtil.DEPT_YI, "乙机房");
            dicDept.Add(CommUtil.DEPT_CENTER, "中控机房");
            dicTR.Add("1", "A01");
            dicTR.Add("2", "A02");
            dicTR.Add("3", "A03");
            dicTR.Add("4", "A04");
            dicTR.Add("5", "A05");
            dicTR.Add("6", "A06");
            dicTR.Add("7", "B01");
            dicAN.Add("1", "101");
            dicAN.Add("2", "102");
            dicAN.Add("3", "103");
            dicAN.Add("4", "104");
            dicAN.Add("5", "105");
            dicAN.Add("6", "106");
            dicAN.Add("7", "107");
            dicTR.Add("8", "108");
            dicAN.Add("9", "109");
            dicAN.Add("10", "110");
            dicAN.Add("11", "201");
            dicSourceType.Add("NORM","正常任务");
            dicSourceType.Add("INTR", "台际代播");
            dicSourceType.Add("INNR", "自台代播");
            dicOrderType.Add("D", "对内广播");
            dicOrderType.Add("L", "地方广播");
            dicOrderType.Add("F", "对外广播");
            dicOrderType.Add("S", "外转中");
            dicOrderType.Add("M", "中转外");
            dicOrderType.Add("I", "实验");
            dicOmsOrderOpStatus.Add("10","新建");
            dicOmsOrderOpStatus.Add("20", "已校对");
            dicOmsOrderOpStatus.Add("21", "已接收未校对");
            dicOmsOrderOpStatus.Add("30", "已校对");
            dicOmsOrderOpStatus.Add("40", "中控不可执行");
            dicOmsOrderOpStatus.Add("41", "机房不可执行");
            dicOmsOrderOpStatus.Add("50", "中控可执行");
            dicOmsOrderOpStatus.Add("60", "中控完成");
            dicOmsOrderOpStatus.Add("70", "等待下发机房");
            dicOmsOrderOpStatus.Add("80", "已下发机房");
            dicOmsOrderOpStatus.Add("81", "机房可执行");

            /*
            dicIP.Add(CommUtil.DEPT_JIA, Properties.Settings.Default.DEPT_JIA_IP);;
            dicIP.Add(CommUtil.DEPT_YI, Properties.Settings.Default.DEPT_YI_IP);
            dicIP.Add(CommUtil.DEPT_CENTER, Properties.Settings.Default.DEPT_CENTER_IP);
             */
        }
    }
}
