using AFPS.Cores;
using FishNet.Object;
using UnityEngine;

namespace AFPS.Players
{
    [System.Serializable]
    public struct Movement
    {
        public MovemtAction[] MovemtActions; //  when try to switch action (stand, prone, crounch)
        public float MotionSmoothTime; // Blend animation 
        public float Acceleration; // Increase adition move speed 
    }

    [System.Serializable]
    public struct MovemtAction
    {
        public ControlState State;
        public float MoveSpeed;
    }

    public class PlayerMovementState : NetworkBehaviour, IInitalize
    {
        [Header("Refference")]
        [SerializeField] private GameObject m_Target;

        [Space(5)]
        [Header("Setting")]
        [SerializeField] private Movement m_Setting;

        [Space(5)]
        [Header("Runtime")]
        [SerializeField] private float m_CurrentSpeed;

        private PlayerAnimationState m_Animation;
        private CharacterController m_Controller;


        #region IInitalize
        public void Initialize(params object[] objects)
        {
            m_Controller = GetComponent<CharacterController>();
            m_Animation = GetComponent<PlayerAnimationState>();

            m_Target = (GameObject) objects[0];
        }

        private void Update()
        {
            ApplyMove();
        }

        public void Register()
        {

        }

        public void Unregister()
        {

        }
        #endregion

        #region Character Movement 

        private void ApplyMove()
        {
            if (base.IsOwner == false)
            {
                return;
            }

            float speed = m_Setting.MovemtActions[(int)m_Animation.State].MoveSpeed;
            Vector2 joystickPosition = PlayerInputAction.Instance.MoveInput;

            if (PlayerInputAction.Instance.IsMoving == false)
            {
                m_CurrentSpeed -= m_Setting.Acceleration * Time.deltaTime;

                if (m_CurrentSpeed < 0)
                    m_CurrentSpeed = 0;

                ApplyAnimation(joystickPosition, m_CurrentSpeed);
                return;
            }

            m_CurrentSpeed += m_Setting.Acceleration * Time.deltaTime;

            if (m_CurrentSpeed > speed)
                m_CurrentSpeed = speed;

            Vector3 direction = new Vector3(joystickPosition.x, 0, joystickPosition.y);
            Vector3 characterSpeedModifier = direction * m_CurrentSpeed * Time.deltaTime;

            Vector3 transformedDirection = m_Target.transform.TransformDirection(characterSpeedModifier);
            transformedDirection.y = 0f; 

            m_Controller.Move(transformedDirection);

            float animationSpeedModifier = m_CurrentSpeed / speed; // m_Controller.velocity.sqrMagnitude / speed;
            ApplyAnimation(direction, animationSpeedModifier);
        }

        private void ApplyAnimation(Vector3 direction, float animationSpeedModifier)
        {
            m_Animation.SetDirection(direction);
            m_Animation.SetSpeed(animationSpeedModifier, m_Setting.MotionSmoothTime * Time.deltaTime, Time.deltaTime);
        }

        #endregion
    }
}