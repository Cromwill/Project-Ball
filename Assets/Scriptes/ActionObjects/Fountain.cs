using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : ActionObject
{
    [SerializeField] private float _cicleTime;
    private ParticleSystem _particle;
    private bool IsUsing;
    private Coroutine _particlePlayCorutine;

    private void Start()
    {
        _particle = GetComponentInChildren<ParticleSystem>();
        IsUsing = true;
        _particlePlayCorutine = StartCoroutine(ParticlePlay());
    }

    private IEnumerator ParticlePlay()
    {
        while (IsUsing)
        {
            _particle.Play();
            yield return new WaitForSeconds(_cicleTime);
        }
    }
}
