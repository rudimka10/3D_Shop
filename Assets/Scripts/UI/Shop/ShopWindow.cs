using System;
using Core;
using Data;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.Shop
{
    public class ShopWindow : MonoInstaller, IClickListener
    {
        [Inject] private ItemsData _itemsData;
        [Inject] private PlayerResourcesController _playerResourcesController;
        
        [SerializeField] private ShopElement _shopElementPrefab;
        [SerializeField] private Transform _itemsParent;
        [SerializeField] private TMP_Text _header;
        
        private PoolMono<ShopElement> _pool;
        private WindowType _currentWindowType;
        
        public override void InstallBindings()
        {
            Container.BindInstance(this).AsSingle().NonLazy();
        }

        private void Awake()
        {
            _pool = new PoolMono<ShopElement>(_shopElementPrefab, 0, _itemsParent)
            {
                Autoexpand = true
            };
            
        }

        public void Open(WindowType windowType)
        {
            gameObject.SetActive(true);
            _currentWindowType = windowType;
            _header.text = _currentWindowType == WindowType.Buy ? "But items!" : "Sell items!";
            if (_currentWindowType == WindowType.Buy)
            {
                foreach (var item in _itemsData.GetAllItems())
                {
                    var element = _pool.GetFreeElement();
                    int count = item.AvailableAmount - _playerResourcesController.GetItemCount(item.Key);
                    element.Construct(item.Key, item.ItemIcon, count, item.BuyPrice, this);
                }  
            }
            else
            {
                foreach (var item in _playerResourcesController.GetAllItems())
                {
                    if (item.OwnedAmount == 0)
                        continue;
                    
                    var element = _pool.GetFreeElement();
                    int count = _playerResourcesController.GetItemCount(item.Key);
                    var itemData = _itemsData.GetItemByKey(item.Key);
                    element.Construct(item.Key, itemData.ItemIcon, count, itemData.CellPrice, this);
                }
            }

        }


        public void Close()
        {
            _pool.DisableAll();
            gameObject.SetActive(false);
        }

        public void OnElementClick(string key, ShopElement clickedElement)
        {
            var itemData = _itemsData.GetItemByKey(key);
            int newCount;
            if (_currentWindowType == WindowType.Buy)
            {
                if (itemData.BuyPrice > _playerResourcesController.Coins.Value)
                    return;

                _playerResourcesController.Coins.Value -= itemData.BuyPrice;
                _playerResourcesController.ChangeItemValue(itemData.Key, 1);
                newCount = itemData.AvailableAmount - _playerResourcesController.GetItemCount(key);
                Debug.Log($"куплен элемент {key}!");
            }
            else
            {
                if (_playerResourcesController.GetItemCount(key) - 1 < 0)
                    throw new Exception("After selling count of items < 0!");

                _playerResourcesController.Coins.Value += itemData.CellPrice;
                _playerResourcesController.ChangeItemValue(key, -1);
                newCount = _playerResourcesController.GetItemCount(key);
                Debug.Log($"продан элемент {key}!");
            }
            
            clickedElement.UpdateCount(newCount);
        }
    }
}
