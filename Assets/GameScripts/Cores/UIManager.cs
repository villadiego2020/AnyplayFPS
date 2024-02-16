using AFPS.Commons;
using UnityEngine;

namespace AFPS.UIs
{
    [DefaultExecutionOrder(ScriptOrder.UI_MANAGER)]
    public class UIManager : MonoBehaviour
    {
        public static UIControl UIControl;
        public static UINetwork UINetwork;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            UIControl = GetComponentInChildren<UIControl>();
            UINetwork = GetComponentInChildren<UINetwork>();
        }
    }
}