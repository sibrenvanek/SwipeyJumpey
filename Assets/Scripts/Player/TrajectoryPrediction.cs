using UnityEngine;

public class TrajectoryPrediction : MonoBehaviour
{
    [SerializeField] private GameObject indicatorsPrefab = null;
    [SerializeField] private MouseIndicator mouseIndicatorPrefab = null;
    private MouseIndicator activeMouseIndicator = null;
    private Indicator activeIndicator = null;
    private GameObject activeIndicators = null;

    public void UpdateTrajectory(Vector2 baseMousePosition, Vector2 startVelocity, float angle, float time, float cancelSize, Vector2 maxVelocity)
    {
        if (startVelocity.x == 0 && startVelocity.y == 0)
            return;

        float velocity = startVelocity.magnitude;

        float velocityX = Mathf.Cos(angle);
        float velocityY = Mathf.Sin(angle);

        Vector2 distance;
        distance.x = Mathf.Abs(velocity * time * velocityX);
        distance.y = Mathf.Abs(velocity * time * velocityY);

        float lengthZ = Mathf.Sqrt(Mathf.Pow(distance.x, 2) + Mathf.Pow(distance.y, 2));

        float maximumVelocity = maxVelocity.magnitude;

        float maxVelocityX = Mathf.Cos(angle);
        float maxVelocityY = Mathf.Sin(angle);

        Vector2 maxDistance;
        maxDistance.x = Mathf.Abs(maximumVelocity * time * maxVelocityX);
        maxDistance.y = Mathf.Abs(maximumVelocity * time * maxVelocityY);

        float maxLengthZ = Mathf.Sqrt(Mathf.Pow(maxDistance.x, 2) + Mathf.Pow(maxDistance.y, 2));


        if (startVelocity.x < 0)
            distance.x *= -1;

        if (startVelocity.y < 0)
            distance.y *= -1;

        if (!activeIndicator)
        {
            activeIndicators = Instantiate(indicatorsPrefab, transform.position, Quaternion.identity);
            activeIndicator = activeIndicators.GetComponentInChildren<Indicator>();
            activeMouseIndicator = Instantiate(mouseIndicatorPrefab, baseMousePosition, Quaternion.identity);
            activeMouseIndicator.SetCancelDistance(cancelSize);
        }

        activeIndicators.transform.position = transform.position;
        activeIndicators.transform.eulerAngles = new Vector3(0, 0, angle);
        activeIndicator.SetDistance(maxLengthZ / 2);
        activeIndicator.SetWidth(lengthZ / 4);
    }

    public void RemoveIndicators()
    {
        if(activeIndicators)
        {   
            if (activeIndicator)
            {
                activeIndicator = null;
            }
            Destroy(activeIndicators.gameObject);
        }
        

        if (activeMouseIndicator)
        {
            Destroy(activeMouseIndicator.gameObject);
            activeMouseIndicator = null;
        }
    }
}
