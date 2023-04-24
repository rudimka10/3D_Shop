using Core;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class CoinsValueUpdater : MonoBehaviour
    {
        [Inject] private PlayerResourcesController _playerResourcesController;
        [SerializeField] private TMP_Text _valueText;
        
        private void OnEnable()
        {
            _playerResourcesController.Coins.ValueChanged += UpdateContent;
            UpdateContent();
        }

        private void UpdateContent()
        {
            _valueText.text = _playerResourcesController.Coins.ToString();
        }

        private void OnDisable()
        {
            _playerResourcesController.Coins.ValueChanged -= UpdateContent;
        }
    }
}
