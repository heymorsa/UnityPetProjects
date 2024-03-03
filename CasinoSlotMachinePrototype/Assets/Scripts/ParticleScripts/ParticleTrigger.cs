using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(ParticleAutoClose());
    }

    IEnumerator ParticleAutoClose()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
    
}
