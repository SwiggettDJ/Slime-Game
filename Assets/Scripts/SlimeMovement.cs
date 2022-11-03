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

    void Update()
    {
        currentPos = transform.position.x;
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        float size = playerController.height;
        
        if (playerController.isGrounded)
        {
            direction.y = 0;
        }

        if (horizontal < 0) FacingLeftEvent.Invoke();
        else if(horizontal > 0) FacingRightEvent.Invoke();
        
        if (Mathf.Abs(horizontal) >= 0.1f)
        {
            direction.x = horizontal * speed;
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
            direction.y = jumpStrength * size;
            //Take a chunk of size off for jumping
            JumpEvent.Invoke();
        }

        if (!playerController.isGrounded)
        {
            direction.y += gravity * Mathf.Clamp(size, 0.2f, 1.5f) * Time.deltaTime;
            direction.y = Mathf.Clamp(direction.y, maxGravity, Single.PositiveInfinity);
        }
        playerController.Move(direction * Time.deltaTime);
        
        
        lastPos = currentPos;
    }
}
