using DG.Tweening;
using DG.Tweening.Core;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlowMotion : MonoBehaviour
{
    public event Action OnSlowMotionActivated = delegate {};
    public event Action OnSlowMotionDeActivated = delegate {};
    [SerializeField] private Rigidbody2D playerRigidbody = null;
    [SerializeField] private PlayerMovement playerMovement = null;
    [SerializeField] private float slowSpeed = .9f;
    [SerializeField] private float slowMotionDuration = 2f;
    [SerializeField] private float pitchReduction = 0.1f;
    [SerializeField] private float totalPitchReduction = 0.5f;
    [SerializeField] private GameObject radialFill = null;
    [SerializeField] private Image fill = null;
    public bool doingSlowmotion { get; private set; } = false;
    private Coroutine curCoroutine = null;
    public DOSetter<float> FillSetter { get; private set; }
    public DOGetter<float> FillGetter { get; private set; }

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        SetUpDOGettersAndSetters();
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
        Cancel();
        curCoroutine = StartCoroutine(StartSlowMotionSequence());
    }

    public void Cancel()
    {
        if (curCoroutine != null)
        {
            StopCoroutine(curCoroutine);
            DOTween.KillAll();
        }
        ResetTime();
    }

    private IEnumerator StartSlowMotionSequence()
    {
        OnSlowMotionActivated.Invoke();
        playerMovement.RemoveGravity();

        fill.fillAmount = 1f;
        radialFill.SetActive(true);
        DOTween.To(FillGetter, FillSetter, 0f, slowMotionDuration);

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
        AudioManager.Instance.ReduceAudioPitch(pitchReduction);
    }

    private void ResetTime()
    {
        if (doingSlowmotion)
        {
            radialFill.SetActive(false);
            AudioManager.Instance.ResetAudioPitch();
            OnSlowMotionDeActivated.Invoke();
            doingSlowmotion = false;
            playerMovement.CancelJump();
            playerMovement.RestoreGravity();
        }
    }

    private void SetUpDOGettersAndSetters()
    {
        FillGetter = new DOGetter<float>(() => {
            return fill.fillAmount;
        });

        FillSetter = new DOSetter<float>((fill) => {
            this.fill.fillAmount = fill;
        });
    }
}
