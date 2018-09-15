
using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using MetroParser;
using Newtonsoft.Json;
using Realms;

namespace RAExt
{
    using static Globals;
    public class ItemData : RealmObject
    {
        /// <summary>
        /// Inner id in database (usually index).
        /// </summary>
        [PrimaryKey] public int Uid { get; set; }
        
        [Indexed] public int iItemType { get; set; } //type: ItemType
        [Indexed] public int iListType { get; set; } //type: ItemType
        
        /// <summary>
        /// Item id in elements.data
        /// </summary>
        [Indexed] public int Id { get; set; }
        [Indexed] public string Name { get; set; }
        public int ColorArgb { get; set; }
        public string IconName { get; set; } //on 'ItemIcon' table
        public string Description { get; set; }
        public int StackAmount { get; set; }
        public int Mask { get; set; }
        public int ProcType { get; set; }
        public byte[] Octet { get; set; }
        
        /*reserved*/[Obsolete("Not supported yet.", true)] public string HtmlContent { get; set; }
        /*reserved*/[Obsolete("Not supported yet.", true)] public IList<string> Values { get; }
        /*reserved*/[Obsolete("Not supported yet.", true)] public byte[] BinaryData { get; set; }
 
        
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

        [Ignored] public ItemType ItemType => (RAExt.ItemType) iItemType;
        [Ignored] public ItemType ListType => (RAExt.ItemType) iListType;
        
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

        public byte[] IconBytes => Icon?.Bytes;

        [Obsolete("Not supported yet.", true)]
        public string this[string fieldName]
        {
            get
            {
                int f = Realm.Find<FieldInfo>(fieldName).Index;
                return Values[f];
            }
        }

        public static ItemData FromUnmanaged(ItemDataUnmanaged unmanaged)
            => RMapper.Map<ItemData>(unmanaged);
        public static ItemData FromJson(string json)
            => JsonConvert.DeserializeObject<ItemData>(json);

        public ItemDataUnmanaged ToUnmanaged()
            => RMapper.Map<ItemDataUnmanaged>(this);
        public string ToJson(bool saveIconBytes = true) 
            => ToUnmanaged().ToJson(saveIconBytes);
        
    }
}