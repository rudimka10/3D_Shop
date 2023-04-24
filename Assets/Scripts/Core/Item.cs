using UnityEngine;

namespace Core
{
    public class Item
    {
        private string _key;
        private int _ownedAmount;

        public string Key => _key;
        public int OwnedAmount => _ownedAmount;
        
        public Item(string key)
        {
            _key = key;
            
            if (!PlayerPrefs.HasKey(_key))
            {
                PlayerPrefs.SetInt(_key, 0);   
            }
            
            _ownedAmount = PlayerPrefs.GetInt(_key);
        }

        public void AddItem(int amount)
        {
            _ownedAmount += amount;
            PlayerPrefs.SetInt(_key, _ownedAmount);
        }
    }
}