using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Data
{
    [CreateAssetMenu(fileName = "ItemsData", menuName = "Scriptable Object Installer/Items Data")]
    public class ItemsData : ScriptableObjectInstaller
    {
        [SerializeField] private Item[] _items;

        public override void InstallBindings()
        {
            Container.BindInstance<ItemsData>(this).AsSingle();
        }

        public IEnumerable<Item> GetAllItems()
        {
            foreach (var item in _items)
            {
                yield return item;
            }
        }

        public Item GetItemByKey(string key)
        {
            var item = _items.First(x => x.Key == key);
            
            if (item == null)
                throw new Exception($"Wrong key, item not found by key {key}!");
            
            return item;
        }
        
    }
}
