using System.Collections.Generic;
using UnityEngine;

public class TrajectoryPrediction : MonoBehaviour
{
    /*************
     * VARIABLES *
     *************/

    /**General**/
    [SerializeField] private Indicator indicatorPrefab = null;
    private Indicator activeIndicator = null;

    /*************
     * FUNCTIONS *
     *************/

    /**Trajectory**/

    // Update the current trajectory
    public void UpdateTrajectory(Vector2 startPosition, Vector2 startVelocity, Vector2 gravity, float time)
    {
        RemoveIndicators();

        PlotTrajectory(startPosition, startVelocity, gravity, time);
    }

    // Plot the expected trajectory of the player character
    private void PlotTrajectory(Vector2 startPosition, Vector2 startVelocity, Vector2 gravity, float time)
    {
        if (startVelocity.x == 0 && startVelocity.y == 0)
            return;

        float velocity = startVelocity.magnitude;
        float angle = Mathf.Rad2Deg * Mathf.Atan(startVelocity.y / startVelocity.x);

        if (startVelocity.x < 0 && startVelocity.y < 0)
            angle -= 180;
        else if (startVelocity.x < 0 && startVelocity.y >= 0)
            angle += 180;

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

        Indicator indicator = Instantiate(indicatorPrefab, transform.position, Quaternion.identity);
        indicator.transform.Rotate(0, 0, angle);
        indicator.SetDistance(lengthZ / 2);
        indicator.SetWidth(lengthZ);

        activeIndicator = indicator;
    }

    // Remove all of the plotted points
    public void RemoveIndicators()
    {
        if (!activeIndicator)
            return;

        Destroy(activeIndicator.gameObject);
        activeIndicator = null;
    }
}
