using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WorkToolsSln.Model
{
    public class DBModel : ObservableObject
    {
        private string dbInstanceUID;
        private string operationType;
        private DateTime time;
        private int index;
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [MaxLength(255)]
        public string DbInstanceUID
        {
            get => dbInstanceUID;
            set => SetProperty(ref dbInstanceUID, value);
        }

        /// <summary>
        /// 操作类型
        /// </summary>
        [MaxLength(255)]
        public string OperationType
        {
            get => operationType;
            set => SetProperty(ref operationType, value);
        }

        /// <summary>
        /// 时间
        /// </summary>
        [MaxLength(255)]
        public DateTime Time
        {
            get => time;
            set => SetProperty(ref time, value);
        }

        [NotMapped]
        public int Index
        {
            get => index;
            set => SetProperty(ref index, value);
        }
    }
}
