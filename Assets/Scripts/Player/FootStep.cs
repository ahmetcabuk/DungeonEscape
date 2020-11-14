using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStep : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource audioSource;
    private bool onStair = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (rb.velocity.magnitude > 1f && audioSource.isPlaying == false)
        {
            audioSource.volume = Random.Range(0.8f, 1f);
            audioSource.pitch = Random.Range(1.4f, 1.8f);
            audioSource.Play();
        }

        else if (onStair && rb.velocity.magnitude > 0.5f && audioSource.isPlaying == false)
        {
            audioSource.volume = Random.Range(0.8f, 1f);
            audioSource.pitch = Random.Range(2.4f, 2.8f);
            audioSource.Play();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stair"))
        {
            onStair = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stair"))
        {
            onStair = false;
        }
    }
}
