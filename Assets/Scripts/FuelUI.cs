using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour
{
    [SerializeField] private Image fuelImage = null;
    private float secondsToEmpty = 2f;
    private PlayerMovement playerMovement = null;


    private void Update() 
    {
        
    }

    private void OnSlowMotionActivated()
    {

    }

    private void OnSlowMotionDeActivated()
    {

    }
}
