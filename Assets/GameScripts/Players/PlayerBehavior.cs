using AFPS.Cores;
using AFPS.Players;
using FishNet.Object;

namespace Assets.GameScripts.Players
{
    public class PlayerBehavior : NetworkBehaviour, IInitalize
    {
        public PlayerAnimationState PlayerAnimationState;
        public PlayerCameraLookState PlayerCameraLookState;
        public PlayerMovementState PlayerMovementState;

        #region IInitalize
        public void Initialize(params object[] objects)
        {
            PlayerAnimationState = GetComponent<PlayerAnimationState>();
            PlayerCameraLookState = GetComponent<PlayerCameraLookState>();
            PlayerMovementState = GetComponent<PlayerMovementState>();

            PlayerAnimationState.Initialize();
            PlayerCameraLookState.Initialize(objects[0]);
            PlayerMovementState.Initialize(objects[0]);

            Register();
        }

        public void Register()
        {
            PlayerAnimationState.Register();
            PlayerCameraLookState.Register();
            PlayerMovementState.Register();
        }

        public void Unregister()
        {
            PlayerAnimationState.Unregister();
            PlayerCameraLookState.Unregister();
            PlayerMovementState.Unregister();
        }
        #endregion

        public override void OnStartClient()
        {
            base.OnStartClient();
        }

        public override void OnStartNetwork()
        {
            base.OnStartNetwork();

            if (base.Owner.IsLocalClient)
            {
                PlayerHandler.Instance.Initialize(this);
            }
        }
    }
}