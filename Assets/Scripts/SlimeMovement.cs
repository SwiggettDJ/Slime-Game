using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class SlimeMovement : MonoBehaviour
{
    private CharacterController playerController;
    public Joystick joystick;

    public float speed = 6f;
    public float gravity = -10f;
    private float maxGravity = -15f;
    public float jumpDefault = 5f;
    private float jumpThreshold = 0.2f;
    private Vector3 direction;
    private Animator slimeAnimator;
    //start is as true so when you spawn in he slooshes
    private bool isFalling = true;
    private float airTime;
    private float fallThreshhold = 0.15f;
    private bool isJumping;
    private float remappedSize;
    
    private float knockBackAmount = 5f;
    private float knockBackTracker;
    private float knockBackDirection;
    public bool jumpButton { get; set; }

    private float lastPos;
    private float currentPos;
    public FloatData distanceCovered;

    public UnityEvent MovementEvent, FacingRightEvent, FacingLeftEvent, JumpEvent;

    private WaitForSeconds resizeDelay;
    
    private void Start()
    {
        playerController = GetComponent<CharacterController>();
        lastPos = currentPos = transform.position.x;
        distanceCovered.value = 0;
        slimeAnimator = GetComponentInChildren<Animator>();
        resizeDelay = new WaitForSeconds(0.05f);
        StartCoroutine(SizeChangeLoop());
    }

    //Why does mathF not have a remap??
    private float Remap(float value, float inMin, float inMax, float outMin, float outMax)
    {
        float inversed = Mathf.InverseLerp(inMin, inMax, value);
        return Mathf.Lerp(outMin, outMax, inversed);
    }

    void Update()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        float size = transform.localScale.y;
        
        remappedSize = Remap(size, 0f, 6f, .5f, 3f);

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
        
        if (Mathf.Abs(horizontal) >= 0.1f && knockBackDirection == 0)
        {
            direction.x = horizontal * speed * remappedSize;
            // distanceCovered.value = -(Mathf.Abs(currentPos - lastPos)*20f + size/1000)*Time.deltaTime;
            // MovementEvent.Invoke();
            slimeAnimator.SetBool("isWalking", true);
        }
        else
        {
            slimeAnimator.SetBool("isWalking", false);
        }
        
        if (direction.x != 0)
        {
            direction.x += Math.Sign(direction.x) * -5 * Time.deltaTime - Math.Sign(direction.x)* size*2*Time.deltaTime;
        }
        if (Mathf.Abs(direction.x) < 0.25)
        {
            knockBackDirection = 0;
            direction.x = 0;
        }
        
        if ((vertical >= jumpThreshold || jumpButton) && playerController.isGrounded)
        {
            Jump();
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
            jumpButton = false;
        }

        if (direction.y <= 0)
        {
            isJumping = false;
        }
        
        if (Mathf.Abs(direction.x) < 0.1 && !isFalling && !isJumping)
        {
            GetComponentInChildren<SlimeSize>().Regenerate(true);
        }
        else
        {
            GetComponentInChildren<SlimeSize>().Regenerate(false);
        }

        slimeAnimator.SetBool("isJumping", isJumping);
        slimeAnimator.SetBool("isFalling", isFalling);
        playerController.Move(direction * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        
    }

    private IEnumerator SizeChangeLoop()
    {
        currentPos = transform.position.x;
        float size = transform.localScale.y;
        yield return resizeDelay;

        float positionDelta = Mathf.Abs(currentPos - lastPos);
        distanceCovered.value = -(positionDelta/40 + size*positionDelta/100);
        MovementEvent.Invoke();
        lastPos = currentPos;
        StartCoroutine(SizeChangeLoop());
    }

    public void Jump()
    {
        //jump strength should be proportional to size of slime
        direction.y = jumpDefault * remappedSize;
        //Take a chunk of size off for jumping
        JumpEvent.Invoke();
        isJumping = true;   
        jumpButton = false;
    }
    
    
    public void KnockBack(EntityBehaviour other)
    {
        if (transform.localPosition.x <= other.transform.localPosition.x)
        {
            knockBackDirection = -1;
        }
        else knockBackDirection = 1;
        
        direction.x = knockBackAmount * knockBackDirection * remappedSize;
        direction.y = 2 * remappedSize;
    }
}
