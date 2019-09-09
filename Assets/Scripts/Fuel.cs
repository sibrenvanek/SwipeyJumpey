using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    /*************
     * VARIABLES *
     *************/

    /**General**/
    [SerializeField] private float forceBoost = 2f;
    [SerializeField] private Vector2 velocityLoss = new Vector2(2f,2f);

    /*************
     * FUNCTIONS *
     *************/

    /**General**/
    public void PickUp(Rigidbody2D rigidbody)
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x / velocityLoss.x, rigidbody.velocity.y / velocityLoss.y);
        rigidbody.AddForce(Vector2.up * forceBoost, ForceMode2D.Impulse);
    }
}
