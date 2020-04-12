using System;
using UnityEngine;

public class Effects : MonoBehaviour
{
    private ParticleSystem _particle;
    private event Action _finishedPlay;

    public bool IsEffectPlaying => _particle.isPlaying;

    public void Play(Vector2 position)
    {
        transform.position = position;
        _particle = GetComponent<ParticleSystem>();
        _particle.Play();
    }
}
