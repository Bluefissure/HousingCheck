using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace HousingCheck
{
    public class HousingOnSaleItem : IEquatable<HousingOnSaleItem>
    {
        public HousingOnSaleItem(HouseArea _area, int _slot, int _id, HouseSize _size, int _price, bool _status)
        {
            Area = _area;
            Slot = _slot + 1;
            Id = _id + 1;
            Size = _size;
            Price = _price;
            AddTime = DateTime.Now;
            ExistenceTime = DateTime.Now;
            CurrentStatus = _status;
        }

        public HousingOnSaleItem(HousingItem item)
        {
            Area = item.Area;
            Slot = item.Slot;
            Id = item.Id;
            Size = item.Size;
            Price = item.Price;
            AddTime = DateTime.Now;
            ExistenceTime = DateTime.Now;
            CurrentStatus = true;
        }

        //用于从json恢复数据
        public HousingOnSaleItem() { }

        [JsonIgnore]
        [DisplayName("住宅区")]
        public string AreaStr { 
            get {
                return HousingItem.GetHouseAreaStr(Area);
            }
            set { }
        }

        [JsonIgnore]
        [DisplayName("区")]
        public int DisplaySlot
        {
            get
            {
                return Slot + 1;
            }
            set { }
        }

        [JsonIgnore]
        [DisplayName("号")]
        public int DisplayId
        {
            get
            {
                return Id + 1;
            }
            set { }
        }

        [JsonIgnore]
        [DisplayName("大小")]
        public string SizeStr { 
            get {
                return HousingItem.GetHouseSizeStr(Size);
            }
            set { }
        }

        [DisplayName("价格")]
        public int Price { get; set; }

        [DisplayName("首次记录时间")]
        public DateTime AddTime { get; set; }

        [DisplayName("最后记录时间")]
        public DateTime ExistenceTime { get; set; }

        [JsonIgnore]
        [DisplayName("当前状态")]
        public string CurrentStatusStr { 
            get {
                return CurrentStatus ? "空房" : "已卖出";
            }
            set { }
        }

        public HouseArea Area = HouseArea.UNKNOW;
        public HouseSize Size = HouseSize.S;
        public int Slot = 0;
        public int Id = 0;
        public bool CurrentStatus = true;

        public bool Equals(HousingOnSaleItem obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            //这里对比价格应该用<=，不然降价后会重复记录
            return (obj.Area == Area
                        && obj.Slot == Slot
                        && obj.Id == Id
                        && obj.Price <= Price);
        }

        public void Update(HousingItem item)
        {
            if(item.Area == Area
                && item.Slot == Slot
                && item.Id == Id
                && item.Price <= Price)
            {
                Price = item.Price;
                ExistenceTime = DateTime.Now;
                CurrentStatus = item.IsEmpty;
            }
        }
    }
}
