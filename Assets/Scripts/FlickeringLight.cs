using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class FlickeringLight : MonoBehaviour
{
    private new Light2D light = null;
    private bool active = true;

    [Header("User edited values")]
    [SerializeField] private float minFlickerSpeed = 1f;
    [SerializeField] private float maxFlickerSpeed = 1f;
    [SerializeField] private float minDecreaseIntensity = .2f;
    [SerializeField] private float maxDecreaseIntensity = .7f;
    [SerializeField] private float minIncreaseIntensity = 1.5f;
    [SerializeField] private float maxIncreaseIntensity = 2f;

    [Header("Not to be input by user")]
    [SerializeField] private bool increasing = false;
    [SerializeField] private float increaseGoal;
    [SerializeField] private float decreaseGoal;
    [SerializeField] private float flickerSpeed;
    [SerializeField] private float goalOffset = .05f;
    

    private void Awake()
    {
        light = GetComponent<Light2D>();
        increaseGoal = maxIncreaseIntensity;
        decreaseGoal = minDecreaseIntensity;
    }

    private void Update() 
    {
        if(active)
        {
            if(increasing)
            {
                IncreaseIntensity();
            }
            else
            {
                DecreaseIntensity();
            }
        }    
    }

    private void OnGoalReached()
    {
        if(increasing)
        {
            increasing = false;
            SetNewIncreaseGoal();
        }
        else
        {
            increasing = true;
            SetNewDecreaseGoal();
        }

        SetNewFlickerSpeed();
    }

    private void SetNewIncreaseGoal()
    {
        increaseGoal = Random.Range(minIncreaseIntensity, maxDecreaseIntensity);
    }

    private void SetNewDecreaseGoal()
    {
        decreaseGoal = Random.Range(minDecreaseIntensity, maxDecreaseIntensity);
    }

    private void SetNewFlickerSpeed()
    {
        flickerSpeed = Random.Range(minFlickerSpeed, maxFlickerSpeed);
    }

    private void IncreaseIntensity()
    {
        light.intensity = Mathf.Lerp(light.intensity, increaseGoal, flickerSpeed * Time.deltaTime);
        if(light.intensity >= increaseGoal - goalOffset)
        {
            OnGoalReached();
        }
    }

    private void DecreaseIntensity()
    {
        light.intensity = Mathf.Lerp(light.intensity, decreaseGoal, flickerSpeed * Time.deltaTime);
        if(light.intensity <= decreaseGoal + goalOffset)
        {
            OnGoalReached();
        }
    }

    public void TurnOn()
    {
        active = true;
        light.intensity = 1;
    }

    public void TurnOff()
    {
        active = false;
        light.intensity = 0;
    }
}
