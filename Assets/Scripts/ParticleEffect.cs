using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ParticleEffect : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }
    public void Play(Vector3 _playAtPosition)
    {
        Vector3 spawnPosition = _playAtPosition;
        gameObject.transform.position = spawnPosition;
        gameObject.SetActive(true);
        particleSystem.Play();
        Invoke("Deactivate", 1f);
    }
    public void Deactivate()
    {
        gameObject.transform.position = Vector3.zero;
        particleSystem.Stop();
        gameObject.SetActive(false);
    }
}
