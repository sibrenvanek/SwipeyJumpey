using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidbody = null;
    [SerializeField] private PlayerManager playerManager = null;
    [SerializeField] private float slowSpeed = .9f;
    [SerializeField] private float slowMotionDuration = 2f;
    [SerializeField] private float minVelocityInPercent = 10f;

    [SerializeField] private bool doingSlowmotion = false;

    private Vector2 oldVelocity = Vector2.zero;
    private Vector2 goalVelocity = Vector2.zero;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerManager = GetComponent<PlayerManager>();
    }

    private void Update()
    {
        if (doingSlowmotion)
        {
            SlowTime();
        }
    }

    public void Go()
    {
        StartCoroutine(StartSlowMotionSequence());
    }

    public void Cancel()
    {
        ResetTime();
    }

    private IEnumerator StartSlowMotionSequence()
    {
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

    private Vector2 CalculateVelocityGoal()
    {
        return oldVelocity / 100 * minVelocityInPercent;
    }

    private void SlowTime()
    {
        playerRigidbody.velocity *= slowSpeed;
    }

    private void ResetTime()
    {
        doingSlowmotion = false;
        playerManager.EnablePhysics();
    }

}
