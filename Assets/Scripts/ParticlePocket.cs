using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePocket : MonoBehaviour
{
    private ParticleSystem [] psArray;
    private ParticleSystem explodeEffect, showEffect;

    public int amount = 10;
    // Start is called before the first frame update
    void Start()
    {
        psArray = GetComponentsInChildren<ParticleSystem>();
        explodeEffect = psArray[0];
        showEffect = psArray[1];
    }

    private void OnTriggerEnter(Collider other)
    {
        explodeEffect.Emit(amount);
        StartCoroutine(explodeEffect.GetComponent<ParticleHandler>().EnableForces());
        GetComponent<SphereCollider>().enabled = false;
        showEffect.Stop();
    }
}
