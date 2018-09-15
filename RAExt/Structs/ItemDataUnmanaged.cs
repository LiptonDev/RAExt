
using AutoMapper;
using Newtonsoft.Json;

namespace RAExt
{
    using static Globals;
    
    public class ItemDataUnmanaged
    {
        public int iItemType { get; set; } //ItemType
        public int iListType { get; set; } //ItemType
        public int Id { get; set; }
        public string Name { get; set; }
        public int ColorArgb { get; set; }
        public byte[] IconBytes { get; set; }
        public string Description { get; set; }
        public int StackAmount { get; set; }
        public int Mask { get; set; }
        public int ProcType { get; set; }
        public byte[] Octet { get; set; }
        public string HtmlContent { get; set; }
        
        public ItemData ToManaged() 
            => RMapper.Map<ItemData>(this);

        private bool saveIconBytes;
        public string ToJson(bool saveIconBytes = true)
        {
            this.saveIconBytes = saveIconBytes;
            return JsonConvert.SerializeObject(this);
        }
        //For Json lib:
        public bool ShouldSerializeIconBytes() => saveIconBytes;

        public static ItemDataUnmanaged FromManaged(ItemData managed) 
            => RMapper.Map<ItemDataUnmanaged>(managed);
        public static ItemDataUnmanaged FromJson(string json)
            => JsonConvert.DeserializeObject<ItemDataUnmanaged>(json);

    }
}