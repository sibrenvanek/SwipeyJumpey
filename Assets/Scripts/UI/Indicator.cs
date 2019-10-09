using UnityEngine;

public class Indicator : MonoBehaviour
{
    [SerializeField] private Color green = new Color(99, 199, 77, 200);
    [SerializeField] private Color orange = new Color(247, 118, 34, 200);
    [SerializeField] private Color red = new Color(228, 59, 68, 200);
    
    [SerializeField] private SpriteRenderer fillSpriteRenderer = null;
    [SerializeField] protected Transform indicatorSprite = null;
    private SpriteMask spriteMask = null;

    protected virtual void Awake() {
        spriteMask = GetComponentInChildren<SpriteMask>();
    }

    public virtual void SetDistance(float distance)
    {
        indicatorSprite.localPosition = new Vector3(distance, 0, 0);
        fillSpriteRenderer.transform.localPosition = new Vector3(distance, 0, 0);

        if(spriteMask != null)
        {
            spriteMask.transform.localPosition = new Vector3(distance, 0, 0);
        }
    }

    public void SetWidth(float width)
    {
        fillSpriteRenderer.transform.localScale = new Vector3(width, 1);
        SetColor(width);
    }

     private void SetColor(float distance)
    {
        if (distance <= 0.5f)
        {
            fillSpriteRenderer.color = green;
        }
        else if (distance > 0.5f && distance <= 0.75f)
        {
            fillSpriteRenderer.color = orange;
        }
        else
        {
            fillSpriteRenderer.color = red;
        }
    }
}
