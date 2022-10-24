using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class SlimeSize : MonoBehaviour
{
    private float slimeSize;
    private CharacterController controller;
    private ParticleSystem slimeEffect;

    public UnityEvent DeathEvent;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        slimeEffect = GetComponentInChildren<ParticleSystem>();
        ResetSlimeSize();
    }

    public void ResetSlimeSize()
    {
        slimeSize = 1;
        UpdateSize();
    }

    private void UpdateSize()
    {
        transform.localScale = new Vector3(slimeSize, slimeSize, slimeSize);
        if (slimeSize <= 0)
        {
            DeathEvent.Invoke();
        }
    }

    public void AddSize(float delta)
    {
        slimeSize += delta;
        
        UpdateSize();
    }
    public void AddSize(FloatData delta)
    {
        slimeSize += delta.value;
        UpdateSize();
    }
}
