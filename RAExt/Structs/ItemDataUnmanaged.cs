using AutoMapper;
using Newtonsoft.Json;

namespace RAExt
{
    using static Globals;

    /// <summary>
    /// Unmanaged item data.
    /// </summary>
    public class ItemDataUnmanaged
    {
        /// <summary>
        /// Item type.
        /// </summary>
        public int iItemType { get; set; } //ItemType

        /// <summary>
        /// List type.
        /// </summary>
        public int iListType { get; set; } //ItemType

        /// <summary>
        /// Item id in elements.data
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Item name in elements.data
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Item color in elements.data
        /// </summary>
        public int ColorArgb { get; set; }

        /// <summary>
        /// Item icon.
        /// </summary>
        public byte[] IconBytes { get; set; }

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
        /// Html content.
        /// </summary>
        public string HtmlContent { get; set; }

        /// <summary>
        /// Convert to managed item data.
        /// </summary>
        /// <returns></returns>
        public ItemData ToManaged()
            => RMapper.Map<ItemData>(this);

        private bool saveIconBytes;

        /// <summary>
        /// Getting json from unmanaged item data.
        /// </summary>
        /// <param name="saveIconBytes">Save icon bytes.</param>
        /// <returns></returns>
        public string ToJson(bool saveIconBytes = true)
        {
            this.saveIconBytes = saveIconBytes;
            return JsonConvert.SerializeObject(this);
        }
        //For Json lib:
        public bool ShouldSerializeIconBytes() => saveIconBytes;

        /// <summary>
        /// Convert managed item data to unmanaged.
        /// </summary>
        /// <param name="managed">Managed item data.</param>
        /// <returns></returns>
        public static ItemDataUnmanaged FromManaged(ItemData managed) =>
            RMapper.Map<ItemDataUnmanaged>(managed);

        /// <summary>
        /// Parse json to item data.
        /// </summary>
        /// <param name="json">Json.</param>
        /// <returns></returns>
        public static ItemDataUnmanaged FromJson(string json) =>
            JsonConvert.DeserializeObject<ItemDataUnmanaged>(json);
    }
}
