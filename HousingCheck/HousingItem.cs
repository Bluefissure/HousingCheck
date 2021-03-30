using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HousingCheck
{
    public enum HouseOwnerType
    {
        PERSON,
        GUILD,
        EMPTY,
    }

    public enum HouseAccess
    {
        PUBLIC,
        LOCKED,
    }

    public enum HouseArea
    {
        UNKNOW,
        海雾村,
        薰衣草苗圃,
        白银乡,
        高脚孤丘
    }

    public enum HouseSize
    {
        S,
        M,
        L
    }

    public enum HouseFlagsDefine : byte
    {
        IsEstateOwned = 1,
        IsPublicEstate = 2,
        HasEstateGreeting = 4,
        EstateFlagUnknown = 8,
        IsFreeCompanyEstate = 16,
    };

    public struct HouseFlags
    {
        public bool IsForSale;
        public bool IsEstateOwned;
        public bool IsPublicEstate;
        public bool HasEstateGreeting;
        public bool EstateFlagUnknown;
        public bool IsFreeCompanyEstate;

        public HouseFlags(byte flags)
        {
            IsForSale = flags == 0;
            IsEstateOwned = (flags & (byte)HouseFlagsDefine.IsEstateOwned) > 0;
            IsPublicEstate = (flags & (byte)HouseFlagsDefine.IsPublicEstate) > 0;
            HasEstateGreeting = (flags & (byte)HouseFlagsDefine.HasEstateGreeting) > 0;
            EstateFlagUnknown = (flags & (byte)HouseFlagsDefine.EstateFlagUnknown) > 0;
            IsFreeCompanyEstate = (flags & (byte)HouseFlagsDefine.IsFreeCompanyEstate) > 0;
        }

        public override string ToString()
        {
            List<string> flags = new List<string>();
            if (IsForSale) flags.Add("IsForSale");
            if (IsEstateOwned) flags.Add("IsEstateOwned");
            if (IsPublicEstate) flags.Add("IsPublicEstate");
            if (HasEstateGreeting) flags.Add("HasEstateGreeting");
            if (EstateFlagUnknown) flags.Add("EstateFlagUnknown");
            if (IsFreeCompanyEstate) flags.Add("IsFreeCompanyEstate");

            return string.Join(", ", flags);
        }
    }

    public enum HouseTagsDefine: byte
    {
        None = 0,
        Emporium = 1,
        Boutique = 2,
        DesignerHome = 3,
        MessageBook = 4,
        Tavern = 5,
        Eatery = 6,
        ImmersiveExperience = 7,
        Cafe = 8,
        Aquarium = 9,
        Sanctum = 10,
        Venue = 11,
        Florist = 12,
        Library = 14,
        PhotoStudio = 15,
        HauntedHouse = 16,
        Atelier = 17,
        Bathhouse = 18,
        Garden = 19,
        FarEastern = 20,
    };

    public class HousingItemJSONObject
    {
        public int id;
        public string owner;
        public int price;
        public string size;
        public int[] tags;
        public bool isPersonal;
        public bool isEmpty;
        public bool isPublic;
        public bool hasGreeting;
    }

    public class HousingItem
    {
        public HousingSlotSnapshot Snapshot;

        public HouseArea Area
        {
            get
            {
                return Snapshot.Area;
            }
        }

        public int Slot
        {
            get
            {
                return Snapshot.Slot;
            }
        }

        /// <summary>
        /// 房屋id
        /// </summary>
        public int Id;

        /// <summary>
        /// 所有者
        /// </summary>
        public string Owner;

        /// <summary>
        /// 售价
        /// </summary>
        public int Price;

        /// <summary>
        /// 房屋大小
        /// </summary>
        public HouseSize Size;

        /// <summary>
        /// 房屋信息
        /// </summary>
        public HouseFlags Flags;

        /// <summary>
        /// 所有者类型
        /// </summary>
        public HouseOwnerType OwnerType;

        public bool IsEmpty
        {
            get
            {
                return OwnerType == HouseOwnerType.EMPTY;
            }
        }

        /// <summary>
        /// 访客权限
        /// </summary>
        public HouseAccess Access;

        /// <summary>
        /// 房屋展示信息
        /// </summary>
        public HouseTagsDefine[] Tags = new HouseTagsDefine[3] { 0, 0, 0 };

        public static string GetHouseAreaStr(HouseArea area)
        {
            switch (area)
            {
                case HouseArea.海雾村:
                    return "海雾村";
                case HouseArea.薰衣草苗圃:
                    return "薰衣草苗圃";
                case HouseArea.高脚孤丘:
                    return "高脚孤丘";
                case HouseArea.白银乡:
                    return "白银乡";
                default:
                    return "未知";
            }
        }

        public static string GetHouseSizeStr(HouseSize size)
        {
            switch (size)
            {
                case HouseSize.S:
                    return "S";
                case HouseSize.M:
                    return "M";
                case HouseSize.L:
                    return "L";
                default:
                    return "未知";
            }
        }

        public static string GetOwnerTypeStr(HouseOwnerType type)
        {
            switch (type)
            {
                case HouseOwnerType.EMPTY:
                    return "空房";
                case HouseOwnerType.GUILD:
                    return "部队房";
                case HouseOwnerType.PERSON:
                    return "个人房";
                default:
                    return "未知";
            }
        }

        public static string DecodeOwnerName(byte[] buffer)
        {
            int i;
            for(i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] == 0)
                    break;
            }
            return Encoding.UTF8.GetString(buffer.SubArray(0, i));
        }

        public HousingItem() { }

        /// <summary>
        /// 通过数据包获取房屋信息
        /// </summary>
        /// <param name="buffer">数据包Buffer</param>
        public HousingItem(HousingSlotSnapshot snapshot, int id, byte[] buffer)
        {
            Snapshot = snapshot;
            Id = id;
            /*
             * struct HouseInfoEntry
             * {
             *   uint32_t housePrice;
             *   uint8_t infoFlags;
             *   Common::HousingAppeal houseAppeal[3];
             *   char estateOwnerName[32];
             * }
             */
            var nameHeader = buffer.SubArray(0, 8);
            Price = BitConverter.ToInt32(nameHeader, 0);
            Size = (Price > 30000000) ? HouseSize.L : ((Price > 10000000) ? HouseSize.M : HouseSize.S);
            //读取房屋信息
            Flags = new HouseFlags(nameHeader[4]);
            //读取房屋展示信息
            for(var i = 0; i < 3; i++)
            {
                Tags[i] = (HouseTagsDefine)nameHeader[5 + i];
            }
            //读取房主姓名
            Owner = DecodeOwnerName(buffer.SubArray(8, 32));
            //获取所有者信息
            if (Flags.IsEstateOwned)
            {
                if (Flags.IsFreeCompanyEstate)
                {
                    OwnerType = HouseOwnerType.GUILD;
                }
                else
                {
                    OwnerType = HouseOwnerType.PERSON;
                }
            }
            else
            {
                OwnerType = HouseOwnerType.EMPTY;
            }
            //获取访问权限
            if (Flags.IsPublicEstate)
            {
                Access = HouseAccess.PUBLIC;
            }
            else
            {
                Access = HouseAccess.LOCKED;
            }
        }

        public string ToCsvLine()
        {
            return string.Join(",", new string[] { 
                GetHouseAreaStr(Area),
                (Slot + 1) + "区" + (Id + 1) + "号",
                GetOwnerTypeStr(OwnerType),
                Owner,
                Price.ToString(),
                GetHouseSizeStr(Size),
                (Access == HouseAccess.PUBLIC) ? "开放" : "封闭"
            });
        }

        public HousingItemJSONObject ToJsonObject()
        {
            HousingItemJSONObject ret = new HousingItemJSONObject();
            int[] tags = new int[3];
            for(var i = 0; i < 3; i++)
            {
                tags[i] = (int)Tags[i];
            }
            ret.id = Id + 1;
            ret.owner = Owner;
            ret.price = Price;
            ret.size = GetHouseSizeStr(Size);
            ret.tags = tags;
            ret.isPersonal = OwnerType == HouseOwnerType.PERSON;
            ret.isEmpty = IsEmpty;
            ret.isPublic = Access == HouseAccess.PUBLIC;
            ret.hasGreeting = Flags.HasEstateGreeting;
            return ret;
        }
    }
}
