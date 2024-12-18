using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace UIDesign.Model
{
    public class DBModel : ObservableObject
    {
        private string dbInstanceUID;
        private double number;
        private double price;
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
        /// 数量
        /// </summary>
        [MaxLength(255)]
        public double Number
        {
            get => number;
            set => SetProperty(ref number, value); // 使用 SetProperty 方法来更新属性并触发通知
        }

        /// <summary>
        /// 价格
        /// </summary>
        [MaxLength(255)]
        public double Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }

        /// <summary>
        /// 价格
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
