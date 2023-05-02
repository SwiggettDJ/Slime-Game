using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour
{
    protected float size;
    protected float startSize;
    protected Vector3 startPos;
    private Animator entityAnimator;
    protected float growthMultiplier = 1f;

    protected virtual void Start()
    {
        startPos = transform.position;
        size = startSize = transform.localScale.x;
        entityAnimator = GetComponentInChildren<Animator>();
    }

    public void ResetSize()
    {
        size = startSize;
        UpdateSize();
    }
    protected virtual void UpdateSize()
    {
        transform.localScale = new Vector3(size, size, size);
    }
    public void AddSize(float delta)
    {
        float final = delta;
        if (delta > 0)
        {
            final *= growthMultiplier;
        }
        size += final;
        UpdateSize();
    }
    public void AddSize(FloatData delta)
    {
        float final = delta.value;
        if (delta.value > 0)
        {
            final *= growthMultiplier;
        }
        size += final;
        UpdateSize();
    }

    public float GetSize()
    {
        return size;
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void FullReset()
    {
        GetComponent<CharacterController>().transform.position = startPos;
        ResetSize();
    }
    
    protected bool CompareSize(EntityBehaviour other)
    {
        if (size > other.GetSize())
        {
            AddSize(other.size/2);
            entityAnimator.SetTrigger("Growth");
            return true;
        }
        else return false;
    }
}
