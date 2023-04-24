using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    public class ShopElement : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _countLabel;
        [SerializeField] private TMP_Text _buyLabel;
        [SerializeField] private Button _button;
        private string _key;
        private IClickListener _clickListener;
        
        public void Construct(string key, Sprite icon, int count, int price, IClickListener clickListener)
        {
            _key = key;
            _icon.sprite = icon;
            _buyLabel.text = price.ToString();
            _clickListener = clickListener;
            UpdateCount(count);
        }

        public void UpdateCount(int count)
        {
            _countLabel.text = count.ToString();
            _button.interactable = count != 0;
        }
        
        public void OnButtonClick()
        {
            _clickListener.OnElementClick(_key, this);
        }
        
    }
}
