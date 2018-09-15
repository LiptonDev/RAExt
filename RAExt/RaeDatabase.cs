using System;
using AutoMapper;
using Realms;

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

        private Realm realm;
        
        public RaeDatabase(string filename = "rae_items.realm")
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ItemData, ItemDataRealm>();
                cfg.CreateMap<ItemDataRealm, ItemData>();
            });
            InitRealm(filename);
        }

        public void InitRealm(string filename = "rae_items.realm")
        {
            var config = new RealmConfiguration(filename);
            RealmConfiguration.DefaultConfiguration = config;
            var realm = Realm.GetInstance();   
        }

        public void ToRealm(ItemData[] items)
        {
            realm.RemoveAll();
            int current = 0, total = items.Length;;
            using (var trans = realm.BeginWrite())
            {
                foreach (var itemData in items)
                {
                    realm.Add(ItemDataRealm.FromUnmanaged(itemData));

                    if (OnProgress == null) continue;
                    if (++current % 10 == 0 || current == total)
                        OnProgress(current, total);
                }
                trans.Commit();
            }
            
            Realm.Compact();
            realm.Dispose();
            if (OnComplete != null)
                OnComplete();
        }

        public ItemData GetItem(int id)
        {
            if(realm == null)
                InitRealm();
            var rItem = realm.Find<ItemDataRealm>(id);
            return ItemData.FromManaged(rItem);
        }
    }
}