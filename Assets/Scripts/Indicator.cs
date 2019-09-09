using UnityEngine;

public class Indicator : MonoBehaviour
{
    /*************
     * VARIABLES *
     *************/

    /**General**/
    [SerializeField] private Transform indicatorSprite = null;

    /*************
     * FUNCTIONS *
     *************/

    /**General**/

    // Destroy child when destroyed
    public void OnDestroy()
    {
        Destroy(indicatorSprite.gameObject);
    }

    /**Child**/

    // Add distance between object and child
    public void SetDistance(float distance)
    {
        indicatorSprite.localPosition = new Vector3(distance, 0, 0);
    }

    // Set width of child
    public void SetWidth(float width)
    {
        indicatorSprite.localScale = new Vector3(width, 1);
    }
}
