using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour 
{
    
    [SerializeField] private CinemachineConfiner cinemachineConfiner = null;

    private Collider2D currentCollider = null;
    private Collider2D previousCollider = null; 
    public void SetConfinerBoundingShape(Collider2D collider)
    {
        if(currentCollider != null) 
            previousCollider = currentCollider;
        currentCollider = collider;

        // if(previousCollider != null)
        //     print(previousCollider.name);
        // print(currentCollider.name);
        cinemachineConfiner.m_BoundingShape2D = currentCollider;
    }

    public void OnExitCollider(Collider2D collider)
    {
        if(currentCollider == collider && previousCollider != null)
        {
            SetConfinerBoundingShape(previousCollider);
        }
    }
}