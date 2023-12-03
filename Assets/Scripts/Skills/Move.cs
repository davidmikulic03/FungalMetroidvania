using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ground)), RequireComponent(typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField] private MovementStats movement;

    private Vector2 direction;
    private Vector2 velocity;

    private Rigidbody2D body = null;
    private Ground ground = null;

    private float accelerationTime = 0;
    private float decelerationTime = 0;
    private bool isGrounded;
    private float friction = 0;

    void Awake()
    {
        Initialize();
    }
    void Update()
    {
        Tick();
    }
    void FixedUpdate()
    {
        FixedTick();
    }

    private void Initialize()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();

        input.Enable();
    }
    private void Tick()
    {
        direction.x = input.HorizontalMoveInput();
        friction = ground.GetFriction();
    }
    private void FixedTick()
    {
        isGrounded = ground.GetIsGrounded();
        velocity = body.velocity;

        if (direction.x != 0)
            Accelerate();
        else
            Decelerate();

        body.velocity = velocity;
    }

    void Accelerate()
    {
        accelerationTime = isGrounded ? movement.groundedAccelerationTime : movement.airborneAccelerationTime;
        float maxSpeed = Mathf.Max(movement.maxSpeed - friction, 0);
        float speedChange = (maxSpeed / accelerationTime) * Time.fixedDeltaTime;

        if(Mathf.Abs(velocity.x + speedChange * direction.x) < maxSpeed)
            velocity += speedChange * direction;
        else
            velocity.x = maxSpeed * direction.x;
    }
    void Decelerate()
    {
        decelerationTime = isGrounded ? movement.groundedDecelerationTime : movement.airborneDecelerationTime;
        float maxSpeed = movement.maxSpeed;
        float speedChange = (maxSpeed / decelerationTime) * Time.fixedDeltaTime;

        if (Mathf.Abs(velocity.x) - speedChange > 0)
            velocity.x -= Mathf.Sign(velocity.x) * speedChange;
        else
            velocity.x = 0f;
    }
}
