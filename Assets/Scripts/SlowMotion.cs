using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidbody = null;
    [SerializeField] private float slowSpeed = .9f;
    [SerializeField] private float slowMotionDuration = 2f;
    [SerializeField] private float minVelocityInPercent = 10f;

    [SerializeField] private bool doingSlowmotion = false;

    private Vector2 oldVelocity = Vector2.zero;
    private Vector2 goalVelocity = Vector2.zero;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
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
        float newX = Mathf.Clamp(playerRigidbody.velocity.x - (slowSpeed * Time.deltaTime), goalVelocity.x, oldVelocity.x);
        float newY = Mathf.Clamp(playerRigidbody.velocity.y - (slowSpeed * Time.deltaTime), goalVelocity.y, oldVelocity.y);

        playerRigidbody.velocity = new Vector2(newX, newY);
    }

    private void ResetTime()
    {
        doingSlowmotion = false;
    }

}
