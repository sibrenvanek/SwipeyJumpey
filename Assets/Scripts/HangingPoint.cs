using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HangingPointType
{
    Fuel,
    ACouldHaveHangingPointType
}

public class HangingPoint : MonoBehaviour
{
    public PlayerMovement playerMovement;
    [SerializeField] private bool active = true;
    [SerializeField] private float timeBeforeReset = 5f;
    [SerializeField] private float maxHangingTime = 2f;
    [SerializeField] private float detectionRange = 2f;
    [SerializeField] private float centerRange = .2f;
    [SerializeField] private float dragSpeed = 3f;
    [SerializeField] private bool holdingPlayer = false;
    [SerializeField] private HangingPointType hangingPointType;
    [SerializeField] private int maxResets = 0; //0 is infinite
    private bool isInfinite = false;
    private int resetCounter = 0;

    private void Start()
    {
        isInfinite = maxResets > 0 ? false : true;
    }

    private void Update()
    {
        if (active)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerMovement.transform.position);

            if (distanceToPlayer <= detectionRange && !holdingPlayer)
            {
                if (distanceToPlayer > centerRange)
                {
                    DragPlayer();
                }
                else
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

        playerMovement.StartHang(this);
    }

    public void TurnOff()
    {
        playerMovement.StopHang();

        active = false;
        holdingPlayer = false;

        if (isInfinite || resetCounter < maxResets)
        {
            StartCoroutine(WaitAndResetPoint());
        }
    }

    private IEnumerator WaitAndTurnOff()
    {
        yield return new WaitForSeconds(maxHangingTime);

        if (active)
        {
            TurnOff();
        }
    }

    private IEnumerator WaitAndResetPoint()
    {
        yield return new WaitForSeconds(timeBeforeReset);
        ResetPoint();
    }

    private void ResetPoint()
    {
        active = true;
        resetCounter++;
    }

    private void DragPlayer()
    {
        playerMovement.transform.position = Vector2.MoveTowards(playerMovement.transform.position, transform.position, dragSpeed * Time.deltaTime);
    }
}
