using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject Sword;
    public GameObject Scabbard;
    public GameObject Rhand;
    public GameObject Lhand;

    public float walkSpeed = 6;
	public float runSpeed = 6;
    public float gravity = -12;
    public float jumpHeight = 1;
    [Range(0,1)]
    public float airControlPercent;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    float velocityY;

	Animator animator;
    Transform cameraT;
    CharacterController controller;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
        cameraT = Camera.main.transform;
        controller = GetComponent<CharacterController>();
		
	}

    // Update is called once per frame
    void Update()
    {

        Vector3 fwd = Camera.main.transform.forward;
        fwd.y = 0;
        transform.rotation = Quaternion.LookRotation(fwd);

        // input
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;
            bool running = Input.GetKey(KeyCode.LeftShift);


        if (Input.GetMouseButton(1))
        {
            animator.SetBool("Stunned", true);
        }
        else
        {
            animator.SetBool("Stunned", false);
            if (Input.GetMouseButton(0))
            {
                animator.SetBool("Attacking", true);
            }
            else
            {
                animator.SetBool("Attacking", false);
                Move(inputDir, running);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
            }
        }

        // animator
        float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f);
        animator.SetFloat("SpeedX", inputDir.x * (animationSpeedPercent), speedSmoothTime, Time.deltaTime);
        animator.SetFloat("SpeedY", inputDir.y * (animationSpeedPercent), speedSmoothTime, Time.deltaTime);



    }		

    void Move(Vector2 inputDir, bool running)
    {
        /*
        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
        }
        */
        

        
        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

        velocityY += Time.deltaTime * gravity;
        Vector3 velocity = transform.forward * currentSpeed * inputDir.y + transform.right * currentSpeed * inputDir.x + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

        if (controller.isGrounded)
        {
            velocityY = 0;
        }

       
    }
    

    void Jump ()
    {
        if (controller.isGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
        }
    }

    float GetModifiedSmoothTime(float smoothTime)
    {
        if (controller.isGrounded)
        {
            return smoothTime;
        }
        if (airControlPercent == 0)
        {
            return float.MaxValue;
        }
        return smoothTime / airControlPercent;
    }

    void GrabSword()
    {
        Sword.gameObject.transform.parent = Rhand.transform;
        Sword.gameObject.transform.localRotation = Quaternion.identity;
        Sword.gameObject.transform.localPosition = Vector3.zero;
    }

    void SheathSword()
    {
        Sword.gameObject.transform.parent = Scabbard.transform;
        Sword.gameObject.transform.localRotation = Quaternion.identity;
        Sword.gameObject.transform.localPosition = Vector3.zero;

    }

}