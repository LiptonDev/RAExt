using Realms;

namespace RAExt
{
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

        [PrimaryKey]
        public string Name { get; set; }
        public byte[] Bytes { get; set; }
    }
}