using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.SocketService
{
    public class AppLinkHelper
    {
        //System.Configuration.ConfigurationSettings.AppSettings["connectionstring"];
        private readonly static string socketInfo = ConfigurationSettings.AppSettings["UserUintSocketInfo"];
        private readonly static string ipAddr = ConfigurationSettings.AppSettings["IpAddr"];
        private readonly static string socketPort = ConfigurationSettings.AppSettings["SocketPort"];
        private readonly static string autoStartFlag = ConfigurationSettings.AppSettings["AutoStartFlag"];

        #region 监狱转发的交易动作码
        //许可以给监狱转发的操作：
        /*
        0001:住院登记
        0002:获取犯人住院当本月在监狱已消费的金额
        0003:犯人出院
        0004:联机消费扣款
        0005:同步犯人余额信息
        0006:同步未完成的订单
        0007:网络是否在线函数
         */
        private readonly static string actionCode = ConfigurationSettings.AppSettings["ActionCode"];
        
        #endregion

        //转发格式标志，是否原包转发：0是原包转发，1是只转发Data部份，2是转发“交易码”+Data部份
        private readonly static string sendActionFlag = ConfigurationSettings.AppSettings["SendActionFlag"];
        
        //数据包头的长度32位（不含Data部份）
        private readonly static string bagLength = ConfigurationSettings.AppSettings["BagLength"];
        

        //获取监狱服务器地址列表
        public static string getSocketstr()
        {
            return socketInfo;
        }

        //获取许可以动用交易码
        public static string getActionCode()
        {
            return actionCode;
        }

        //获取服务器的IP地址
        public static string getIpAddr()
        {
            return ipAddr;
        }
        //获取服务程序的服务端口
        public static string getServicePort()
        {
            return socketPort;
        }

        //获取服务程序的服务端口
        public static string getSendActionFlag()
        {
            return sendActionFlag;
        }

        //获取自启动标志
        public static string getAutoStartFlag()
        {
            return autoStartFlag;
        }

        //获取数据包头的长度32位（不含Data部份）
        public static int getBagHeadLength()
        {
            int i = 32;
            if (bagLength != "")
            {
                i = Convert.ToInt32(bagLength);
            }
            return  i;
        }
    }


    
}