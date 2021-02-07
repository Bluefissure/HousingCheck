using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HousingCheck
{
    public class HousingItem
    {
        public HousingItem(string _area, int _slot, int _id, string _size, int _price)
        {
            Area = _area;
            Slot = _slot;
            Id = _id;
            Size = _size;
            Price = _price;
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
    }
}
