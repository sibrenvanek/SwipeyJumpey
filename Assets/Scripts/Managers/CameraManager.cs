using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraManager : MonoBehaviour 
{
    [SerializeField] private float dampingTime = .2f;
    [SerializeField] private float dampingAmount = .15f; 
    [SerializeField] private CinemachineConfiner cinemachineConfiner = null;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera = null;
    private CinemachineFramingTransposer cinemachineCameraBody = null;
    private Collider2D currentCollider = null;
    private Collider2D previousCollider = null; 

    private void Awake() 
    {
        cinemachineCameraBody = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    public void SetConfinerBoundingShape(Collider2D collider)
    {
        StartCoroutine(TriggerConfinerDamping());
        if(currentCollider != null) 
            previousCollider = currentCollider;
        currentCollider = collider;

        cinemachineConfiner.m_BoundingShape2D = currentCollider;
    }

    public void OnExitCollider(Collider2D collider)
    {
        if(currentCollider == collider && previousCollider != null)
        {
            SetConfinerBoundingShape(previousCollider);
        }
    }

    private IEnumerator TriggerConfinerDamping()
    {
        cinemachineConfiner.m_Damping = dampingAmount;
        cinemachineCameraBody.m_XDamping = 0;
        cinemachineCameraBody.m_XDamping = 0;

        yield return new WaitForSeconds(dampingTime);

        cinemachineConfiner.m_Damping = 0;
        cinemachineCameraBody.m_XDamping = 1;
        cinemachineCameraBody.m_XDamping = 1;
    }
}