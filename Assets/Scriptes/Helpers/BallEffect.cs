using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEffect : Effects
{
    private void Update()
    {
        if(!IsEffectPlaying)
        {
            Destroy(gameObject);
        }
    }
}
