using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    private ParticleSystem ps;

    private ParticleHandler ph;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        ph = GetComponentInChildren<ParticleHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        ps.Emit(10);
        StartCoroutine(ph.EnableForces());
        GetComponent<SphereCollider>().enabled = false;
    }
}
