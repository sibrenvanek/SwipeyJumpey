using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject nextButton = null;

    public void DisableCanvas()
    {
        gameObject.SetActive(false);
    }

    public void EnableCanvas()
    {
        gameObject.SetActive(true);
    }

    public void DisableNextButton()
    {
        nextButton.SetActive(false);
    }

    public void EnableNextButton()
    {
        nextButton.SetActive(true);
    }
}
