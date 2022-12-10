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
    private ParticleSystem.TriggerModule triggerModule;
    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        externalForcesModule = ps.externalForces;
        triggerModule = ps.trigger;
        externalForcesModule.enabled = false;
        triggerModule.enabled = false;
    }

    private void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        
        for (int i = 0; i < numEnter; i++)
        {
            ParticleTriggerEvent.Invoke();
        }
    }

    public IEnumerator EnableForces()
    {
        yield return new WaitForSeconds(.3f);
        if (this)
        {
            externalForcesModule.enabled = true;
            triggerModule.enabled = true;
        }
    }
}
