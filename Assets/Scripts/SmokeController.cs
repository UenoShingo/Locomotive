using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SmokeController : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private MainModule mainModule;
    private EmissionModule emissionModule;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        mainModule = particleSystem.main;
        emissionModule = particleSystem.emission;
    }

    public void SmokeUp()
    {
        Debug.Log("smoke");
        mainModule.startSpeed = 5f;
        emissionModule.rateOverTime = 20;
    }

    public void SmokeDown()
    {
        Debug.Log("smokeDown");
        mainModule.startSpeed = 1f;
        emissionModule.rateOverTime = 6;
    }
}