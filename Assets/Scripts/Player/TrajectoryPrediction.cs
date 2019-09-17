using UnityEngine;

public class TrajectoryPrediction : MonoBehaviour
{
    [SerializeField] private Indicator indicatorPrefab = null;
    [SerializeField] private MouseIndicator mouseIndicatorPrefab = null;
    private Indicator activeIndicator = null;
    private MouseIndicator activeMouseIndicator = null;

    public void UpdateTrajectory(Vector2 baseMousePosition, Vector2 startVelocity, Vector2 gravity, float angle, float time)
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

        if (startVelocity.x < 0)
            distance.x *= -1;

        if (startVelocity.y < 0)
            distance.y *= -1;

        if (!activeIndicator)
        {
            activeIndicator = Instantiate(indicatorPrefab, transform.position, Quaternion.identity);
            activeMouseIndicator = Instantiate(mouseIndicatorPrefab, baseMousePosition, Quaternion.identity);
        }

        activeIndicator.transform.eulerAngles = new Vector3(0, 0, angle);
        activeIndicator.SetDistance(lengthZ / 2);
        activeIndicator.SetWidth(lengthZ);
    }

    public void RemoveIndicators()
    {
        if (!activeIndicator)
            return;

        Destroy(activeMouseIndicator.gameObject);
        Destroy(activeIndicator.gameObject);
        activeIndicator = null;
        activeMouseIndicator = null;
    }
}
