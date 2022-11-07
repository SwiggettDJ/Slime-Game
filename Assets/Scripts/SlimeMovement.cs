using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class SlimeMovement : MonoBehaviour
{
    private CharacterController playerController;
    public Joystick joystick;
    public float speed = 6f;
    public float gravity = -10f;
    private float slimeLossMultiplier;
    private float maxGravity = -15f;
    public float jumpStrength = 5f;
    private float jumpThreshold = 0.2f;
    private Vector3 direction;
    private Animator slimeAnimator;
    private bool isFalling = false;
    private float airTime = 0f;
    private float fallThreshhold = 0.2f;
    private bool isJumping = false;

    private float lastPos;
    private float currentPos;
    public FloatData distanceCovered;

    public UnityEvent MovementEvent, FacingRightEvent, FacingLeftEvent, JumpEvent;

    private void Start()
    {
        slimeLossMultiplier = 0.02f;
        playerController = GetComponent<CharacterController>();
        lastPos = currentPos = transform.position.x;
        distanceCovered.value = 0;
        slimeAnimator = GetComponentInChildren<Animator>();
    }

    //Why does mathF not have a remap??
    private float Remap(float value, float inMin, float inMax, float outMin, float outMax)
    {
        float inversed = Mathf.InverseLerp(inMin, inMax, value);
        return Mathf.Lerp(outMin, outMax, inversed);
    }

    void Update()
    {
        currentPos = transform.position.x;
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        float size = transform.localScale.y;
        
        float remappedSize = Remap(size, 0f, 2f, .5f, 1.5f);

        if (playerController.isGrounded)
        {
            if (isFalling)
            {
                slimeAnimator.SetTrigger("Landed");
            }
            isFalling = false;
            isJumping = false;
            direction.y = 0;
            airTime = 0f;
        }

        if (horizontal < 0) FacingLeftEvent.Invoke();
        else if(horizontal > 0) FacingRightEvent.Invoke();
        
        if (Mathf.Abs(horizontal) >= 0.1f)
        {
            direction.x = horizontal * speed * remappedSize;
            distanceCovered.value = -slimeLossMultiplier * Mathf.Abs(currentPos - lastPos);
            MovementEvent.Invoke();
            slimeAnimator.SetBool("isWalking", true);
        }
        else
        {
            direction.x = 0;
            slimeAnimator.SetBool("isWalking", false);
        }
        if (vertical >= jumpThreshold && playerController.isGrounded)
        {
            //jump strength should be proportional to size of slime
            direction.y = jumpStrength * remappedSize;
            //Take a chunk of size off for jumping
            JumpEvent.Invoke();
            isJumping = true;
        }

        if (!playerController.isGrounded)
        {
            airTime += Time.deltaTime;
            direction.y += gravity *remappedSize * Time.deltaTime;
            direction.y = Mathf.Clamp(direction.y, maxGravity, Single.PositiveInfinity);
        }
        //make sure we've fallen at least a little
        if (airTime >= fallThreshhold && direction.y <= 0)
        {
            isFalling = true;
        }

        if (direction.y <= 0)
        {
            isJumping = false;
        }
        slimeAnimator.SetBool("isJumping", isJumping);
        slimeAnimator.SetBool("isFalling", isFalling);
        playerController.Move(direction * Time.deltaTime);
        
        
        
        lastPos = currentPos;
    }
}
