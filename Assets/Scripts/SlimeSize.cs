using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SlimeSize : MonoBehaviour
{
    private float slimeSize;
    private CharacterController controller;
    private ParticleSystem slimeEffect;

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
    }

    public void AddSize(float delta)
    {
        slimeSize += delta;
        UpdateSize();
    }
}
