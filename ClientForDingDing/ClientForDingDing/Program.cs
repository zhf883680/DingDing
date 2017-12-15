using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientForDingDing
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            string url = System.Configuration.ConfigurationManager.AppSettings["url"];
            string userId = System.Configuration.ConfigurationManager.AppSettings["userId"];
            string phone = System.Configuration.ConfigurationManager.AppSettings["phone"];
            try
            {
               var result= HttpPost(url, "userId=" + userId + "&phone=" + phone);
            }
            catch
            {

            }
        }
        /// <summary>
        /// 获取到相应流信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetResponseStream(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);  //创建请求
            var response = (HttpWebResponse)request.GetResponse();  //获取服务器传的值
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }

        #region  post请求
        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="Url">请求的Url</param>
        /// <param name="postDataStr">内容</param>
        /// <returns></returns>
        public static string HttpPost(string Url, string postDataStr)
        {
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(postDataStr);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            Stream myRequestStream = request.GetRequestStream();
            myRequestStream.Write(data, 0, data.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            using (StreamReader sr = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8")))
            {
                return sr.ReadToEnd();
            }
        }
        #endregion
    }
}
