using UnityEngine;

[RequireComponent(typeof(BorderOutline))]
public class DrawOutline : MonoBehaviour
{
    private BorderOutline outline;

    void Start()
    {
        outline = GetComponent<BorderOutline>();
        outline.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RayCast")
        {
            outline.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "RayCast")
        {
            outline.enabled = false;
        }
    }
}
