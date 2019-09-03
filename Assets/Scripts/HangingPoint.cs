using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingPoint : MonoBehaviour
{
    //TODO: Single responsibility!!!
    public Transform player;
    [SerializeField]private float detectionRange = 2f;
    [SerializeField]private float centerRange = .2f;
    [SerializeField]private float dragSpeed = 3f;
    private Rigidbody2D playerRigidbody = null;

    private bool holdingPlayer = false;

    private void Start() {
        playerRigidbody = player.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if(distanceToPlayer <= detectionRange && !holdingPlayer)
        {
            if(distanceToPlayer > centerRange)
            {
                DisablePlayerPhysics(); //Should be done in player script
                DragPlayer();
            }else
            {
                EnablePlayerMovement();
                holdingPlayer = true;
                playerRigidbody.velocity = Vector3.zero;
            }
        }  
    }

    private void EnablePlayerMovement()
    {
        print("Player can move");
    }
    
    private void DisablePlayerPhysics()
    {
        playerRigidbody.velocity /= 1.2f;
        playerRigidbody.bodyType = RigidbodyType2D.Kinematic;
    }

    private void DragPlayer()
    {
        player.position = Vector2.MoveTowards(player.position, transform.position, dragSpeed * Time.deltaTime);
    }
}
