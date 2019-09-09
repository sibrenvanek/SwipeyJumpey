using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour
{
    [SerializeField] private Image fuelImage = null;
    [SerializeField] private float secondsToEmpty = 2f;
    private SlowMotion slowMotion = null;
    private bool inSlowMotion = false;
    private void Start() 
    {
        slowMotion.OnSlowMotionActivated += OnSlowMotionActivated;
        slowMotion.OnSlowMotionDeActivated += OnSlowMotionDeActivated;    
    }

    private void Update() 
    {
        if(inSlowMotion)
        {
            
        }
    }

    private void OnSlowMotionActivated()
    {
        inSlowMotion = true;
        fuelImage.gameObject.SetActive(true);
    }

    private void OnSlowMotionDeActivated()
    {
        inSlowMotion = false;
        ResetFuelImage();
        fuelImage.gameObject.SetActive(false);
    }

    private void ResetFuelImage()
    {
        fuelImage.transform.localScale = new Vector3(1,1,1);
    }
}
