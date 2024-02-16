using AFPS.Cores;
using AFPS.UIs;
using UnityEngine;

namespace AFPS.Players
{
    public enum ControlState
    {
        Stand,
        Prone,
        Crounch
    }

    public class PlayerAnimationState : MonoBehaviour, IInitalize
    {
        private const string VELO_X = "velo_x";
        private const string VELO_Z = "velo_z";
        private const string SPEED = "speed";
        private const string ACTION = "action";

        public CharacterController CharacterController { get; private set; }
        public Animator Animator { get; private set; }

        public ControlState State;

        #region IInitalize
        public void Initialize(params object[] objects)
        {
            CharacterController = GetComponent<CharacterController>();
            Animator = GetComponentInChildren<Animator>();
        }

        public void Register()
        {
            UIManager.UIControl.OnStateEvent += SetAnimationState;
        }

        public void Unregister()
        {
            UIManager.UIControl.OnStateEvent -= SetAnimationState;
        }
        #endregion

        #region Animation State
        public void SetDirection(Vector3 direction)
        {
            Animator.SetFloat(VELO_X, direction.x);
            Animator.SetFloat(VELO_Z, direction.z);
        }

        public void SetSpeed(float speed, float motionSmoothTime, float deltaTime)
        {
            Animator.SetFloat(SPEED, speed, motionSmoothTime, deltaTime);
        }

        /// <summary>
        /// Switch Control State 
        /// Stand, Prone, Crounch
        /// </summary>
        /// <param name="state"></param>
        public void SetAnimationState(ControlState state)
        {
            switch (state)
            {
                case ControlState.Prone:
                    Prone();
                    break; 

                case ControlState.Crounch:
                    Crounch();
                    break; 
            }
        }

        private void Prone()
        {
            switch(State)
            {
                case ControlState.Stand:
                case ControlState.Crounch:
                    State = ControlState.Prone;
                    break;
                case ControlState.Prone:
                    State = ControlState.Stand;
                    break;
            }

            Animator.SetInteger(ACTION, (int)State);
        }

        private void Crounch()
        {
            switch (State)
            {
                case ControlState.Stand:
                case ControlState.Prone:
                    State = ControlState.Crounch;
                    break;
                case ControlState.Crounch:
                    State = ControlState.Stand;
                    break;
            }

            Animator.SetInteger(ACTION, (int)State);
        }
        #endregion
    }
}