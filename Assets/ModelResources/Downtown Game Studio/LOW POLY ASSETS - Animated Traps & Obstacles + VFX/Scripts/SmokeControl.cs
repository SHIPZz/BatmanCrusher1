using UnityEngine;
using UnityEngine.VFX;

public class SmokeControl : MonoBehaviour
{
    private void OnTriggerEnter(Collider trig)
    {
        GetComponent<VisualEffect>().Play();
    }
}