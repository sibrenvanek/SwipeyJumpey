using System.Collections.Generic;
using UnityEngine;

public class TrajectoryPrediction : MonoBehaviour
{
    /*************
     * VARIABLES *
     *************/

    /**General**/
    [SerializeField] private GameObject indicatorPrefab = null;
    private List<GameObject> activeIndicators = new List<GameObject>();

    /*************
     * FUNCTIONS *
     *************/

    /**Trajectory**/

    // Update the current trajectory
    public void UpdateTrajectory(Vector2 startPosition, Vector2 startVelocity, Vector2 gravity, float time)
    {
        RemoveIndicators();

        PlotTrajectory(startPosition, startVelocity, gravity, time);

        //foreach (Vector2 point in points)
        //    activeIndicators.Add(Instantiate(indicator, point, Quaternion.identity));
    }

    // Plot the expected trajectory of the player character
    private void PlotTrajectory(Vector2 startPosition, Vector2 startVelocity, Vector2 gravity, float time)
    {
        Debug.Log(startVelocity);
        if (startVelocity.x == 0 && startVelocity.y == 0)
            return;

        Vector2[] points = new Vector2[2] { transform.position, transform.position };

        float velocity = startVelocity.magnitude;
        float gravityMagnitude = gravity.magnitude;
        float angle = Mathf.Rad2Deg * Mathf.Atan(startVelocity.y / startVelocity.x) + 90;
        float velocityX = (startVelocity.x < 0) ? Mathf.Cos(angle) * -1 : Mathf.Cos(angle);
        float velocityY = (startVelocity.x < 0) ? Mathf.Sin(angle) * -1 : Mathf.Sin(angle);

        Vector2 position;
        position.x = startPosition.x + velocity * time * velocityX;
        position.y = startPosition.y + velocity * time * velocityY;

        float lengthX = position.x - startPosition.x;
        float lengthY = position.y - startPosition.y;

        float length = Mathf.Sqrt(Mathf.Pow(lengthX, 2) + Mathf.Pow(lengthY, 2));

        GameObject indicator = Instantiate(indicatorPrefab, startPosition, Quaternion.identity);
        indicator.transform.localScale = new Vector2(1, length);
        indicator.transform.position = new Vector2(startPosition.x + 2, startPosition.y + 2);
        indicator.transform.Rotate(0, 0, angle);

        activeIndicators.Add(indicator);
    }

    // Remove all of the plotted points
    public void RemoveIndicators()
    {
        foreach (GameObject indicator in activeIndicators)
            Destroy(indicator);
    }
}
