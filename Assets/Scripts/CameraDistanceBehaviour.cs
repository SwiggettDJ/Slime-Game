using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraDistanceBehaviour : MonoBehaviour
{
    private CinemachineVirtualCamera cam;

    private float targetSize;

    private CinemachineComponentBase componentBase;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        componentBase = cam.GetCinemachineComponent(CinemachineCore.Stage.Body);
    }

    // Update is called once per frame
    void Update()
    {
        CinemachineFramingTransposer transposer = componentBase as CinemachineFramingTransposer;
        targetSize = cam.Follow.localScale.x;
        float remappedSize = Remap(targetSize, 0, 5, 2, 16);
        transposer.m_CameraDistance = Mathf.Lerp(transposer.m_CameraDistance, remappedSize, .5f);
        //transposer.m_TrackedObjectOffset.y = Remap(targetSize, 0,10,0,-0.96f);
    }
    
    private float Remap(float value, float inMin, float inMax, float outMin, float outMax)
    {
        float inversed = Mathf.InverseLerp(inMin, inMax, value);
        return Mathf.Lerp(outMin, outMax, inversed);
    }

}
