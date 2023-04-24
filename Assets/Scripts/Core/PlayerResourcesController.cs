using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;
using Utils;
using Zenject;

namespace Core
{
    public class PlayerResourcesController : MonoInstaller
    {
        [Inject] private ItemsData _itemsData;
        private List<Item> _items;

        public ReactiveProperty<int> Coins = new ReactiveProperty<int>();
        
        public override void InstallBindings()
        {
            Container.BindInstance(this).AsSingle().NonLazy();
        }
        
/// <summary>
/// В следующих методах специально не сделана обработка на null чтобы вызвать nullref в случае проблемы
/// </summary>

        public void ChangeItemValue(string key, int amount)
        {
            _items.FirstOrDefault(x => x.Key == key).AddItem(amount);
        }

        public int GetItemCount(string key)
        {
            return _items.FirstOrDefault(x => x.Key == key).OwnedAmount;
        }

        public IEnumerable<Item> GetAllItems()
        {
            foreach (var item in _items)
            {
                yield return item;
            }
        }
        
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            if (!PlayerPrefs.HasKey("Coins"))
            {
                PlayerPrefs.SetInt("Coins", 1000);   
            }

            Coins.Value = PlayerPrefs.GetInt("Coins");
            Coins.ValueChanged += () => { PlayerPrefs.SetInt("Coins", Coins.Value); };
            
            _items = new List<Item>();
            foreach (var item in _itemsData.GetAllItems())
            {
                var newItem = new Item(item.Key);
                _items.Add(newItem);
            }
        }
        
        
    }
}