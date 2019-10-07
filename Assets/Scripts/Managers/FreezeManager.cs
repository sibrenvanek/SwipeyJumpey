using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeManager : MonoBehaviour
{
    [SerializeField] private float frozenTimeScale = 0f;
    private bool freezing = false;

    public void Freeze(float freezeTime)
    {
        if(!freezing)
        {
            StartCoroutine(FreezeAndContinue(freezeTime));
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
