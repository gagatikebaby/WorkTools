using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIDesign.utils;

namespace UIDesign.Model
{
    public class RecordListInfo : ObservableObject
    {
        public RecordListInfo()
        {
            InitData();
        }

        public void InitData()
        {
            GetHistoryInfoList(DBOperation.Instance.GetRecords());
        }

        public void GetHistoryInfoList(List<DBModel> dBRecord)
         {
            if(dBRecord==null)
            {
                return;
            }

            // 清空现有数据
            RecordInfoList.Clear();

            // 排序后添加到 ObservableCollection
            var sortedList = dBRecord.OrderByDescending(a => a.Time).ToList();

            foreach (var info in sortedList)
            {
                RecordInfoList.Add(info);
            }

            // 更新序号
            for (int i = 0; i < RecordInfoList.Count; i++)
            {
                RecordInfoList[i].Index = i + 1;
            }
        }

        private ObservableCollection<DBModel> recordInfoList = new ObservableCollection<DBModel>();
        public ObservableCollection<DBModel> RecordInfoList
        {
            get => recordInfoList;
            set => SetProperty(ref recordInfoList, value);
        }

        private DBModel selectedRecordInfo;
        public DBModel SelectedRecordInfo
        {
            get => selectedRecordInfo;
            set => SetProperty(ref selectedRecordInfo, value);
        }
    }
}
