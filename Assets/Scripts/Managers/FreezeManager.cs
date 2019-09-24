using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeManager : MonoBehaviour
{
    public static FreezeManager Instance { get; private set; }

    [SerializeField] private float frozenTimeScale = 0f;
    private bool freezing = false;
    private Coroutine currentCoroutine = null;

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

    public void Freeze(float freezeTime)
    {
        if(!freezing)
        {
            currentCoroutine = StartCoroutine(FreezeAndContinue(freezeTime));
        }
    }   

    private IEnumerator FreezeAndContinue(float freezeTime)
    {
        freezing = true;
        Time.timeScale = frozenTimeScale;

        yield return new WaitForSecondsRealtime(freezeTime);

        freezing = false;
        Time.timeScale = 1;
    }
}
