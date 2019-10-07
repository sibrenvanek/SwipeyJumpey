using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCollectable : Collectable
{
    private PlayerManager player = null;
    public override void Collect()
    {
        base.Collect();
        
        if(player == null)
            player = FindObjectOfType<PlayerManager>();

        player.Collect(this);
        ProgressionManager.Instance.IncreaseAmountOfSideCollectables();
    }
}
