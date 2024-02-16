using UnityEngine;

namespace AFPS.Cores
{
    public class Spawner : MonoBehaviour
    {
        public static GameObject Create(GameObject prefab, Transform parent)
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.SetParent(parent);
            obj.transform.position = Vector3.zero;
            obj.transform.localScale = Vector3.one;

            return obj;
        }
    }
}