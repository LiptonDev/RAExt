using Realms;

namespace RAExt
{
    public class FieldInfo : RealmObject
    {
        [PrimaryKey] public string Name { get; set; }
        [Indexed] public int Index { get; set; }
        public FieldTranslation Translation { get; set; }
    }
    
    public class FieldTranslation : RealmObject
    {
        
        public string Ru { get; set; }
        public string En { get; set; }
        public string Cn { get; set; }
        public string Pt { get; set; }
//        
//        [Backlink(nameof(FieldInfo.Translation))]
//        public FieldInfo FieldInfo{ get; set; }
    }
}