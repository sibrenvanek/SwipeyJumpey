using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FinishRocket : MonoBehaviour
{
    

    private bool flying = false;
    [SerializeField] private float speed = 2f;

    private void FixedUpdate() 
    {
        if(flying)
            transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);    
    }

    public void Fly(Action callback)
    {
        StartCoroutine(FlyAndCallback(callback));
    }

    private IEnumerator FlyAndCallback(Action callback)
    {
        flying = true;
        yield return new WaitForSeconds(2f);
        flying = false;
        callback();
    }
}
