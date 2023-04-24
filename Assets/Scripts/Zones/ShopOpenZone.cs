using StarterAssets;
using UI.Shop;
using UnityEngine;
using Zenject;

namespace Zones
{
    public class ShopOpenZone : MonoBehaviour
    {
        [Inject] private ShopWindow _shopWindow;
#if !UNITY_IOS && !UNITY_ANDROID
        [Inject] private StarterAssetsInputs _inputs;
#endif
        [SerializeField] private WindowType _windowType;
        
        private void OnTriggerEnter(Collider other)
        {
            _shopWindow.Open(_windowType);
#if !UNITY_IOS && !UNITY_ANDROID
            _inputs.cursorLocked.Value = false;
            _inputs.cursorInputForLook = false;
#endif
        }

        private void OnTriggerExit(Collider other)
        {          
            _shopWindow.Close();
#if !UNITY_IOS && !UNITY_ANDROID
            _inputs.cursorLocked.Value = true;
            _inputs.cursorInputForLook = true;
#endif
        }
    }
}
