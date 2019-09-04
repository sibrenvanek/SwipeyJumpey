using System.Collections.Generic;
using UnityEngine;

public class TrajectoryPrediction : MonoBehaviour
{
    // Indicator which will be placed at points along the predicted trajectory
    [SerializeField] private GameObject indicator = null;

    // List of the indicators that are currently active
    private List<GameObject> activeIndicators = new List<GameObject>();

    // Update the current trajectory
    public void UpdateTrajectory(Vector2 startPosition, Vector2 startVelocity, Vector2 gravity, int steps)
    {
        RemoveIndicators();

        Vector2[] points = PlotTrajectory(startPosition, startVelocity, gravity, steps);

        foreach (Vector2 point in points)
            activeIndicators.Add(Instantiate(indicator, point, Quaternion.identity));
    }

    // Plot the expected trajectory of the player character
    private Vector2[] PlotTrajectory(Vector2 startPosition, Vector2 startVelocity, Vector2 gravity, int steps)
    {
        List<Vector2> points = new List<Vector2>();

        if (startVelocity.x == 0 && startVelocity.y == 0)
            return points.ToArray();

        float velocity = startVelocity.magnitude;
        float gravityMagnitude = gravity.magnitude;
        float angle = Mathf.Atan(startVelocity.y / startVelocity.x);
        float velocityX = (startVelocity.x < 0) ? Mathf.Cos(angle) * -1 : Mathf.Cos(angle);
        float velocityY = (startVelocity.x < 0) ? Mathf.Sin(angle) * -1 : Mathf.Sin(angle);

        float timestep, timespend;
        timestep = timespend = 0.25f;

        for (int i = 0; i < steps; i++)
        {
            Vector2 position;
            position.x = startPosition.x + velocity * timespend * velocityX;
            position.y = startPosition.y + velocity * timespend * velocityY - gravityMagnitude * Mathf.Pow(timespend, 2) / 2;
            if (position.y < -5)
                break;
            timespend += timestep;
            points.Add(position);
        }

        return points.ToArray();
    }

    // Remove all of the plotted points
    public void RemoveIndicators()
    {
        foreach (GameObject indicator in activeIndicators)
            Destroy(indicator);
    }
}