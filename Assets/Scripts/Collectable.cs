using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    protected new Collider2D collider = null;

    protected virtual void Awake() 
    {
        collider = GetComponent<Collider2D>();    
    }

    public virtual void Collect()
    {
        ProgressionManager.Instance.IncreaseAmountOfSideCollectables();
        TurnOff();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Collect();
        }
    }

    public virtual void TurnOff()
    {
        gameObject.SetActive(false);
    }
}
