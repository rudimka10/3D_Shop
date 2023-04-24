using UnityEngine;

namespace Control
{
    public class MobileControlDisabler : MonoBehaviour
    {
#if !UNITY_IOS && !UNITY_ANDROID

        private void Start()
        {
            Destroy(gameObject);
        }

#endif
    }
}
