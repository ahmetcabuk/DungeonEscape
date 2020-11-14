using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearthbeat : MonoBehaviour
{
    public CapsuleCollider playerCollider;
    //public List<BoxCollider> tensionAreasList = new List<BoxCollider>();
    //public BoxCollider tensionArea;

    private AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider)
        {
            //audioSource.volume = Random.Range(0.80f, 1f);
            //audioSource.pitch = Random.Range(0.90f, 1f);
            audioSource.loop = true;
            audioSource.Play();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == playerCollider)
        {
            audioSource.Stop();
        }
    }

}
