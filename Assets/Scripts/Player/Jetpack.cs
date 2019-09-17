using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;

public class Jetpack : MonoBehaviour
{
    [SerializeField] private Jet[] jets = null;

    [Header("Charging values")]
    [SerializeField] private float chargingStartLifeTime = .1f;
    [SerializeField] private float chargingStartSize = .5f;

    [Header("Flying values")]
    [SerializeField] private float flyingStartLifeTime = .2f;
    [SerializeField] private float flyingStartSize = .8f;

    [Header("Engine tests")]
    [SerializeField] private bool testEngines = false;
    [SerializeField] private float testInterval = 2f;
    public bool EngineRunning { get; private set; }
    public bool EngineCharging { get; private set; }

    private void OnValidate() {
        jets = GetComponentsInChildren<Jet>();    
    }

    private void Start() {
        if(testEngines)
            StartCoroutine(TestEngines());
    }


    public void Charge()
    {
        EngineCharging = true;
        TurnOnJets();
        foreach (var jet in jets)
        {
            jet.Accelerate(chargingStartLifeTime, chargingStartSize);
        }
    }

    public void Launch()
    {
        EngineCharging = false;
        foreach (var jet in jets)
        {
            jet.Accelerate(flyingStartLifeTime, flyingStartSize);
        }
    }

    public void TurnOff()
    {
        foreach (var jet in jets)
        {
            jet.StopEngine();
        }
        EngineRunning = false;
        EngineCharging = false;
    }

    private void TurnOnJets()
    {
        foreach (var jet in jets)
        {
            jet.StartEngine();
        }
        EngineRunning = true;
    }
    
    private IEnumerator TestEngines(bool charge = false)
    {
        if(charge)
        {
            Charge();
        }else{
            Launch();
        }

        yield return new WaitForSeconds(testInterval);

        StartCoroutine(TestEngines(!charge));
    }
}
