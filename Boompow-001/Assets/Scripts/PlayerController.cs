using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject Sword;
    public MeleeWeaponTrail SwordTrail;
    public GameObject Scabbard;
    public GameObject Rhand;
    public GameObject Lhand;

    public Material BaseEmission;
    public Material GlowEmission;

    public float walkSpeed = 2;
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
    bool stunned;

	Animator animator;
    Transform cameraT;
    CharacterController controller;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
        cameraT = Camera.main.transform;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!stunned)
        {
            Vector3 fwd = cameraT.forward;
            fwd.y = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(fwd), .1f);
        }
    }

    void FixedUpdate()
    {


        // input
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;
            bool running = Input.GetKey(KeyCode.LeftShift);
        DebugConsole.Clear();
        DebugConsole.Log("Input: " + inputDir.ToString(), "normal");
        DebugConsole.Log("Running: " + running.ToString(), "normal");


        DebugConsole.Log("Animator: " + animator.ToString(), "normal");
        DebugConsole.Log("Controller: " + controller.ToString(), "normal");
        DebugConsole.Log("Controller velocity: " + controller.velocity.ToString(), "normal");
        DebugConsole.Log("walkSpeed: " + walkSpeed.ToString(), "normal");
        DebugConsole.Log("runSpeed: " + runSpeed.ToString(), "normal");


        if (Input.GetMouseButton(1))
        {
            stunned = true;
            DebugConsole.Log("RMB is down", "normal");
            animator.SetBool("Stunned", true);
        }
        else
        {
            stunned = false;
            animator.SetBool("Stunned", false);
            if (Input.GetMouseButton(0))
            {
                DebugConsole.Log("LMB is down", "normal");
                Move(Vector2.zero, false);
                animator.SetBool("Attacking", true);
            }
            else
            {
                Move(inputDir, running);
                animator.SetBool("Attacking", false);
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
        DebugConsole.Log("Velocity: " + velocity.ToString(), "normal");
        DebugConsole.Log("Time: " + Time.deltaTime.ToString(), "normal");

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

    void SwordTrailEmission(float time)
    {
        SwordTrail.EmitTime = time;
        SwordTrail.Emit = time > 0;
    }

    void Glow(bool on)
    {
        if (on)
        {
        }
        else
        {

        }
    }

}