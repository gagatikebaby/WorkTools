using System;
using System.Collections.Generic;
//using System.Data.SQLite;
using System.IO;
using WorkToolsSln.Model;
using Wpf.Ui;

namespace WorkToolsSln.utils
{
    public class DBOperation
    {
        public static readonly DBOperation Instance = new DBOperation();

        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="OperationType"></param>
        /// 操作类型
        /// <returns></returns>
        public bool AddRecord(string OperationType)
        {
            DBModel record = new DBModel
            {
                DbInstanceUID = Guid.NewGuid().ToString(),
                OperationType = OperationType,
                Time = DateTime.Now
            };

            return InsertReportRecord(record);
        }

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<DBModel> GetRecords()
        {
            return DBManger.Instance.Select<DBModel>().ToList();
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public bool DeleteRecord(string uid)
        {
            DBModel record = new DBModel
            {
                DbInstanceUID = uid
            };
            return DBManger.Instance.Delete<DBModel>(record) > 0;
        }

        private bool InsertReportRecord(DBModel record)
        {
            int count = DBManger.Instance.Insert<DBModel>(record);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
