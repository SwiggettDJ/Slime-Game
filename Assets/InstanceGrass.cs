using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InstanceGrass : MonoBehaviour
{
    private ParticleSystem ps;
    public GameObject prefab;
    public GameObject parent;
    //private GameObject empty;
    private ParticleSystem.TriggerModule triggerModule;
    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        triggerModule = ps.trigger;
    }

    private void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle par = enter[i];
            GameObject grass = Instantiate(prefab, par.position, Quaternion.Euler(par.rotation3D), parent.transform);
            grass.transform.rotation = Quaternion.LookRotation(par.position - transform.position);
            //grass.transform.rotation = Quaternion.Euler(0,0 , grass.transform.eulerAngles.z);
            
        }
    }
}
