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
        float angle = CalculateAngle(startVelocity);

        Vector2 distance = CalculateDistance(velocity, angle, time);

        float lengthZ = Mathf.Sqrt(Mathf.Pow(distance.x, 2) + Mathf.Pow(distance.y, 2));

        if (startVelocity.x < 0)
            distance.x *= -1;

        if (startVelocity.y < 0)
            distance.y *= -1;

        activeIndicator = CreateIndicator(angle, lengthZ);
    }

    // Remove all of the plotted points
    public void RemoveIndicators()
    {
        if (!activeIndicator)
            return;

        Destroy(activeIndicator.gameObject);
        activeIndicator = null;
    }

    Vector2 CalculateMaxVelocity(float maxVelocity, float angle)
    {
        float x, y;
        float angleRadiant = angle * Mathf.Deg2Rad;
        x = Mathf.Cos(angleRadiant) * maxVelocity;
        y = Mathf.Sin(angleRadiant) * maxVelocity;

        return new Vector2(x, y);
    }

    public float CalculateAngle(Vector2 velocity)
    {
        float angle = Mathf.Rad2Deg * Mathf.Atan(velocity.y / velocity.x);

        if (velocity.x < 0 && velocity.y < 0)
            angle -= 180;
        else if (velocity.x < 0 && velocity.y >= 0)
            angle += 180;

        return angle;
    }

    public Vector2 LimitJumpVelocity(Vector2 jumpVelocity, float angle, float maxVelocity)
    {
        Vector2 maxVelocityVector = CalculateMaxVelocity(maxVelocity, angle);
        if (jumpVelocity.x > 0)
        {
            if (maxVelocityVector.x < jumpVelocity.x)
            {
                jumpVelocity.x = maxVelocityVector.x;
            }
        }
        else
        {
            if (-maxVelocityVector.x < jumpVelocity.x)
            {
                jumpVelocity.x = -maxVelocityVector.x;
            }
        }
        if (jumpVelocity.y > 0)
        {
            if (maxVelocityVector.y < jumpVelocity.y)
            {
                jumpVelocity.y = maxVelocityVector.y;
            }
        }
        else
        {
            if (-maxVelocityVector.y < jumpVelocity.y)
            {
                jumpVelocity.y = -maxVelocityVector.y;
            }
        }
        return jumpVelocity;
    }

    Vector2 CalculateDistance(float velocity, float angle, float time)
    {
        float velocityX = Mathf.Cos(angle);
        float velocityY = Mathf.Sin(angle);

        Vector2 distance;
        distance.x = Mathf.Abs(velocity * time * velocityX);
        distance.y = Mathf.Abs(velocity * time * velocityY);

        return distance;
    }

    Indicator CreateIndicator(float angle, float lengthZ)
    {
        Indicator indicator = Instantiate(indicatorPrefab, transform.position, Quaternion.identity);
        indicator.transform.Rotate(0, 0, angle);
        indicator.SetDistance(lengthZ / 2);
        indicator.SetWidth(lengthZ);
        return indicator;
    }
}
