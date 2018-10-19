using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.IO;

namespace ChatClient
{
    public partial class FClient : Form
    {
        public FClient()
        {
            InitializeComponent();
            //关闭对文本框的非法线程操作检查
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }
        //创建1个客户端套接字和1个负责监听服务端请求的线程  
        Socket socketClient = null;
        Thread threadClient = null;

        private void btnBeginListen_Click(object sender, EventArgs e)
        {
            //定义一个套字节监听包含3个参数(IP4寻址协议,流式连接,TCP协议)
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //需要获取文本框中的IP地址
            IPAddress ipaddress = IPAddress.Parse(txtIP.Text.Trim());
            //将获取的ip地址和端口号绑定到网络节点endpoint上
            IPEndPoint endpoint = new IPEndPoint(ipaddress, int.Parse(txtPort.Text.Trim()));
            try
            {
                //这里客户端套接字连接到网络节点(服务端)用的方法是Connect 而不是Bind
                socketClient.Connect(endpoint);
                //创建一个线程 用于监听服务端发来的消息
                threadClient = new Thread(RecMsg);
                //将窗体线程设置为与后台同步
                threadClient.IsBackground = true;
                //启动线程
                threadClient.Start();
                MessageBox.Show("与服务器连接成功");
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 接收服务端发来信息的方法
        /// </summary>
        private void RecMsg()
        {
            while (true) //持续监听服务端发来的消息
            {
                string folder = "serverFolder";
                //定义一个1M的内存缓冲区 用于临时性存储接收到的信息
                byte[] arrRecMsg = new byte[1024 * 1024];
                //将客户端套接字接收到的数据存入内存缓冲区, 并获取其长度
                int length = socketClient.Receive(arrRecMsg);
                //将套接字获取到的字节数组转换为人可以看懂的字符串
                //string strRecMsg = Encoding.UTF8.GetString(arrRecMsg, 0, length);

                //时间转化为20180809格式
                string fileName = util.DateOptions.DateConversionString();
                //把服务器反馈的信息写进对应的logo文件中
                util.Logo.Log(fileName, util.common.byteConversionStrByL(arrRecMsg,length), folder);
            }
        }

        /// <summary>
        /// 发送字符串信息到服务端的方法
        /// </summary>
        /// <param name="sendMsg">发送的字符串信息</param>
        private void ClientSendMsg(byte[] sendMsg)
        {
            string folder = "clientFolder";
            byte[] bytes = sendMsg;
            if (socketClient != null)
            {
                //调用客户端套接字发送字节数组
                socketClient.Send(bytes);
                //时间转化为20180809格式
                string fileName = util.DateOptions.DateConversionString();
                //把发送到服务器端的信息写进logo文件中
                util.Logo.Log(fileName, util.common.byteConversionStr(sendMsg), folder);

            }
            else {
                MessageBox.Show(string.Format("未与服务器连接上{0}，不能进行测试", "ARG0"));
            }
        }

        //点击按钮btnSend 向服务端发送信息
        private void btnSend_Click(object sender, EventArgs e)
        {
            List<int> arrayList = new List<int>();
            if (ccb_len_l.Text != "")
            {
                string value = ccb_len_l.Text;
                int intValue = Int32.Parse(value);
                arrayList.Add(intValue);

            }
            if (ccb_len_h.Text != "")
            {
                string value = ccb_len_h.Text;
                int intValue = Int32.Parse(value);
                arrayList.Add(intValue);

            }
            if (message_type.Text != "")
            {
                string value = message_type.Text;
                int intValue = Int32.Parse(value);
                arrayList.Add(intValue);

            }
            if (reserved_fisrt.Text != "")
            {
                string value = reserved_fisrt.Text;
                int intValue = Int32.Parse(value);
                arrayList.Add(intValue);

            }
            if (reserved_second.Text != "")
            {
                string value = reserved_second.Text;
                int intValue = Int32.Parse(value);
                arrayList.Add(intValue);

            }
            if (reserved_three.Text != "")
            {
                string value = reserved_three.Text;
                int intValue = Int32.Parse(value);
                arrayList.Add(intValue);

            }
            if (sub_command.Text != "")
            {
                string value = sub_command.Text;
                int intValue = Int32.Parse(value);
                arrayList.Add(intValue);

            }
            if (sub_node.Text != "")
            {
                string value = sub_node.Text;
                int intValue = Int32.Parse(value);
                arrayList.Add(intValue);

            }
            if (data_zero.Text != "")
            {
                string value = data_zero.Text;
                int intValue = Int32.Parse(value);
                arrayList.Add(intValue);

            }
            if (data_second.Text != "")
            {
                string value = data_second.Text;
                int intValue = Int32.Parse(value);
                arrayList.Add(intValue);

            }
            if (data_first.Text != "")
            {
                string value = data_first.Text;
                int intValue = Int32.Parse(value);
                arrayList.Add(intValue);

            }
            if (data_three.Text != "")
            {
                string value = data_three.Text;
                int intValue = Int32.Parse(value);
                arrayList.Add(intValue);

            }
            if (data_four.Text != "")
            {
                string value = data_four.Text;
                int intValue = Int32.Parse(value);
                arrayList.Add(intValue);

            }
            if (data_five.Text != "")
            {
                string value = data_five.Text;
                int intValue = Int32.Parse(value);
                arrayList.Add(intValue);

            }
            if (data_six.Text != "")
            {
                string value = data_six.Text;
                int intValue = Int32.Parse(value);
                arrayList.Add(intValue);

            }
            byte[] bytes = new byte[arrayList.Count];
            for (int i = 0; i < arrayList.Count; i++)
            {
                bytes[i] = ((byte)arrayList[i]);
            }
            //调用ClientSendMsg方法 将文本框中输入的信息发送给服务端
            ClientSendMsg(bytes);
        }

        //快捷键 Enter发送信息
        private void txtCMsg_KeyDown(object sender, KeyEventArgs e)
        {
            //当光标位于文本框时 如果用户按下了键盘上的Enter键 
            if (e.KeyCode == Keys.Enter)
            {
                //则调用客户端向服务端发送信息的方法
                //ClientSendMsg(txtCMsg.Text.Trim());
            }
        }

        public void FClient_Load(object sender, EventArgs e) {
            ccb_len_l.SelectedIndex = 0;
        }
        private void rb23_CheckedChanged(object sender, EventArgs e)
        {
            ShowFunColor(2);
            showFunInformation();
        }

        /// <summary>
        /// 让功能对应的选相显示特殊的颜色
        /// </summary>
        /// <param name="indexvalue"></param>
        private void ShowFunColor(int indexvalue)
        {
            Color normalColor, indicatorColor;
            normalColor = Color.Black;
            indicatorColor = Color.Red;
            label3.ForeColor = normalColor;
            label4.ForeColor = normalColor;
            label5.ForeColor = normalColor;
            label6.ForeColor = normalColor;
            label7.ForeColor = normalColor;
            label8.ForeColor = normalColor;
            label9.ForeColor = normalColor;
            label10.ForeColor = normalColor;
            label11.ForeColor = normalColor;
            label12.ForeColor = normalColor;
            label13.ForeColor = normalColor;
            label14.ForeColor = normalColor;
            label15.ForeColor = normalColor;
            label16.ForeColor = normalColor;
            label17.ForeColor = normalColor;
            switch (indexvalue)
            {
                case 1:
                    label3.ForeColor = indicatorColor;
                    label4.ForeColor = indicatorColor;
                    label5.ForeColor = indicatorColor;
                    label6.ForeColor = indicatorColor;
                    label7.ForeColor = indicatorColor;
                    label8.ForeColor = indicatorColor;
                    label9.ForeColor = indicatorColor;
                    label10.ForeColor = indicatorColor;
                    label11.ForeColor = indicatorColor;
                    label12.ForeColor = indicatorColor;
                    label13.ForeColor = indicatorColor;
                    label14.ForeColor = indicatorColor;
                    label15.ForeColor = indicatorColor;
                    label16.ForeColor = indicatorColor;
                    label17.ForeColor = indicatorColor;
                    ccb_len_l.SelectedIndex = 7;
                    break;
                case 2:
                    label3.ForeColor = indicatorColor;
                    label4.ForeColor = indicatorColor;
                    label5.ForeColor = indicatorColor;
                    label6.ForeColor = indicatorColor;
                    label7.ForeColor = indicatorColor;
                    label8.ForeColor = indicatorColor;
                    label9.ForeColor = indicatorColor;
                    label10.ForeColor = indicatorColor;
                    ccb_len_l.SelectedIndex = 0;
                    break;
                case 3:
                    label3.ForeColor = indicatorColor;
                    label4.ForeColor = indicatorColor;
                    label5.ForeColor = indicatorColor;
                    label6.ForeColor = indicatorColor;
                    label7.ForeColor = indicatorColor;
                    label8.ForeColor = indicatorColor;
                    label9.ForeColor = indicatorColor;
                    label10.ForeColor = indicatorColor;
                    ccb_len_l.SelectedIndex = 0;
                    break;
                case 4:
                    label3.ForeColor = indicatorColor;
                    label4.ForeColor = indicatorColor;
                    label5.ForeColor = indicatorColor;
                    label6.ForeColor = indicatorColor;
                    label7.ForeColor = indicatorColor;
                    label8.ForeColor = indicatorColor;
                    label9.ForeColor = indicatorColor;
                    label10.ForeColor = indicatorColor;
                    ccb_len_l.SelectedIndex = 0;
                    break;
                case 5:
                    label3.ForeColor = indicatorColor;
                    label4.ForeColor = indicatorColor;
                    label5.ForeColor = indicatorColor;
                    label6.ForeColor = indicatorColor;
                    label7.ForeColor = indicatorColor;
                    label8.ForeColor = indicatorColor;
                    label9.ForeColor = indicatorColor;
                    label10.ForeColor = indicatorColor;
                    ccb_len_l.SelectedIndex = 0;
                    break;
                case 6:
                    label3.ForeColor = indicatorColor;
                    label4.ForeColor = indicatorColor;
                    label5.ForeColor = indicatorColor;
                    label6.ForeColor = indicatorColor;
                    label7.ForeColor = indicatorColor;
                    label8.ForeColor = indicatorColor;
                    label9.ForeColor = indicatorColor;
                    label10.ForeColor = indicatorColor;
                    label11.ForeColor = indicatorColor;
                    label12.ForeColor = indicatorColor;
                    label13.ForeColor = indicatorColor;
                    label14.ForeColor = indicatorColor;
                    label15.ForeColor = indicatorColor;
                    label16.ForeColor = indicatorColor;
                    label17.ForeColor = indicatorColor;
                    ccb_len_l.SelectedIndex = 7;
                    break;
                case 7:
                    label3.ForeColor = indicatorColor;
                    label4.ForeColor = indicatorColor;
                    label5.ForeColor = indicatorColor;
                    label6.ForeColor = indicatorColor;
                    label7.ForeColor = indicatorColor;
                    label8.ForeColor = indicatorColor;
                    label9.ForeColor = indicatorColor;
                    label10.ForeColor = indicatorColor;
                    ccb_len_l.SelectedIndex = 0;
                    break;
                case 8:
                    label3.ForeColor = indicatorColor;
                    label4.ForeColor = indicatorColor;
                    label5.ForeColor = indicatorColor;
                    label6.ForeColor = indicatorColor;
                    label7.ForeColor = indicatorColor;
                    label8.ForeColor = indicatorColor;
                    label9.ForeColor = indicatorColor;
                    label10.ForeColor = indicatorColor;
                    label11.ForeColor = indicatorColor;
                    label12.ForeColor = indicatorColor;
                    ccb_len_l.SelectedIndex = 2;
                    break;
                case 9:
                    label3.ForeColor = indicatorColor;
                    label4.ForeColor = indicatorColor;
                    label5.ForeColor = indicatorColor;
                    label6.ForeColor = indicatorColor;
                    label7.ForeColor = indicatorColor;
                    label8.ForeColor = indicatorColor;
                    label9.ForeColor = indicatorColor;
                    label10.ForeColor = indicatorColor;
                    ccb_len_l.SelectedIndex = 0;
                    break;
                case 10:
                    label3.ForeColor = indicatorColor;
                    label4.ForeColor = indicatorColor;
                    label5.ForeColor = indicatorColor;
                    label6.ForeColor = indicatorColor;
                    label7.ForeColor = indicatorColor;
                    label8.ForeColor = indicatorColor;
                    label9.ForeColor = indicatorColor;
                    label10.ForeColor = indicatorColor;
                    label11.ForeColor = indicatorColor;
                    label12.ForeColor = indicatorColor;
                    ccb_len_l.SelectedIndex = 2;
                    break;
                case 11:
                    label3.ForeColor = indicatorColor;
                    label4.ForeColor = indicatorColor;
                    label5.ForeColor = indicatorColor;
                    label6.ForeColor = indicatorColor;
                    label7.ForeColor = indicatorColor;
                    label8.ForeColor = indicatorColor;
                    label9.ForeColor = indicatorColor;
                    label10.ForeColor = indicatorColor;
                    label11.ForeColor = indicatorColor;
                    label12.ForeColor = indicatorColor;
                    ccb_len_l.SelectedIndex = 2;
                    break;
                case 12:
                    label3.ForeColor = indicatorColor;
                    label4.ForeColor = indicatorColor;
                    label5.ForeColor = indicatorColor;
                    label6.ForeColor = indicatorColor;
                    label7.ForeColor = indicatorColor;
                    label8.ForeColor = indicatorColor;
                    label9.ForeColor = indicatorColor;
                    label10.ForeColor = indicatorColor;
                    label11.ForeColor = indicatorColor;
                    label12.ForeColor = indicatorColor;
                    label13.ForeColor = indicatorColor;
                    label14.ForeColor = indicatorColor;
                    ccb_len_l.SelectedIndex = 4;
                    break;
                case 13:
                    label3.ForeColor = indicatorColor;
                    label4.ForeColor = indicatorColor;
                    label5.ForeColor = indicatorColor;
                    label6.ForeColor = indicatorColor;
                    label7.ForeColor = indicatorColor;
                    label8.ForeColor = indicatorColor;
                    label9.ForeColor = indicatorColor;
                    label10.ForeColor = indicatorColor;
                    label11.ForeColor = indicatorColor;
                    label12.ForeColor = indicatorColor;
                    label13.ForeColor = indicatorColor;
                    label14.ForeColor = indicatorColor;
                    label15.ForeColor = indicatorColor;
                    ccb_len_l.SelectedIndex = 5;
                    break;
                case 14:
                    label3.ForeColor = indicatorColor;
                    label4.ForeColor = indicatorColor;
                    label5.ForeColor = indicatorColor;
                    label6.ForeColor = indicatorColor;
                    label7.ForeColor = indicatorColor;
                    label8.ForeColor = indicatorColor;
                    label9.ForeColor = indicatorColor;
                    label10.ForeColor = indicatorColor;
                    ccb_len_l.SelectedIndex = 0;
                    break;
                case 15:
                    label3.ForeColor = indicatorColor;
                    label4.ForeColor = indicatorColor;
                    label5.ForeColor = indicatorColor;
                    label6.ForeColor = indicatorColor;
                    label7.ForeColor = indicatorColor;
                    label8.ForeColor = indicatorColor;
                    label9.ForeColor = indicatorColor;
                    label10.ForeColor = indicatorColor;
                    ccb_len_l.SelectedIndex = 0;
                    break;
                case 16:
                    label3.ForeColor = indicatorColor;
                    label4.ForeColor = indicatorColor;
                    label5.ForeColor = indicatorColor;
                    label6.ForeColor = indicatorColor;
                    label7.ForeColor = indicatorColor;
                    label8.ForeColor = indicatorColor;
                    label9.ForeColor = indicatorColor;
                    label10.ForeColor = indicatorColor;
                    ccb_len_l.SelectedIndex = 0;
                    break;
                case 17:
                    label3.ForeColor = indicatorColor;
                    label4.ForeColor = indicatorColor;
                    label5.ForeColor = indicatorColor;
                    label6.ForeColor = indicatorColor;
                    label7.ForeColor = indicatorColor;
                    label8.ForeColor = indicatorColor;
                    label9.ForeColor = indicatorColor;
                    label10.ForeColor = indicatorColor;
                    ccb_len_l.SelectedIndex = 0;
                    break;
                case 18:
                    label3.ForeColor = indicatorColor;
                    label4.ForeColor = indicatorColor;
                    label5.ForeColor = indicatorColor;
                    label6.ForeColor = indicatorColor;
                    label7.ForeColor = indicatorColor;
                    label8.ForeColor = indicatorColor;
                    label9.ForeColor = indicatorColor;
                    label10.ForeColor = indicatorColor;
                    ccb_len_l.SelectedIndex = 0;
                    break;
                case 19:
                    label3.ForeColor = indicatorColor;
                    label4.ForeColor = indicatorColor;
                    label5.ForeColor = indicatorColor;
                    label6.ForeColor = indicatorColor;
                    label7.ForeColor = indicatorColor;
                    label8.ForeColor = indicatorColor;
                    label9.ForeColor = indicatorColor;
                    label10.ForeColor = indicatorColor;
                    ccb_len_l.SelectedIndex = 0;
                    break;
                case 20:
                    label3.ForeColor = indicatorColor;
                    label4.ForeColor = indicatorColor;
                    label5.ForeColor = indicatorColor;
                    label6.ForeColor = indicatorColor;
                    label7.ForeColor = indicatorColor;
                    label8.ForeColor = indicatorColor;
                    label9.ForeColor = indicatorColor;
                    label10.ForeColor = indicatorColor;
                    ccb_len_l.SelectedIndex = 0;
                    break;
                case 21:
                    label3.ForeColor = indicatorColor;
                    label4.ForeColor = indicatorColor;
                    label5.ForeColor = indicatorColor;
                    label6.ForeColor = indicatorColor;
                    label7.ForeColor = indicatorColor;
                    label8.ForeColor = indicatorColor;
                    label9.ForeColor = indicatorColor;
                    label10.ForeColor = indicatorColor;
                    ccb_len_l.SelectedIndex = 0;
                    break;
                case 22:
                    label3.ForeColor = indicatorColor;
                    label4.ForeColor = indicatorColor;
                    label5.ForeColor = indicatorColor;
                    label6.ForeColor = indicatorColor;
                    label7.ForeColor = indicatorColor;
                    label8.ForeColor = indicatorColor;
                    label9.ForeColor = indicatorColor;
                    label10.ForeColor = indicatorColor;
                    ccb_len_l.SelectedIndex = 0;
                    break;
                case 23:
                    label3.ForeColor = indicatorColor;
                    label4.ForeColor = indicatorColor;
                    label5.ForeColor = indicatorColor;
                    label6.ForeColor = indicatorColor;
                    label7.ForeColor = indicatorColor;
                    label8.ForeColor = indicatorColor;
                    label9.ForeColor = indicatorColor;
                    label10.ForeColor = indicatorColor;
                    ccb_len_l.SelectedIndex = 0;
                    break;
                case 24:
                    label3.ForeColor = indicatorColor;
                    label4.ForeColor = indicatorColor;
                    label5.ForeColor = indicatorColor;
                    label6.ForeColor = indicatorColor;
                    label7.ForeColor = indicatorColor;
                    label8.ForeColor = indicatorColor;
                    label9.ForeColor = indicatorColor;
                    label10.ForeColor = indicatorColor;
                    ccb_len_l.SelectedIndex = 0;
                    break;
                case 25:
                    label3.ForeColor = indicatorColor;
                    label4.ForeColor = indicatorColor;
                    label5.ForeColor = indicatorColor;
                    label6.ForeColor = indicatorColor;
                    label7.ForeColor = indicatorColor;
                    label8.ForeColor = indicatorColor;
                    label9.ForeColor = indicatorColor;
                    label10.ForeColor = indicatorColor;
                    label11.ForeColor = indicatorColor;
                    label12.ForeColor = indicatorColor;
                    ccb_len_l.SelectedIndex = 2;
                    break;
                default:
                    break;
            }

        }
        public byte[] IntToByteArray(int n)
        {
            byte[] b = new byte[4];
            b[0] = (byte)(n & 0xff);
            b[1] = (byte)(n >> 8 & 0xff);
            b[2] = (byte)(n >> 16 & 0xff);
            b[3] = (byte)(n >> 24 & 0xff);
            return b;
        }
        public void showFunInformation() {
            ccb_len_h.Text = "0";
            message_type.Text = "96";
            reserved_fisrt.Text = "0";
            reserved_second.Text = "0";
            reserved_three.Text = "0";
            ccb_len_h.ReadOnly = true;
            message_type.ReadOnly = true;
            reserved_fisrt.ReadOnly = true;
            reserved_second.ReadOnly = true;
            reserved_three.ReadOnly = true;

        }
        private void rb24_CheckedChanged(object sender, EventArgs e)
        {
            ShowFunColor(1);
            showFunInformation();
        }

        private void rb22_CheckedChanged(object sender, EventArgs e)
        {
            ShowFunColor(3);
            showFunInformation();
        }

        private void rb19_CheckedChanged(object sender, EventArgs e)
        {
            ShowFunColor(4);
            showFunInformation();
        }

        private void rb20_CheckedChanged(object sender, EventArgs e)
        {
            ShowFunColor(5);
            showFunInformation();
        }

        private void rb21_CheckedChanged(object sender, EventArgs e)
        {

            ShowFunColor(6);
            showFunInformation();
        }

        private void rb18_CheckedChanged(object sender, EventArgs e)
        {
            ShowFunColor(7);
            showFunInformation();
        }

        private void rb17_CheckedChanged(object sender, EventArgs e)
        {
            ShowFunColor(8);
            showFunInformation();
        }

        private void rb16_CheckedChanged(object sender, EventArgs e)
        {
            ShowFunColor(9);
            showFunInformation();
        }

        private void rb15_CheckedChanged(object sender, EventArgs e)
        {
            ShowFunColor(10);
            showFunInformation();
        }

        private void rb14_CheckedChanged(object sender, EventArgs e)
        {
            ShowFunColor(11);
            showFunInformation();
        }

        private void rb13_CheckedChanged(object sender, EventArgs e)
        {
            ShowFunColor(12);
            showFunInformation();
        }

        private void rb12_CheckedChanged(object sender, EventArgs e)
        {
            ShowFunColor(13);
            showFunInformation();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            sub_command.Text = "";
            sub_node.Text = "";
            data_zero.Text = "";
            data_first.Text = "";
            data_second.Text = "";
            data_three.Text = "";
            data_four.Text = "";
            data_five.Text = "";
            data_six.Text = "";
        }

        private void rb11_CheckedChanged(object sender, EventArgs e)
        {
            ShowFunColor(14);
            showFunInformation();
        }

        private void rb10_CheckedChanged(object sender, EventArgs e)
        {
            ShowFunColor(15);
            showFunInformation();
        }

        private void rb9_CheckedChanged(object sender, EventArgs e)
        {
            ShowFunColor(16);
            showFunInformation();
        }

        private void rb8_CheckedChanged(object sender, EventArgs e)
        {
            ShowFunColor(17);
            showFunInformation();
        }

        private void rb7_CheckedChanged(object sender, EventArgs e)
        {
            ShowFunColor(18);
            showFunInformation();
        }

        private void rb6_CheckedChanged(object sender, EventArgs e)
        {
            ShowFunColor(19);
            showFunInformation();
        }

        private void rb5_CheckedChanged(object sender, EventArgs e)
        {
            ShowFunColor(20);
            showFunInformation();
        }

        private void rb4_CheckedChanged(object sender, EventArgs e)
        {
            ShowFunColor(21);
            showFunInformation();
        }

        private void rb3_CheckedChanged(object sender, EventArgs e)
        {
            ShowFunColor(22);
            showFunInformation();
        }

        private void rb2_CheckedChanged(object sender, EventArgs e)
        {
            ShowFunColor(23);
            showFunInformation();
        }

        private void rb1_CheckedChanged(object sender, EventArgs e)
        {
            ShowFunColor(24);
            showFunInformation();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ShowFunColor(25);
            showFunInformation();
        }
    }
}
