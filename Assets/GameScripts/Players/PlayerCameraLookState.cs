using AFPS.Cores;
using FishNet.Object;
using UnityEngine;

namespace AFPS.Players
{
    [System.Serializable]
    public struct CameraLook
    {
        public bool InvertY;
        public bool InvertX;
        public float Sensivity;
        public float MaxAngle;
        public float MinAngle;
        public float Radius;
        public float RotateFollowCameraSpeed;
    }

    public class PlayerCameraLookState : NetworkBehaviour, IInitalize
    {
        [Header("Refference")]
        [SerializeField] private Transform m_Root;
        [SerializeField] private GameObject m_Camera;

        [Space(5)]
        [Header("Config")]
        [SerializeField] private CameraLook m_Setting;

        [Space(5)]
        [Header("Runtimes")]
        [SerializeField] private float m_RotationX;
        [SerializeField] private float m_RotationY;

        private void Update()
        {
            if (IsOwner == false)
                return;

            ApplyLook();
            ApplyLookModel();
        }

        #region IInitalize
        public void Initialize(params object[] objects)
        {
            m_Camera = (GameObject) objects[0];
        }

        public void Register()
        {

        }

        public void Unregister()
        {

        }
        #endregion

        #region Rotate Camera to look view

        private void ApplyLook()
        {
            m_RotationX += PlayerInputAction.Instance.LookInput.y * m_Setting.Sensivity * Time.deltaTime;
            m_RotationX = Mathf.Clamp(m_RotationX, m_Setting.MinAngle, m_Setting.MaxAngle);
            m_RotationY += PlayerInputAction.Instance.LookInput.x * m_Setting.Sensivity * Time.deltaTime;

            float x = (m_Setting.InvertY ? -m_RotationX : m_RotationX);
            float y = (m_Setting.InvertX ? -m_RotationY : m_RotationY);

            Quaternion targetRotation = Quaternion.Euler(x, y, 0);
            m_Camera.transform.position = m_Root.transform.position - targetRotation * new Vector3(0f, 0f, m_Setting.Radius);
            m_Camera.transform.rotation = targetRotation;
        }

        private void ApplyLookModel()
        {
            
            m_Root.transform.rotation = Quaternion.Euler(0, m_Camera.transform.eulerAngles.y, 0);
        }

        #endregion
    }
}