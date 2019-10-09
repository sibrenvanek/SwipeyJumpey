using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCollectable : Collectable
{
    [SerializeField] private int id = 0;
    [SerializeField] private float opacity = 0.2f;
    private bool hasBeenCollectedBefore = false;

    public override void Collect()
    {
        base.Collect();
    }

    public override void TurnOff()
    {
        base.TurnOff();
        hasBeenCollectedBefore = true;
    }

    public void SetCollected(bool collected = true)
    {
        hasBeenCollectedBefore = collected;
        var renderer = this.GetComponent<SpriteRenderer>();
        renderer.color = new Color(1, 1, 1, opacity);
    }

    public override int GetId()
    {
        return id;
    }
}
