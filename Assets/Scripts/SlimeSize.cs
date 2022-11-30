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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemySize>())
        {
            EnemySize enemy = other.GetComponent<EnemySize>();
            float enemySize = enemy.GetSize();
            if (CompareSize(enemy))
            {
                enemy.Death();
            }
            else
            {
                if (slimeAnimator.GetBool("isJumping") || slimeAnimator.GetBool("isFalling") && (transform.position.y > enemy.transform.position.y))
                {
                    enemy.Damaged();
                }
                else
                {
                    AddSize(-enemySize/4);
                    slimeAnimator.SetTrigger("Shrink");
                }
                GetComponent<SlimeMovement>().KnockBack(enemy);
            }
        }
    }
}
