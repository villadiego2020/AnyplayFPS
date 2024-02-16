using UnityEngine;

namespace AFPS.Cores
{
    public abstract class IJobState : MonoBehaviour, IInitalize
    {
        public bool IsDone;

        public abstract void Initialize(params object[] objects);
        public abstract void Register();
        public abstract void Unregister();
    }
}