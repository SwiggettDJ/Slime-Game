using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour
{
    protected float size;
    protected float startSize;
    protected Vector3 startPos;

    protected virtual void Start()
    {
        startPos = transform.position;
        startSize = transform.localScale.x;
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
}
