using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private float verticalSpeed = 0f;
    [SerializeField] private float horizontalSpeed = 0f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        Vector3 horizontalOffset = new Vector3(Mathf.Sin(Time.time), 0.0f, 0.0f) * horizontalSpeed;
        Vector3 verticalOffset = new Vector3(0.0f, Mathf.Cos(Time.time), 0.0f) * verticalSpeed;
        transform.position = startPosition + horizontalOffset + verticalOffset;
    }
}
