using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HousingCheck
{
    public class HousingItem : IEquatable<HousingItem>
    {
        public HousingItem(string _area, int _slot, int _id, string _size, int _price)
        {
            Area = _area;
            Slot = _slot;
            Id = _id;
            Size = _size;
            Price = _price;
            AddTime = DateTime.Now;
        }


        [DisplayName("住宅区")]
        public string Area { get; set; }
        [DisplayName("区")]
        public int Slot { get; set; }
        [DisplayName("号")]
        public int Id { get; set; }
        [DisplayName("大小")]
        public string Size { get; set; }
        [DisplayName("价格")]
        public int Price { get; set; }
        [DisplayName("记录时间")]
        public DateTime AddTime { get; set; }

        public bool Equals(HousingItem obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return (obj.Area == Area
                        && obj.Slot == Slot
                        && obj.Id == Id
                        && obj.Size == Size
                        && obj.Price == Price);
        }

    }
}
