using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainCollectable : Collectable
{
    [SerializeField] private int id = 0;
    [SerializeField] private float opacity = 0.2f;
    private bool hasBeenCollectedBefore = false;

    public override void Collect()
    {
        base.Collect();

        if (!hasBeenCollectedBefore)
        {
            ProgressionManager.Instance.IncreaseAmountOfMainCollectables(ConvertToMainCollectable());
        }
    }

    public MinifiedMainCollectable ConvertToMainCollectable()
    {
        return new MinifiedMainCollectable
        {
            id = this.id,
                name = this.name,
                position = this.transform.position
        };
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

    public int GetId()
    {
        return id;
    }
}
