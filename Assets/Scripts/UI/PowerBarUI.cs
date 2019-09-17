using UnityEngine;
using UnityEngine.UI;

public class PowerBarUI : MonoBehaviour
{
    [SerializeField] private GameObject background = null;
    [SerializeField] private GameObject bar = null;
    [SerializeField] private float maximumHeight = 0f;
    private bool displaying = false;
    private Slider slider = null;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = maximumHeight;
    }

    public void DisplayForce(Vector2 force, Vector2 maximumForce)
    {
        slider.value = (maximumHeight / maximumForce.magnitude) * force.magnitude;

        if (!displaying)
        {
            displaying = true;
            background.SetActive(true);
            bar.SetActive(true);
        }
    }

    public void ResetBar()
    {
        displaying = false;
        background.SetActive(false);
        bar.SetActive(false);
    }
}
