using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource m_MyAudioSource;
    bool m_Play;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] AudioClip mainEngine;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m_MyAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            m_Play = true;
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            PlayThrustSound(m_Play);
        } else {
            m_Play = false;
            PlayThrustSound(m_Play);

        }

    }

    void ProcessRotation() {

        if (Input.GetKey(KeyCode.A)) {
            ApplyRotation(rotationSpeed);
        } else if (Input.GetKey(KeyCode.D)) {
            ApplyRotation(-rotationSpeed);
        }
    }

    void ApplyRotation(float rotationThisFrame) {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }

    void PlayThrustSound(bool m_Play) {
        if (m_Play) {
            if (!m_MyAudioSource.isPlaying) {
                m_MyAudioSource.PlayOneShot(mainEngine);
            }
        } else {
            m_MyAudioSource.Stop();
        }
    }
}
