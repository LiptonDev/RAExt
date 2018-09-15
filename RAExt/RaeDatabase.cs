using AutoMapper;
using Realms;
using System.Collections.Generic;
using System.Linq;

namespace RAExt
{
    public class RaeDatabase
    {
        public delegate void ProgressDelegate(int current, int total);
        public delegate void ErrorDelegate(string message);
        public delegate void CompleteDelegate();
        
        public event ProgressDelegate OnProgress;
        public event ErrorDelegate OnError;
        public event CompleteDelegate OnComplete;

        public Realm realm;
        
        public RaeDatabase(string filename = "rae_items.realm")
        {
            InitRealm(filename);
        }

        public void InitRealm(string filename = "rae_items.realm")
        {
            var config = new RealmConfiguration(filename);
            RealmConfiguration.DefaultConfiguration = config;
            realm = Realm.GetInstance();   
        }

        public void SaveToRealm(ItemDataUnmanaged[] items)
        {
            var managedItems = new ItemData[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                managedItems[i] = ItemData.FromUnmanaged(items[i]);
            }
            SaveToRealm(managedItems);
        }

        public void SaveToRealm(ItemData[] items)
        {
            int current = 0, total = items.Length;
            using (var trans = realm.BeginWrite())
            {
	            //realm.RemoveAll()
				foreach (var itemData in items)
                {
                    realm.Add(itemData);

                    if (OnProgress == null) continue;
                    if (++current % 10 == 0 || current == total)
                        OnProgress(current, total);
                }
                trans.Commit();
            }
            
            Realm.Compact();
            realm.Dispose();
	        OnComplete?.Invoke();
        }

	    public void RemoveAll() => realm.Write(() => realm.RemoveAll());
	    public void CloseRealm()
	    {
		    Realm.Compact();
		    realm.Dispose();
	    }
	    
	    
			//Realm.DeleteRealm(RealmConfiguration.DefaultConfiguration);

	 //   private Transaction transaction;
		//public void BeginWrite()
		//{
		//	transaction = realm.BeginWrite();
		//}
		//public void AddItem()
		//{
		//	transaction = realm.BeginWrite();
		//}
		//public void EndWrite()
		//{
		//	transaction.Commit();
		//	transaction.Dispose();
		//}


		public ItemData GetItem(int id, ItemType itemType = ItemType.Matter)
        {
			if (realm == null) InitRealm();
	        if(itemType > ItemType._PW_STARTS_HERE_)
		        return realm.All<ItemData>().FirstOrDefault(i => i.Id == id && i.iListType == (int)itemType);
		    else
				return realm.All<ItemData>().FirstOrDefault(i => i.Id == id && i.iItemType == (int)itemType);
		}
    }
}