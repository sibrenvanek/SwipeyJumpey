using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainCollectable : Collectable
{
    [SerializeField] private int id = 0;
    private bool hasBeenCollectedBefore = false;

    public override void Collect()
    {
        if (!hasBeenCollectedBefore)
        {
            ProgressionManager.Instance.IncreaseAmountOfMainCollectables(ConvertToMainCollectable());
        }
        gameObject.SetActive(false);
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

    public override void TurnOff()
    {
        hasBeenCollectedBefore = true;
        gameObject.SetActive(false);
    }
}
