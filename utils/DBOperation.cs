using System;
using System.Collections.Generic;
//using System.Data.SQLite;
using System.IO;
using UIDesign.Model;
using Wpf.Ui;

namespace UIDesign.utils
{
    public class DBOperation
    {
        public static readonly DBOperation Instance = new DBOperation();
        /// <summary>
        /// 数据库插入
        /// </summary>
        /// <param name="number"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public bool AddRecord(int number, double price)
        {
            DBModel record = new DBModel
            {
                DbInstanceUID = Guid.NewGuid().ToString(),
                Number = Convert.ToInt32(number),
                Price = Convert.ToInt32(price),
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
