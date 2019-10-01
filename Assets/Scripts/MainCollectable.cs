using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCollectable : Collectable
{
    [SerializeField] private int id = 0;
    private bool hasBeenCollectedBefore = false;

    public override void Start()
    {
        List<MinifiedMainCollectable> mainCollectables = ProgressionManager.Instance.GetMainCollectables();
        foreach (MinifiedMainCollectable collectable in mainCollectables)
        {
            if (collectable.id == this.id)
            {
                hasBeenCollectedBefore = true;
                gameObject.SetActive(false);
            }
        }
    }

    public override void Collect()
    {
        if (!hasBeenCollectedBefore)
        {
            ProgressionManager.Instance.IncreaseAmountOfMainCollectables(ConvertToMainCollectable());
        }
        gameObject.SetActive(false);
    }

    private MinifiedMainCollectable ConvertToMainCollectable()
    {
        return new MinifiedMainCollectable
        {
            id = this.id,
                name = this.name,
                position = this.transform.position
        };
    }
}
