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
        if (size <= 0)
        {
            DeathEvent.Invoke();
        }
        else
        {
            base.UpdateSize();
        }
    }

    private void CompareSize(EntityBehaviour other)
    {
        float otherSize = other.GetSize();
        if (size > otherSize)
        {
            AddSize(otherSize/2);
            slimeAnimator.SetTrigger("Growth");
            other.Death();
        }
        else
        {
            if (slimeAnimator.GetBool("isJumping") || slimeAnimator.GetBool("isFalling") && (transform.position.y - (size / 3) > other.transform.position.y))
            {
                ParticleSystem ps = other.GetComponentInChildren<ParticleSystem>();
                ps.Emit((int)Mathf.Clamp(size*15, 5, Single.PositiveInfinity));
                other.AddSize(-size/2);
            }
            else
            {
                AddSize(-otherSize/4);
                slimeAnimator.SetTrigger("Shrink");
            }
            GetComponent<SlimeMovement>().KnockBack(other);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CompareSize(other.GetComponent<EntityBehaviour>());
    }
}
