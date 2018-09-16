using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;

namespace RAExt
{
    using static Globals;

    /// <summary>
    /// Managed item data.
    /// </summary>
    public class ItemData : RealmObject
    {
        /// <summary>
        /// Inner id in database (usually index).
        /// </summary>
        [PrimaryKey] public int Uid { get; set; }
        
        /// <summary>
        /// Item type.
        /// </summary>
        [Indexed] public int iItemType { get; set; } //type: ItemType

        /// <summary>
        /// List type.
        /// </summary>
        [Indexed] public int iListType { get; set; } //type: ItemType
        
        /// <summary>
        /// Item id in elements.data
        /// </summary>
        [Indexed] public int Id { get; set; }

        /// <summary>
        /// Item name in elements.data
        /// </summary>
        [Indexed] public string Name { get; set; }

        /// <summary>
        /// Item color in elements.data
        /// </summary>
        public int ColorArgb { get; set; }

        /// <summary>
        /// Item icon name.
        /// </summary>
        public string IconName { get; set; } //on 'ItemIcon' table

        /// <summary>
        /// Item description in configs.pck
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Amount in stack in elements.data
        /// </summary>
        public int StackAmount { get; set; }

        /// <summary>
        /// Item mask in elements.data
        /// </summary>
        public int Mask { get; set; }

        /// <summary>
        /// Item proctype (bit masks) in elements.data
        /// </summary>
        public int ProcType { get; set; }

        /// <summary>
        /// Item octet from RAE.
        /// </summary>
        public byte[] Octet { get; set; }

        /// <summary>
        /// Not supported yet.
        /// </summary>
        [Obsolete("Not supported yet.", true)] public string HtmlContent { get; set; }

        /// <summary>
        /// Not supported yet.
        /// </summary>
        [Obsolete("Not supported yet.", true)] public IList<string> Values { get; }

        /// <summary>
        /// Not supported yet.
        /// </summary>
        [Obsolete("Not supported yet.", true)] public byte[] BinaryData { get; set; }
 
        
        public ItemData() {}
        public ItemData(int uid = 0, ItemType itemType = default, ItemType listType = default, 
            int id = default, string name = null, int colorArgb = default, string iconName = null, 
            string description = null, int stackAmount = default, int mask = default, 
            int procType = default, byte[] octet = null)
        {
            iItemType = (int)itemType;
            iListType = (int)listType;
            Id = id;
            Name = name;
            ColorArgb = colorArgb;
            IconName = iconName;
            Description = description;
            StackAmount = stackAmount;
            Mask = mask;
            ProcType = procType;
            Octet = octet;

            if (uid == 0 && !Realm.IsClosed)
                uid = Realm.GetNewId<ItemData>("uid");
            Uid = uid;
        }

        /// <summary>
        /// Item type.
        /// </summary>
        [Ignored] public ItemType ItemType => (ItemType)iItemType;

        /// <summary>
        /// List type.
        /// </summary>
        [Ignored] public ItemType ListType => (ItemType)iListType;
        
        /// <summary>
        /// Item icon.
        /// </summary>
        [Ignored]
        public ItemIcon Icon {
            get
            {
                ItemIcon result = null;
                if (IsManaged && !string.IsNullOrWhiteSpace(IconName))
                {
                    result = Realm.Find<ItemIcon>(IconName);
                }

                return result;
            } 
        }

        /// <summary>
        /// Item icon.
        /// </summary>
        public byte[] IconBytes => Icon?.Bytes;

        /// <summary>
        /// Not supported yet.
        /// </summary>
        [Obsolete("Not supported yet.", true)]
        public string this[string fieldName]
        {
            get
            {
                int f = Realm.Find<FieldInfo>(fieldName).Index;
                return Values[f];
            }
        }

        /// <summary>
        /// Convert unmanaged item data to managed item data.
        /// </summary>
        /// <param name="unmanaged">Unmanaged item data.</param>
        /// <returns></returns>
        public static ItemData FromUnmanaged(ItemDataUnmanaged unmanaged)
            => RMapper.Map<ItemData>(unmanaged);

        /// <summary>
        /// Parse json to item data.
        /// </summary>
        /// <param name="json">Json.</param>
        /// <returns></returns>
        public static ItemData FromJson(string json)
            => JsonConvert.DeserializeObject<ItemData>(json);

        /// <summary>
        /// Convert managed item data to unmanaged.
        /// </summary>
        /// <returns></returns>
        public ItemDataUnmanaged ToUnmanaged()
            => RMapper.Map<ItemDataUnmanaged>(this);

        /// <summary>
        /// Getting json from item data.
        /// </summary>
        /// <param name="saveIconBytes">Save icon bytes.</param>
        /// <returns></returns>
        public string ToJson(bool saveIconBytes = true) 
            => ToUnmanaged().ToJson(saveIconBytes);
        
    }
}
