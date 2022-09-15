using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomPitchSetter : MonoBehaviour
{
    AudioSource _audioSource;
    [SerializeField] float minPitch;
    [SerializeField] float maxPitch;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        SetPitch();
    }

    void SetPitch()
    {
        var pitch = Random.Range(minPitch, maxPitch);
        _audioSource.pitch = pitch;
    }
}
