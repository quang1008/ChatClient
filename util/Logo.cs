using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ChatClient.util
{
    class Logo
    {
        /// <summary>
        /// 生成日志文件
        /// </summary>
        /// <param name="program"></param>
        /// <param name="msg"></param>
        public static void Log(string program, string msg, string folder)
        {
            string path = Path.Combine("logo/"+ folder);
            if (!Directory.Exists(path)){
                Directory.CreateDirectory(path);
            }
            string info = path + "\\" + program;
            if (!Directory.Exists(info)){
                Directory.CreateDirectory(info);
            }
            string logFileName = path + "\\" + program + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";//生成日志文件
            StreamWriter writer;
            try
            {
                writer = File.AppendText(logFileName);
                writer.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " " + msg);
                writer.Flush();
                writer.Close();
            }
            catch (Exception e)
            {
                writer = File.AppendText(logFileName);
                writer.WriteLine(DateTime.Now.ToString("日志记录错误HH:mm:ss") + " " + e.Message + " " + msg);
                writer.Flush();
                writer.Close();
            }
        }
    }
}
