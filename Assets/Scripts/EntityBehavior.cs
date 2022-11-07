using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehavior : MonoBehaviour
{
    protected float size;
    protected float startSize = 1;
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
}
