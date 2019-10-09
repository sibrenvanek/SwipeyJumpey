using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackPickup : MonoBehaviour
{
    public event Action OnJetpackPickup = delegate { };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        OnJetpackPickup();
        Destroy(this.gameObject);
    }
}
