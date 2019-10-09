using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainCollectable : Collectable
{
    [SerializeField] private int id = 0;
    [SerializeField] private float opacity = 0.2f;
    private bool hasBeenCollectedBefore = false;

    public bool HasBeenCollectedBefore()
    {
        return hasBeenCollectedBefore;
    }

    public override void Collect()
    {
        base.Collect();
    }

    public void SetCollected(bool collected = true)
    {
        hasBeenCollectedBefore = collected;
        var renderer = this.GetComponent<SpriteRenderer>();
        renderer.color = new Color(1, 1, 1, opacity);
    }

    public override void TurnOff()
    {
        base.TurnOff();
        hasBeenCollectedBefore = true;
    }

    public override int GetId()
    {
        return id;
    }

    public static MinifiedMainCollectable Minify(MainCollectable mainCollectable)
    {
        return new MinifiedMainCollectable
        {
            id = mainCollectable.id,
            name = mainCollectable.name,
            position = mainCollectable.transform.position
        };
    }
}
