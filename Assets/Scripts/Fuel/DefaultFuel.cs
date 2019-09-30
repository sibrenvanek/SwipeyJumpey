using UnityEngine;

public class DefaultFuel : Fuel
{
    [SerializeField] private float forceBoost = 2f;
    [SerializeField] private Vector2 velocityLoss = new Vector2(2f, 2f);

    public override void AddForceBoost(Rigidbody2D rigidbody)
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x / velocityLoss.x, rigidbody.velocity.y / velocityLoss.y);
        rigidbody.AddForce(Vector2.up * forceBoost, ForceMode2D.Impulse);
    }
}
