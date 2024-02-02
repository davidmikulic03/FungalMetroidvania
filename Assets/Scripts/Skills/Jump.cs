using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ground)), RequireComponent(typeof(Rigidbody2D))]
public class Jump : MonoBehaviour
{
    [SerializeField] private Controller input = null;
    [SerializeField] private JumpStats jumpStats = null;

    private Rigidbody2D body = null;
    private Ground ground = null;

    private Vector2 velocity = Vector2.zero;
    private bool isGrounded = false;

    private bool jumpInput = false;

    private float coyoteCounter, bufferCounter;

    private void Awake()
    {
        Initialize();
    }
    private void Update()
    {
        Tick();
    }
    private void FixedUpdate()
    {
        FixedTick();
    }
    private void Initialize()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
    }
    private void Tick()
    {
        jumpInput = input.JumpInput();
    }
    private void FixedTick()
    {
        isGrounded = ground.GetIsGrounded();
        velocity = body.velocity;

        if(isGrounded && body.velocity.y == 0)
            coyoteCounter = jumpStats.coyoteTime;
        else if(!jumpInput && coyoteCounter > 0)
            coyoteCounter -= Time.fixedDeltaTime;

        if (jumpInput)
            bufferCounter = jumpStats.bufferTime;
        else if(!jumpInput && bufferCounter > 0)
            bufferCounter -= Time.fixedDeltaTime;

        if (body.velocity.y < 0 || !jumpInput)
            body.gravityScale = jumpStats.fallSpeedMultiplier;
        else
            body.gravityScale = 1f;

        if (bufferCounter > 0)
            PerformJump();

        body.velocity = velocity;
    }

    private void PerformJump()
    {
        if (coyoteCounter > 0)
        {
            bufferCounter = 0;
            coyoteCounter = 0;

            float speed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpStats.jumpHeight);
            velocity.y = speed;
        }
    }
}
