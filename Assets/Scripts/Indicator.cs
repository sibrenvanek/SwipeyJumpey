using UnityEngine;

public abstract class Indicator : MonoBehaviour
{
    [SerializeField] internal Transform indicatorSprite = null;

    public void OnDestroy()
    {
        Destroy(indicatorSprite.gameObject);
    }

    public virtual void SetDistance(float distance)
    {
        indicatorSprite.localPosition = new Vector3(distance, 0, 0);
    }

    public void SetWidth(float width)
    {
        indicatorSprite.localScale = new Vector3(width, 1);
    }
}
