using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatClient.util
{
    class DateOptions
    {
        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <returns></returns>
        public static string DateConversionString() {
            string fileName = "";
            string strDate = GetCurrentTime().ToString();
            string []strArray = strDate.Split(' ')[0].Split('/');
            fileName = strArray[0];
            for (int i = 1;i < strArray.Length;i++) {
                if (strArray[i].Length == 1)
                {
                    fileName = fileName + '0' + strArray[i];
                }
                else {
                    fileName = fileName + strArray[i];
                }
            }
            return fileName;
        }
        /// <summary>
        /// 获取当前系统时间的方法
        /// </summary>
        /// <returns>当前时间</returns>
        public static DateTime GetCurrentTime()
        {
            DateTime currentTime = new DateTime();
            currentTime = DateTime.Now;
            return currentTime;
        }
    }
}
