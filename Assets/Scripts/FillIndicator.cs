using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillIndicator : Indicator
{
    [SerializeField] private Color green = new Color(99, 199, 77, 200);
    [SerializeField] private Color orange = new Color(247, 118, 34, 200);
    [SerializeField] private Color red = new Color(228, 59, 68, 200);
    [SerializeField] private float maxDistance = 0;

    public override void SetDistance(float distance)
    {
        SetColor(distance);
        indicatorSprite.localPosition = new Vector3(distance, 0, 0);
    }

    private void SetColor(float distance)
    {
        float distanceRatio = distance / maxDistance;
        if (distanceRatio <= 0.5f)
        {
            this.GetComponentInChildren<SpriteRenderer>().color = green;
        }
        else if (distanceRatio > 0.5f && distanceRatio <= 0.75f)
        {
            this.GetComponentInChildren<SpriteRenderer>().color = orange;
        }
        else
        {
            this.GetComponentInChildren<SpriteRenderer>().color = red;
        }
    }
}
