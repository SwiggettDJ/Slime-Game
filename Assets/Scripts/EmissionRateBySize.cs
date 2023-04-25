using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionRateBySize : MonoBehaviour
{
    private float density = 5f;
    void Awake()
    {
        ParticleSystem system = GetComponent<ParticleSystem>();
        var emission = system.emission;
        emission.rateOverTime = system.shape.scale.x * density;
    }
    
}
