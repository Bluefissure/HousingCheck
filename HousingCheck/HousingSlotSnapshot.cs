using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Newtonsoft.Json;

namespace HousingCheck
{
    public class HousingSlotSnapshotJSONObject
    {
        public long time;
        public int server;
        public string area;
        public int slot;
        public HousingItemJSONObject[] houses;
    }

    public class HousingSlotSnapshot
    {
        public DateTime Time;
        public int ServerId;
        public HouseArea Area = HouseArea.UNKNOW;
        public int Slot;
        public SortedList<int, HousingItem> HouseList = new SortedList<int, HousingItem>();

        /// <summary>
        /// 从buffer中解析房区信息
        /// </summary>
        /// <param name="message"></param>
        public HousingSlotSnapshot(byte[] message)
        {
            Time = DateTime.Now;

            var dataList = message.SubArray(32, message.Length - 32);
            var dataHeader = dataList.SubArray(0, 8);
            switch (dataHeader[4])
            {
                case 0x53:
                    Area = HouseArea.海雾村;
                    break;
                case 0x54:
                    Area = HouseArea.薰衣草苗圃;
                    break;
                case 0x55:
                    Area = HouseArea.高脚孤丘;
                    break;
                case 0x81:
                    Area = HouseArea.白银乡;
                    break;
            }
            Slot = dataHeader[2];

            for (int i = 8; i < dataList.Length; i += 40)
            {
                int houseId = (i - 8) / 40;
                HousingItem house = new HousingItem(this, houseId, dataList.SubArray(i, 40));
                HouseList.Add(houseId, house);
            }
        }

        public HousingItem[] GetOnSale()
        {
            List<HousingItem> onSaleList = new List<HousingItem>();
            foreach(HousingItem house in HouseList.Values)
            {
                if (house.IsEmpty)
                {
                    onSaleList.Add(house);
                }
            }
            return onSaleList.ToArray();
        }

        public string ToCsv()
        {
            StringBuilder csv = new StringBuilder();
            foreach(var house in HouseList.Values)
            {
                csv.AppendLine(house.ToCsvLine());
            }
            csv.AppendLine();
            return csv.ToString();
        }

        public HousingSlotSnapshotJSONObject ToJsonObject()
        {
            HousingSlotSnapshotJSONObject ret = new HousingSlotSnapshotJSONObject();
            ret.area = HousingItem.GetHouseAreaStr(Area);
            ret.server = ServerId;
            ret.slot = Slot;
            ret.time = new DateTimeOffset(Time).ToUnixTimeSeconds();

            List<HousingItemJSONObject> houseListJson = new List<HousingItemJSONObject>();
            foreach(var house in HouseList.Values)
            {
                houseListJson.Add(house.ToJsonObject());
            }

            ret.houses = houseListJson.ToArray();

            return ret;
        }
    }
}
