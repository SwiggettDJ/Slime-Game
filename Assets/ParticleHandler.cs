using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParticleHandler : MonoBehaviour
{
    private ParticleSystem ps;
    public UnityEvent ParticleTriggerEvent;
    private ParticleSystem.ExternalForcesModule externalForcesModule;
    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        externalForcesModule = ps.externalForces;
        externalForcesModule.enabled = false;
    }

    private void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        // iterate through the particles which entered the trigger and make them red
        for (int i = 0; i < numEnter; i++)
        {
            ParticleTriggerEvent.Invoke();
        }
    }

    public IEnumerator EnableForces()
    {
        yield return new WaitForSeconds(.5f);
        if (this)
        {
            externalForcesModule.enabled = true;
        }
    }
}
