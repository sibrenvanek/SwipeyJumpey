using UnityEngine;

public class Indicator : MonoBehaviour
{
    [SerializeField] private Transform spriteTransform;

    // Add distance between object and child
    public void SetDistance(float distance)
    {
        spriteTransform.localPosition = new Vector3(distance, 0, 0);
    }

    // Set width of child
    public void SetWidth(float width)
    {
        spriteTransform.localScale = new Vector3(width, 1);
    }

    // Destroy child when destroyed
    public void OnDestroy()
    {
        Destroy(spriteTransform.gameObject);
        Destroy(this.gameObject);
    }
}
