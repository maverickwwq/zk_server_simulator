using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatchServer
{
    public class OrderRecord
    {
        public int OD_ID;//调度令号
        public int OD_NUM_ID;//调度指令序列号
        public int DOWN_COUNT;//下发次数
        public string DISPATCH_ORDER_STATUS;//分发系统的调令状态
        public string OMS_DOWN_TIME;//台运行系统下发时间
        public string DISPATCH_DOWN_TIME;//分发系统下发时间
        public string DOWN_PERSON;//中控下发人
        public string RECEIVE_DEPT;//接收部门
        public string RECEIVE_TIME;//机房接收时间
        public string RECEIVE_PERSON;//接收人
        public string INEXE_REASON;//不可执行原因
        public string BROADCAST_TIME;//开启时间
        public string FEEDBACK_TIME;//反馈时间
        public string FEEDBACK_PERSON;//反馈人员
        public string TRACK_INFO;//调度指令跟踪信息

        public OrderRecord()
        {
            OD_ID = -1;
            OD_NUM_ID = -1;
            DOWN_COUNT = 0;
            DISPATCH_ORDER_STATUS = "";
            OMS_DOWN_TIME = "";
            DISPATCH_DOWN_TIME = "";
            DOWN_PERSON = "";
            RECEIVE_DEPT = "";
            RECEIVE_TIME = "";
            RECEIVE_PERSON = "";
            INEXE_REASON = "";
            BROADCAST_TIME = "";
            FEEDBACK_TIME = "";
            FEEDBACK_PERSON = "";
            TRACK_INFO = "";
        }
    }
}
