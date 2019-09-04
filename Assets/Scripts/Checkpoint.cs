﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public void Check()
    {
        GameManager.Instance.SetLatestCheckpoint(this);
    }
}
