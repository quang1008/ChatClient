using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatClient.util
{
    class common
    {
        public static string byteConversionStr(byte[] bytes) {
            string str = "";
            foreach (byte b in bytes) {
                str = str + b.ToString()+"-";
            }
            str = str.Substring(0, str.Length-1);
            return str;
        }
        public static string byteConversionStrByL(byte[] bytes,int length) {
            string str = "";
           for(int i = 0;i < length;i++)
            {
                str = str + bytes[i].ToString() + "-";
            }
            str = str.Substring(0, str.Length - 1);
            return str;
        }
    }
}
