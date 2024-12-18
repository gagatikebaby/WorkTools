using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIDesign.utils
{
    public class SqlConnection
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string connectionString = GetMySqlName();

        /// <summary>
        /// 连接名称
        /// </summary>
        public static string connName { get; set; }

        /// <summary>
        /// 连接方法
        /// </summary>
        /// <returns></returns>
        public static string GetMySqlName()
        {
            connName = Properties.Settings.Default.Context.ToString();
            //WriteDataBaseLog(connName);
            return connName;
        }
        private static void WriteDataBaseLog(string connName)
        {
            //LogWriter logWriter = new LogWriter();
            //logWriter.WriteLog("Log/DatabaseLog", "log:" + connName);
        }
    }
}
