using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    /*************
     * VARIABLES *
     *************/

    /**Singleton**/
    public static CanvasManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void DisableCanvas()
    {
        gameObject.SetActive(false);
    }

    public void EnableCanvas()
    {
        gameObject.SetActive(true);
    }
}
