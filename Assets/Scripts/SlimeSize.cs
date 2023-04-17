using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class SlimeSize : EntityBehaviour
{
    private Animator slimeAnimator;
    public UnityEvent DeathEvent;
    private bool greenMutation;
    private bool regenerating;
    private float maxGrowth;

    private float lastSize;


    protected override void Start()
    {
        base.Start();
        slimeAnimator = GetComponentInChildren<Animator>();
        ResetSize();
        maxGrowth = size;
        lastSize = size;
    }

    protected override void UpdateSize()
    {
        if (size > maxGrowth)
            maxGrowth = size;
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
                if ((slimeAnimator.GetBool("isJumping") || slimeAnimator.GetBool("isFalling")) && transform.position.y > enemy.transform.position.y)
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

        if (other.GetComponent<Jelly>())
        {
            if (other.GetComponent<Jelly>().gameObject.name == "Blue Jelly")
            {
                growthMultiplier += .25f;
            }
            if (other.GetComponent<Jelly>().gameObject.name == "Green Jelly")
            {
                greenMutation = true;
            }
        }

        if (other.CompareTag("Death"))
        {
            DeathEvent.Invoke();
        }
    }

    private void Update()
    {
        float diff = maxGrowth - size;
        if (diff > .05f)
        {
            maxGrowth += .05f - diff;
        }
        
        if (regenerating && size < maxGrowth)
        {
            AddSize(Time.deltaTime/10);
        }
        else regenerating = false;

    }

    public void Regenerate(bool condition)
    {
        if (greenMutation)
        {
            regenerating = condition;
        }
    }
}
