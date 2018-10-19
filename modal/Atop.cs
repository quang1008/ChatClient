using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatClient.modal
{
    class Atop
    {
        /// <summary>
        ///控制器头数据块
        /// </summary>
        private string ccb_len_l { get; set; }
        /// <summary>
        /// 控制器头8为字符和数据字符长
        /// </summary>
        private string ccb_len_h { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        private string message_type { get; set; }
        /// <summary>
        /// 返回数据1
        /// </summary>
        private string reserved_fisrt { get; set; }
        /// <summary>
        /// 返回数据2
        /// </summary>
        private string reserved_second { get; set; }
        /// <summary>
        /// 返回数据3
        /// </summary>
        private string reserved_three { get; set; }
        /// <summary>
        /// 子命令
        /// </summary>
        private string sub_command { get; set; }
        /// <summary>
        /// tag的编号
        /// </summary>
        private string sub_node { get; set; }
        /// <summary>
        /// 第一个输入数据
        /// </summary>
        private string date_zero { get; set; }
        /// <summary>
        ///  第二个输入数据
        /// </summary>
        private string date_first { get; set; }
        /// <summary>
        ///  第三个输入数据
        /// </summary>
        private string date_second { get; set; }
        /// <summary>
        ///  第四个输入数据
        /// </summary>
        private string date_three { get; set; }
        /// <summary>
        ///  第五个输入数据
        /// </summary>
        private string date_four { get; set; }
        /// <summary>
        ///  第六个输入数据
        /// </summary>
        private string date_five { get; set; }
        /// <summary>
        ///  第七个输入数据
        /// </summary>
        private string date_six { get; set; }

    }
}
