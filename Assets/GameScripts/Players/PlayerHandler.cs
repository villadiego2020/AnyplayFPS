using AFPS.Commons;
using AFPS.Cores;
using AFPS.UIs;
using Assets.GameScripts.Players;
using UnityEngine;

namespace AFPS.Players
{
    [DefaultExecutionOrder(ScriptOrder.PLAYER_CONTROLER)]
    public class PlayerHandler : MonoBehaviour, IInitalize
    {
        public static PlayerHandler Instance;

        [HideInInspector] public PlayerBehavior Player;

        public IJobState[] JobStates;
        [HideInInspector] public GameplayNetwork GameplayNetwork;
        [HideInInspector] public CameraHandler CameraHandler;

        [SerializeField] private GameObject m_CameraFollowerPrefab;

        private async void Awake()
        {
            foreach (var job in JobStates)
            {
                job.Initialize();
                await new WaitUntil(() => job.IsDone);
                await new WaitForEndOfFrame();
            }

            GameplayNetwork = new GameObject().AddComponent<GameplayNetwork>();
            GameplayNetwork.transform.parent = transform;
            GameplayNetwork.Initialize();
            GameplayNetwork.name = nameof(GameplayNetwork);
            await new WaitForEndOfFrame();

            CameraHandler = new GameObject().AddComponent<CameraHandler>();
            CameraHandler.transform.parent = transform;
            CameraHandler.name = nameof(CameraHandler);
            await new WaitForEndOfFrame();

            UIManager.UINetwork.OnHostClickEvent = GameplayNetwork.OnClickServer;
            UIManager.UINetwork.OnClientClickEvent = GameplayNetwork.OnClickClient;
            await new WaitForEndOfFrame();


            Instance = this;
        }

        #region IInitalize
        public void Initialize(params object[] objects)
        {
            GameObject cameraFollower = Instantiate(m_CameraFollowerPrefab);

            Player = (PlayerBehavior)objects[0];
            Player.Initialize(cameraFollower);

            CameraHandler.Initialize(cameraFollower);

            Register();
        }

        public void Register()
        {
        }

        public void Unregister()
        {

        }
        #endregion


    }
}