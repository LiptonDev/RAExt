using Realms;

namespace RAExt
{
    /// <summary>
    /// Item icon.
    /// </summary>
    public class ItemIcon : RealmObject
    {
        public ItemIcon()
        {
        }

        public ItemIcon(string name, byte[] bytes = null)
        {
            Name = name;
            Bytes = bytes;
        }

        /// <summary>
        /// Icon name.
        /// </summary>
        [PrimaryKey]
        public string Name { get; set; }

        /// <summary>
        /// Icon image.
        /// </summary>
        public byte[] Bytes { get; set; }
    }
}
