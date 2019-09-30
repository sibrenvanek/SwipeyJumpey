using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private bool isMainCollectable = false;
    private bool hasBeenCollectedBefore = false;

    private void Start()
    {
        if (isMainCollectable)
        {
            List<MainCollectable> mainCollectables = ProgressionManager.Instance.GetMainCollectables();
            foreach (MainCollectable collectable in mainCollectables)
            {
                if (collectable.name == this.name && collectable.position.x == this.transform.position.x && collectable.position.y == this.transform.position.y)
                {
                    hasBeenCollectedBefore = true;
                    gameObject.SetActive(false);
                }
            }
        }
    }

    private void Collect()
    {
        if (isMainCollectable)
        {
            if (!hasBeenCollectedBefore)
            {
                ProgressionManager.Instance.IncreaseAmountOfMainCollectables(ConvertToMainCollectable());
            }
        }
        else
        {
            ProgressionManager.Instance.IncreaseAmountOfSideCollectables();
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Collect();
        }
    }

    private MainCollectable ConvertToMainCollectable()
    {
        return new MainCollectable
        {
            name = this.name,
                position = this.transform.position
        };
    }
}
