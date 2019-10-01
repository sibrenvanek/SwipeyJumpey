using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

public class Jetpack : MonoBehaviour
{
    [SerializeField] private Transform launchPos = null;
    [SerializeField] private GameObject vfxLaunchPrefab = null;
    private GameObject launchInstance = null;
    private Animator launchAnimator = null;
    private BoxCollider2D launchBoxColider = null;
    private Animator animator = null;
    [SerializeField] private ParticleSystem trailParticleSystem = null;
    [SerializeField] private Jet[] jets = null;

    [Header("Charging values")]
    [SerializeField] private float chargingStartLifeTime = .1f;
    [SerializeField] private float chargingStartSize = .5f;

    [Header("Flying values")]
    [SerializeField] private float flyingStartLifeTime = .2f;
    [SerializeField] private float flyingStartSize = .8f;
    [SerializeField] private float maximumAdditionToSize = 0.7f;

    [Header("Engine tests")]
    [SerializeField] private bool testEngines = false;
    [SerializeField] private float testInterval = 2f;
    public bool EngineRunning { get; private set; }
    public bool EngineCharging { get; private set; }

    private void OnValidate()
    {
        jets = GetComponentsInChildren<Jet>();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (!isActiveAndEnabled)
            return;

        if (testEngines && isActiveAndEnabled)
            StartCoroutine(TestEngines());
    }

    public void Explode()
    {
        if (!isActiveAndEnabled)
            return;

        animator.SetTrigger("Explode");
    }

    public void Charge()
    {
        if (!isActiveAndEnabled)
            return;

        EngineCharging = true;
        TurnOnJets();
        foreach (var jet in jets)
        {
            jet.Accelerate(chargingStartLifeTime, chargingStartSize);
        }
    }

    public void Launch()
    {
        if (!isActiveAndEnabled)
            return;

        EngineCharging = false;
        foreach (var jet in jets)
        {
            jet.Accelerate(flyingStartLifeTime, flyingStartSize);
        }
        ActivateLaunchEffects();
    }

    public void Launch(Vector2 velocity, Vector2 maximumForce)
    {
        if (!isActiveAndEnabled)
            return;

        float addition = (maximumAdditionToSize / maximumForce.magnitude) * velocity.magnitude;

        EngineCharging = false;
        foreach (var jet in jets)
        {
            jet.Accelerate(flyingStartLifeTime, flyingStartSize + addition);
        }
        ActivateLaunchEffects();
    }

    public void TurnOff()
    {
        if (!isActiveAndEnabled)
            return;

        trailParticleSystem.Stop();

        foreach (var jet in jets)
        {
            jet.StopEngine();
        }

        EngineRunning = false;
        EngineCharging = false;
    }

    private void TurnOnJets()
    {
        if (!isActiveAndEnabled)
            return;

        foreach (var jet in jets)
        {
            jet.StartEngine();
        }
        EngineRunning = true;
    }

    private void ActivateLaunchEffects()
    {
        if (!isActiveAndEnabled)
            return;

        if (launchInstance == null)
        {
            launchInstance = Instantiate(vfxLaunchPrefab, launchPos.position, Quaternion.identity);
            launchAnimator = launchInstance.GetComponent<Animator>();
            launchBoxColider = launchInstance.GetComponent<BoxCollider2D>();
        }
        else
        {
            launchInstance.transform.position = launchPos.position;
            launchAnimator.SetTrigger("Launch");

            launchBoxColider.enabled = false;
            launchBoxColider.enabled = true;
        }
    }

    private IEnumerator TestEngines(bool charge = false)
    {
        if (isActiveAndEnabled)
            yield break;

        if (charge)
        {
            Charge();
        }
        else
        {
            Launch();
        }

        yield return new WaitForSeconds(testInterval);

        StartCoroutine(TestEngines(!charge));
    }
}
