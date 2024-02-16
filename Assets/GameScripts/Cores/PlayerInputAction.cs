using AFPS.Commons;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AFPS.Cores
{
    [DefaultExecutionOrder(ScriptOrder.PLAYER_INPUT_ACTION)]
    public class PlayerInputAction : MonoBehaviour
    {
        public static PlayerInputAction Instance;
        [SerializeField] private InputActionAsset m_InputAsset;

        public bool InvertCoordY { get; private set; } = true;
        public Vector2 MoveInput { get; private set; } = Vector2.zero;
        public Vector2 LookInput { get; private set; } = Vector2.zero;

        public bool IsMoving { get; private set; } = false;

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            m_InputAsset["Move"].performed += SetMove;
            m_InputAsset["Move"].canceled += SetMove;

            m_InputAsset["Look"].performed += SetLook;
            m_InputAsset["Look"].canceled += SetLook;
        }

        private void OnDisable()
        {
            m_InputAsset["Move"].performed -= SetMove;
            m_InputAsset["Move"].canceled -= SetMove;

            m_InputAsset["Look"].performed -= SetLook;
            m_InputAsset["Look"].canceled -= SetLook;
        }

        private void SetMove(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
            IsMoving = MoveInput != Vector2.zero;
        }

        private void SetLook(InputAction.CallbackContext context)
        {
            LookInput = context.ReadValue<Vector2>();
        }
    }
}