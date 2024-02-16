using AFPS.Players;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AFPS.UIs
{
    public class UIControl : MonoBehaviour
    {
        [SerializeField] private Button m_ProneButton;
        [SerializeField] private Button m_CrounchButton;

        public Action<ControlState> OnStateEvent;

        void Start ()
        {
            m_ProneButton.onClick.AddListener(ProneSelect);
            m_CrounchButton.onClick.AddListener(CrounchSelect);
        }

        private void ProneSelect()
        {
            OnStateEvent?.Invoke(ControlState.Prone);
        }

        private void CrounchSelect()
        {
            OnStateEvent?.Invoke(ControlState.Crounch);
        }
    }
}