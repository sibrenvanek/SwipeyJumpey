using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CirclingEnergy : MonoBehaviour
{
    [SerializeField] private float circleInterval = 1f;
    [SerializeField] private Animator animator = null;
    private bool active = true;
    private SpriteRenderer spriteRenderer = null;

    private void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    public void StopCircling()
    {
        spriteRenderer.enabled = false;
        active = false;
    }

    public void StartCircling()
    {
        if(animator != null)
        {
            spriteRenderer.enabled = true;
            active = true;
            SetRandomRotation();
            StartCoroutine(Circle());
        }
        else
        {
            Debug.LogWarning("Cannot start circling because animator is null.");
        }
    }

    public void SetRandomRotation()
    {
        transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, Random.Range(0,359));
    }

    private IEnumerator Circle()
    {
        if(active)
        {
            yield return new WaitForSeconds(circleInterval);
            animator.SetTrigger("Circle");
            StartCoroutine(Circle());
        }else{
            yield return null;
        }
    }
}
