using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class SlimeSize : EntityBehavior
{
    private CharacterController controller;
    private ParticleSystem slimeEffect;

    public UnityEvent DeathEvent;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        slimeEffect = GetComponentInChildren<ParticleSystem>();
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

    private void CompareSize(EntityBehavior other)
    {
        float otherSize = other.GetSize();
        if (size > otherSize)
        {
            AddSize(otherSize/2);
            print("Added " + otherSize + "\n");
        }
        else
        {
            print("too big \n");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CompareSize(other.GetComponent<EntityBehavior>());
    }
}
