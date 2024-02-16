using System.Collections;
using UnityEngine;

namespace AFPS.Utils
{
    public class CoroutineWithData
    {
        public Coroutine Coroutine { get; private set; }
        public object Result;
        private IEnumerator Target;

        public CoroutineWithData(IEnumerator target, MonoBehaviour self)
        {
            Target = target;
            Coroutine = self.StartCoroutine(Run());
        }

        private IEnumerator Run()
        {
            while (Target.MoveNext())
            {
                Result = Target.Current;
                yield return Result;
            }
        }
    }
}