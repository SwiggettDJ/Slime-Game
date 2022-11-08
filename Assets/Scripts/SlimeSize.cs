using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class SlimeSize : EntityBehaviour
{
    private CharacterController controller;
    private ParticleSystem slimeEffect;
    private Animator slimeAnimator;

    public UnityEvent DeathEvent;

    protected override void Start()
    {
        base.Start();
        controller = GetComponent<CharacterController>();
        slimeEffect = GetComponentInChildren<ParticleSystem>();
        slimeAnimator = GetComponentInChildren<Animator>();
        ResetSize();
    }

    protected override void UpdateSize()
    {
        base.UpdateSize();
        if (size <= 0)
        {
            DeathEvent.Invoke();
        }
    }

    private void CompareSize(EntityBehaviour other)
    {
        float otherSize = other.GetSize();
        if (size > otherSize)
        {
            AddSize(otherSize/2);
            slimeAnimator.SetTrigger("Growth");
            //other.Death();
        }
        else
        {
            AddSize(-otherSize/4);
            slimeAnimator.SetTrigger("Shrink");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CompareSize(other.GetComponent<EntityBehaviour>());
    }
}
