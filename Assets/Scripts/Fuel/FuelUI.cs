using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour
{
    [SerializeField] private Image fuelImage = null;
    [SerializeField] private Transform fuelBar = null;
    [SerializeField] private float secondsToEmpty = 2f;
    [SerializeField] private SlowMotion slowMotion = null;

    private void Start()
    {
        slowMotion.OnSlowMotionActivated += OnSlowMotionActivated;
        slowMotion.OnSlowMotionDeActivated += OnSlowMotionDeActivated;
    }

    private void OnSlowMotionActivated()
    {
        ResetFuelImage();
        fuelBar.gameObject.SetActive(true);
        fuelImage.transform.DOScaleX(0, secondsToEmpty);
    }

    private void OnSlowMotionDeActivated()
    {
        ResetFuelImage();
        fuelBar.gameObject.SetActive(false);
    }

    private void ResetFuelImage()
    {
        fuelImage.transform.DOScaleX(1, 0);
    }

    public void SetSlowMotion(SlowMotion newSlowMotion)
    {
        slowMotion = newSlowMotion;
    }

    void OnDestroy(){
        slowMotion.OnSlowMotionActivated -= OnSlowMotionActivated;
        slowMotion.OnSlowMotionDeActivated -= OnSlowMotionDeActivated;
    }
}
