using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SlowMotion : MonoBehaviour
{
    /*************
     * VARIABLES *
     *************/

    /**General**/
    public event Action OnSlowMotionActivated = delegate{};
    public event Action OnSlowMotionDeActivated = delegate{};
    [SerializeField] private Rigidbody2D playerRigidbody = null;
    [SerializeField] private PlayerManager playerManager = null;

    /**SlowMotion**/
    [SerializeField] private float slowSpeed = .9f;
    [SerializeField] private float slowMotionDuration = 2f;
    [SerializeField] private float minVelocityInPercent = 10f;
    [SerializeField] private bool doingSlowmotion = false;
    private Vector2 oldVelocity = Vector2.zero;
    private Vector2 goalVelocity = Vector2.zero;
    private Coroutine curCoroutine = null;

    /*************
     * FUNCTIONS *
     *************/

    /**General**/

    // Awake is called before any other function is executed
    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerManager = GetComponent<PlayerManager>();
    }

    // FixedUpdate is called within fixed intervals
    private void FixedUpdate()
    {
        if (doingSlowmotion)
        {
            SlowTime();
        }
    }

    /**SlowMotion**/


    // Starts the slowmotion coroutine
    public void Go()
    {
        curCoroutine = StartCoroutine(StartSlowMotionSequence());
    }

    // Stops the current coroutine
    public void Cancel()
    {
        if(curCoroutine != null)
        {
            StopCoroutine(curCoroutine);
        }
        ResetTime();
    }

    // Starts the sequence involved in the slowmotion effects
    private IEnumerator StartSlowMotionSequence()
    {
        OnSlowMotionActivated.Invoke();
        playerManager.DisablePhysics();

        oldVelocity = playerRigidbody.velocity;
        goalVelocity = CalculateVelocityGoal();
        doingSlowmotion = true;
        
        yield return new WaitForSeconds(slowMotionDuration);

        if (doingSlowmotion)
        {
            ResetTime();
        }
    }

    // Calculates the desired slowmotion velocity
    private Vector2 CalculateVelocityGoal()
    {
        return oldVelocity / 100 * minVelocityInPercent;
    }

    // Slows the player down
    private void SlowTime()
    {
        playerRigidbody.velocity *= slowSpeed;
    }

    // Resets the time that the player is in slowmotion
    private void ResetTime()
    {
        if(doingSlowmotion)
        {
            OnSlowMotionDeActivated.Invoke();
            doingSlowmotion = false;
            playerManager.EnablePhysics();
            playerManager.Fall();
        }
    }

}