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
        if (delta > 0)
        {
            delta*=growthMultiplier;
        }
        size += delta;
        UpdateSize();
    }
    public void AddSize(FloatData delta)
    {
        size += delta.value;
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
