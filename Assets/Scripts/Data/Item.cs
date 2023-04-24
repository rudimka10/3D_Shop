using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class Item
    {
        [SerializeField] private string _key;
        [SerializeField] private int _availableAmount;
        [SerializeField] private int _buyPrice;
        [SerializeField] private int _cellPrice;
        [SerializeField] private Sprite _itemIcon;
        
        public string Key => _key;
        public int AvailableAmount => _availableAmount;
        public int BuyPrice => _buyPrice;
        public int CellPrice => _cellPrice;
        public Sprite ItemIcon => _itemIcon;
        
        
        
    }
}
