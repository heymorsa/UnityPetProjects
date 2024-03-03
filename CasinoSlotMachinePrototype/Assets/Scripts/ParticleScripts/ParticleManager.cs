using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;

    [SerializeField] private List<ParticleSystem> destroyingSlotsParticle;
    [SerializeField] private ParticleSystem buildingStarParticle;
    private int destroyingSlotsParticleIndex = 0,maxDestroyingSlotsCount;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        maxDestroyingSlotsCount = destroyingSlotsParticle.Count;
    }

    public void SpawnDestroySlotParticle(Vector3 position)
    {
        destroyingSlotsParticle[destroyingSlotsParticleIndex].transform.position = position;
        destroyingSlotsParticle[destroyingSlotsParticleIndex].gameObject.SetActive(true);
        destroyingSlotsParticleIndex++;
        if (maxDestroyingSlotsCount == destroyingSlotsParticleIndex)
            destroyingSlotsParticleIndex = 0;
    }

    public void SpawnBuildingParticle(Vector3 position)
    {
        buildingStarParticle.transform.position = position;
        buildingStarParticle.Play();
    }
}
