using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SelfhelpOrderMgr.SocketService
{
    delegate void DGShowMsg(string msg);
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            dgShowMsg = new DGShowMsg(DoShowMsg);
            ListView.CheckForIllegalCrossThreadCalls = false;
        }
        public void ShowErr(string msg, Exception ex)
        {
            ShowMsg("----------------------Err begin------------------------");
            ShowMsg(msg + ":" + ex.Message);
            ShowMsg("----------------------Err end  ------------------------");
        }
        public void ShowConsoleMsg(string msg)
        {
            Console.WriteLine(msg);
        }
        //-------------ShowMsg begin----------------
        public void ShowMsg(string msg)
        {
            if (dgShowMsg != null)
                this.Invoke(dgShowMsg, msg);
        }
        DGShowMsg dgShowMsg = null;
        void DoShowMsg(string msg)
        {
            txtShow.AppendText(msg + "\r\n");
            LogHelper.WriteInfoLog(typeof(FrmMain), msg);
        }
        //-------------ShowMsg end------------------
        Thread threadWatchPort = null;
        Socket sokWatchPort = null;
        Dictionary<string, ClientConnection> dictConnections = new Dictionary<string, ClientConnection>();

        IPAddress address = null;
        IPEndPoint endpoint = null;

        private void btnWatch_Click(object sender, EventArgs e)
        {
            StartWathService();//启动监听服务
        }

        //启动监听服务
        private void StartWathService()
        {
            WatchConnection();
            LogHelper.WriteInfoLog(typeof(FrmMain), "开启监听服务");
            btnWatch.Enabled = false;
            btnStop.Enabled = !btnWatch.Enabled;
            lblStatusInfo.Text = "已启动，正在监听中.....";
        }

        #region 开始监听客户连接
        /// <summary>
        /// 开始监听客户连接
        /// </summary>
        private void WatchConnection()
        {
            try
            {
                address = IPAddress.Parse(txtIP.Text.Trim());
                endpoint = new IPEndPoint(address, int.Parse(txtPort.Text.Trim()));
                sokWatchPort = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sokWatchPort.Bind(endpoint);
                sokWatchPort.Listen(10);
                threadWatchPort = new Thread(WatchPort);
                threadWatchPort.Name = "threadWatchPort";
                threadWatchPort.IsBackground = true;
                threadWatchPort.Start();
                ShowMsg("启动完毕，等待客户端连接......");
            }
            catch (Exception ex)
            {
                ShowErr("", ex);
                LogHelper.WriteLog(typeof(FrmMain), "开启监听服务时有异常:" + ex.ToString());

            }
        }
        #endregion

        #region 监听端口
        private void WatchPort()
        {
            while (true)
            {
                try
                {
                    Socket cSok = sokWatchPort.Accept();
                    ClientConnection conn = new ClientConnection(this, cSok);
                    ShowMsg("客户端" + cSok.RemoteEndPoint.ToString() + "连接成功...");
                    dictConnections.Add(cSok.RemoteEndPoint.ToString(), conn);
                    AddClientToList(cSok.RemoteEndPoint.ToString());
                    //listConnection.Add(conn);
                }
                catch (Exception ex)
                {
                    ShowErr("WatchPort()", ex);
                    LogHelper.WriteLog(typeof(FrmMain), "监听客户端接入时:" + ex.ToString());
                    break;
                }
            }
        }
        #endregion

        #region 向列表添加项
        /// <summary>
        /// 向列表添加项
        /// </summary>
        /// <param name="uID"></param>
        private void AddClientToList(string uID)
        {
            ListViewItem lvi = new ListViewItem(uID);
            lvi.Tag = uID;
            lvFriends.Items.Add(lvi);
        }
        #endregion

        #region 从列表中移除指定项
        /// <summary>
        /// 从列表中移除指定项
        /// </summary>
        /// <param name="uID"></param>
        public void RemoveListItem(string uID)
        {
            for (int i = 0; i < lvFriends.Items.Count; i++)
            {
                if (lvFriends.Items[i].Tag.ToString().Equals(uID))
                {
                    lvFriends.Items.RemoveAt(i);
                    break;
                }
            }
        }
        #endregion

        #region 清空所有字典中保存的客户端连接对象 - ClearConnection()
        /// <summary>
        /// 清空所有字典中保存的客户端连接对象
        /// </summary>
        private void ClearConnection()
        {
            foreach (string key in new List<string>(dictConnections.Keys))
            {
                dictConnections[key].Close();
                RemoveListItem(key);
                //dictConnections.Remove(key);
            }
            dictConnections.Clear();
        }
        #endregion

        #region 从集合中移除连接
        /// <summary>
        /// 从集合中移除连接
        /// </summary>
        /// <param name="key"></param>
        public void RemoveConnection(string key)
        {
            if (dictConnections.ContainsKey(key))
            {
                dictConnections[key].Close();
                dictConnections.Remove(key);
            }
        }
        #endregion

        #region 退出
        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要退出吗?", "系统提示!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                ClearConnection();
                try
                {
                    sokWatchPort.Close();
                    threadWatchPort.Abort();
                    dgShowMsg = null;
                    LogHelper.WriteInfoLog(typeof(FrmMain), "退出系统");
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(typeof(FrmMain), "退出系统时有异常:" + ex.ToString());
                }


                Application.Exit();
            }
        }
        #endregion

        #region 发送按钮
        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            if (lvFriends.SelectedItems.Count > 0)
            {
                ListViewItem nowItem = lvFriends.SelectedItems[0];
                string clientKey = nowItem.Tag.ToString();
                dictConnections[clientKey].SendMsg(txtInput.Text.Trim());
            }
        }
        #endregion

        #region 发送文件
        private void btnSendFile_Click(object sender, EventArgs e)
        {



            if (lvFriends.SelectedItems.Count > 0)
            {
                ListViewItem nowItem = lvFriends.SelectedItems[0];
                string clientKey = nowItem.Tag.ToString();
                dictConnections[clientKey].SendFile(txtFilePath.Text);
            }
        }
        #endregion

        #region 选择代码
        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\Documents and Settings\Administrator.ICBCOA-6E96E6BE\桌面";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = ofd.FileName;
            }
        }
        #endregion

        private void btnShake_Click(object sender, EventArgs e)
        {
            if (lvFriends.SelectedItems.Count > 0)
            {
                ListViewItem nowItem = lvFriends.SelectedItems[0];
                string clientKey = nowItem.Tag.ToString();
                dictConnections[clientKey].SendShake();
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            txtPort.Text = AppLinkHelper.getServicePort();
            txtIP.Text = AppLinkHelper.getIpAddr();
            if (AppLinkHelper.getAutoStartFlag() == "1")
            {
                StartWathService();//启动监听服务
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                sokWatchPort.Close();
                threadWatchPort.Abort();
                dgShowMsg = null;
                LogHelper.WriteInfoLog(typeof(FrmMain), "手动停止服务");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(FrmMain), "手动停止服务时有异常:" + ex.ToString());
            }
            btnStop.Enabled = false;
            btnWatch.Enabled = !btnStop.Enabled;
            lblStatusInfo.Text = "已停止，要监听请按开启.....";
        }

        private void FrmMain_Load_1(object sender, EventArgs e)
        {

        }


    }
}
