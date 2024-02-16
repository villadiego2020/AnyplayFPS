using AFPS.Players;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AFPS.UIs
{
    public class UINetwork : MonoBehaviour
    {
        [SerializeField] private Button m_HostButton;
        [SerializeField] private Button m_ClientButton;

        public Action OnHostClickEvent;
        public Action OnClientClickEvent;

        void Start()
        {
            m_HostButton.onClick.AddListener(HostSelect);
            m_ClientButton.onClick.AddListener(ClientSelect);
        }

        private void HostSelect()
        {
            OnHostClickEvent?.Invoke();
            OnClientClickEvent?.Invoke();
            gameObject.SetActive(false);
        }

        private void ClientSelect()
        {
            OnClientClickEvent?.Invoke();
            gameObject.SetActive(false);
        }
    }
}