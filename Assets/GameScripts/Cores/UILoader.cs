using AFPS.Commons;
using UnityEngine;

namespace AFPS.Cores
{
    [DefaultExecutionOrder(ScriptOrder.UI_LOADER)]
    public class UILoader : IJobState
    {
        [SerializeField] private GameObject[] m_UIPrefabs;
        [SerializeField] private Transform m_Base;

        public async override void Initialize(params object[] objects)
        {
            IsDone = false;

            foreach (var ui in m_UIPrefabs)
            {
                GameObject go = Spawner.Create(ui, m_Base);
                go.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
                go.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;

                await new WaitForEndOfFrame();
            }

            IsDone = true;
        }

        public override void Register()
        {

        }

        public override void Unregister()
        {

        }
    }
}