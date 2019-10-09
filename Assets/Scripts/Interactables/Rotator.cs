using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField, Range(-100f,100f)] private float speed = 2f;

    private void FixedUpdate() 
    {
        if(speed != 0)
            transform.Rotate(0,0,speed * Time.deltaTime);    
    }
}
