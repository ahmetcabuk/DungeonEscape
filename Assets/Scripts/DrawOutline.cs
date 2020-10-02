using System.Collections;
using System.Collections.Generic;
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
        if (other.tag == "Player")
        {
            outline.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            outline.enabled = false;
        }
    }
}
