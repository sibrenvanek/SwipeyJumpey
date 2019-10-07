using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TouchTutorialHand : MonoBehaviour
{
    private bool active = true;

    private void Update() 
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(active)
            {
                StartCoroutine(FadeAndDeactivate());
            }
        }    
    }

    private IEnumerator FadeAndDeactivate()
    {
        GetComponent<SpriteRenderer>().DOFade(0, 2f);
        active = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
