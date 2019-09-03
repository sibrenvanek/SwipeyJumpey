using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum HangingPointType
{
    Fuel,
    ACouldHaveHangingPointType
}

public class HangingPoint : MonoBehaviour
{
    public Transform player;
    [SerializeField]private bool active = true;
    [SerializeField]private float timeBeforeReset = 5f;
    [SerializeField]private float maxHangingTime = 2f;
    [SerializeField]private float detectionRange = 2f;
    [SerializeField]private float centerRange = .2f;
    [SerializeField]private float dragSpeed = 3f;
    [SerializeField]private bool holdingPlayer = false;

    [SerializeField]private HangingPointType hangingPointType;

    private void Update() {
        if(active)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if(distanceToPlayer <= detectionRange && !holdingPlayer)
            {
                if(distanceToPlayer > centerRange)
                {
                    DragPlayer();
                }else
                {
                    HoldPlayer();
                    StartCoroutine(WaitAndTurnOff());
                }
            }  
        }
    }
    
    private void HoldPlayer()
    {
        holdingPlayer = true;

        //Tell player to start hanging
    }

    private IEnumerator WaitAndTurnOff()
    {
        yield return new WaitForSeconds(maxHangingTime);

        //Tell player to stop hanging
        active = false;
        StartCoroutine(WaitAndResetPoint());
    }

    private IEnumerator WaitAndResetPoint()
    {
        yield return new WaitForSeconds(timeBeforeReset);
        ResetPoint();
    }

    private void ResetPoint() {
        active = true;
    }
    
    private void DragPlayer()
    {
        player.position = Vector2.MoveTowards(player.position, transform.position, dragSpeed * Time.deltaTime);
    }
}
