using UnityEngine;


public class Ground : MonoBehaviour
{
    private bool isGrounded;
    private float friction;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EvaluateCollision(collision);
        RetrieveFriction(collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        EvaluateCollision(collision);
        RetrieveFriction(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
        friction = 0;
    }

    private void EvaluateCollision(Collision2D collision)
    {
        for(int i = 0; i < collision.contactCount; i++)
        {
            Vector2 normal = collision.GetContact(i).normal;
            isGrounded |= normal.y >= 0.9f;
        }
    }
    private void RetrieveFriction(Collision2D collision)
    {
        PhysicsMaterial2D material = collision.collider.sharedMaterial;

        friction = 0;

        if (material)
            friction = material.friction;
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }
    public float GetFriction()
    {
        return friction;
    }
}
