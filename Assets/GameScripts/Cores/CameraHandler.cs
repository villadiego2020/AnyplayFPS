using UnityEngine;

namespace AFPS.Cores
{
    public class CameraHandler : MonoBehaviour, IInitalize
    {
        private GameObject m_CameraFollower;

        public void Initialize(params object[] objects)
        {
            m_CameraFollower = (GameObject) objects[0];
        }

        public void Register()
        {

        }

        public void Unregister()
        {

        }
    }
}