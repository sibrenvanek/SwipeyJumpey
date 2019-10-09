using UnityEngine;

public class ExplodingFuel : Fuel
{
    [SerializeField] private float explosionVelocity = 20;

    public override void AddForceBoost(Rigidbody2D rigidbody)
    {
        Vector3 velocity = rigidbody.velocity;
        velocity.x = Mathf.Abs(velocity.x);
        velocity.y = Mathf.Abs(velocity.y);
        float times = explosionVelocity / (velocity.x + velocity.y);
        velocity *= times;
        if (rigidbody.velocity.x > 0 && velocity.x > 0)
            velocity.x *= -1;
        if (rigidbody.velocity.y > 0 && velocity.y > 0)
            velocity.y *= -1;
        rigidbody.velocity = velocity;
    }
}
