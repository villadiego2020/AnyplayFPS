using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform Target;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Target);
    }
}
