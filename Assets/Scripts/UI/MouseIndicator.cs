using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseIndicator : MonoBehaviour
{
    private SpriteRenderer spriteRenderer = null;
    private Color defaultColor = Color.white;
    [SerializeField] private Color cancelColor = Color.black;
    private bool cancelActive = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }

    private void OnMouseEnter()
    {
        if (cancelActive)
            return;

        cancelActive = true;
        spriteRenderer.color = cancelColor;
    }

    private void OnMouseExit()
    {
        if (!cancelActive)
            return;

        cancelActive = false;
        spriteRenderer.color = defaultColor;
    }

    public void SetCancelDistance(float distance)
    {
        Component.FindObjectOfType<CircleCollider2D>().radius = distance / 2;
    }
}
