using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragJump : MonoBehaviour
{
    private Vector2 velocityToApply = new Vector2(0, 0);
    private Vector2 baseMousePosition = new Vector2(0, 0);
    private int direction = 0;
    private Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // OnMouseDown is called when the player taps on the gameobject
    private void OnMouseDown()
    {
        baseMousePosition = Input.mousePosition;
    }

    // OnMouseDrag is called while the player is dragging across the screen
    private void OnMouseDrag()
    {
        velocityToApply.x = (baseMousePosition.x - Input.mousePosition.x) / 5;
        velocityToApply.y = (baseMousePosition.y - Input.mousePosition.y) / 5;

        direction = (baseMousePosition.x < Input.mousePosition.x) ? -1 : 1;
    }

    // OnMouseUp is called when the player stops holding the screen
    private void OnMouseUp()
    {
        rigidbody2d.AddForce(velocityToApply, ForceMode2D.Impulse);
    }
}
