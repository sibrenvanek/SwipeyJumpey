using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{

    public virtual void Start()
    {

    }

    public virtual void Collect()
    {
        ProgressionManager.Instance.IncreaseAmountOfSideCollectables();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Collect();
        }
    }
}
