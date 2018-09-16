using Realms;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace RAExt
{
    public class RaeDatabase : IDisposable
    {
        public delegate void ProgressDelegate(int current, int total);
        public delegate void ErrorDelegate(string message);
        public delegate void CompleteDelegate();

        public event ProgressDelegate OnProgress;
        public event ErrorDelegate OnError;
        public event CompleteDelegate OnComplete;

        public Realm realm;

        /// <summary>
        /// Items cached.
        /// </summary>
        ConcurrentDictionary<int, ItemData> cache = new ConcurrentDictionary<int, ItemData>();

        public RaeDatabase(string filename = "rae_items.realm")
        {
            InitRealm(filename);
        }

        /// <summary>
        /// Load realm db.
        /// </summary>
        /// <param name="filename">Db file name.</param>
        public void InitRealm(string filename = "rae_items.realm")
        {
            var config = new RealmConfiguration(filename);
            RealmConfiguration.DefaultConfiguration = config;
            realm = Realm.GetInstance();
        }

        /// <summary>
        /// Save items to realm.
        /// </summary>
        /// <param name="items">Unma</param>
        public void SaveToRealm(IEnumerable<ItemDataUnmanaged> items) =>
            SaveToRealm(items.Select(x => ItemData.FromUnmanaged(x)));

        /// <summary>
        /// Saving items to realm.
        /// </summary>
        /// <param name="items">Items.</param>
        public void SaveToRealm(IEnumerable<ItemData> items)
        {
            int current = 0, total = items.Count();
            using (var trans = realm.BeginWrite())
            {
                foreach (var itemData in items)
                {
                    realm.Add(itemData);

                    if (++current % 10 == 0 || current == total)
                        OnProgress?.Invoke(current, total);
                }
                trans.Commit();
            }

            Dispose();
            OnComplete?.Invoke();
        }

        /// <summary>
        /// Remove all items.
        /// </summary>
        public void RemoveAll() => realm.Write(() => realm.RemoveAll());

        /// <summary>
        /// Getting item data by item id.
        /// </summary>
        /// <param name="id">Item ID.</param>
        /// <param name="itemType">Item type.</param>
        public ItemData GetItem(int id, ItemType itemType = ItemType.Matter)
        {
            if (realm == null)
                InitRealm();

            if (cache.ContainsKey(id))
                return cache[id];

            ItemData item = null;

            if (itemType > ItemType._PW_STARTS_HERE_)
                item = realm.All<ItemData>().FirstOrDefault(i => i.Id == id && i.iListType == (int)itemType);
            else item = realm.All<ItemData>().FirstOrDefault(i => i.Id == id && i.iItemType == (int)itemType);

            if (item != null)
                cache.TryAdd(id, item);

            return item;
        }

        /// <summary>
        /// Release resources.
        /// </summary>
        public void Dispose()
        {
            cache.Clear();
            Realm.Compact();
            realm.Dispose();
        }
    }
}
