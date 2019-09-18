using System;
using System.Collections;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    public event Action OnSlowMotionActivated = delegate {};
    public event Action OnSlowMotionDeActivated = delegate {};
    [SerializeField] private Rigidbody2D playerRigidbody = null;
    [SerializeField] private PlayerMovement playerMovement = null;
    [SerializeField] private float slowSpeed = .9f;
    [SerializeField] private float slowMotionDuration = 2f;
    [SerializeField] private float minVelocityInPercent = 10f;
    [SerializeField] private float pitchReduction = 0.1f;
    [SerializeField] private float totalPitchReduction = 0.5f;
    public bool doingSlowmotion { get; private set; } = false;
    private Coroutine curCoroutine = null;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
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
        curCoroutine = StartCoroutine(StartSlowMotionSequence());
    }

    public void Cancel()
    {
        if (curCoroutine != null)
        {
            StopCoroutine(curCoroutine);
        }
        ResetTime();
    }

    private IEnumerator StartSlowMotionSequence()
    {
        OnSlowMotionActivated.Invoke();
        playerMovement.RemoveGravity();

        pitchReduction = totalPitchReduction / slowMotionDuration;
        doingSlowmotion = true;

        yield return new WaitForSeconds(slowMotionDuration);

        if (doingSlowmotion)
        {
            ResetTime();
        }
    }

    private void SlowTime()
    {
        playerRigidbody.velocity *= slowSpeed;
        GameManager.Instance.ReduceAudioPitch(pitchReduction);
    }

    private void ResetTime()
    {
        if (doingSlowmotion)
        {
            GameManager.Instance.ResetAudioPitch();
            OnSlowMotionDeActivated.Invoke();
            doingSlowmotion = false;
            playerMovement.CancelJump();
            playerMovement.RestoreGravity();
        }
    }

}
