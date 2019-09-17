using UnityEngine;

public class RoomEntrance : MonoBehaviour
{    
    [SerializeField] private bool horizontal = true;
    [SerializeField] private bool positive = true;
    [SerializeField] private float offset = 2f;
    private Collider2D entranceCollider = null;

    private void Awake() 
    {
        entranceCollider = GetComponent<Collider2D>();     
    }

    public void Enter(Transform player)
    {
        if(horizontal)
        {
            if(positive)
                player.position += (Vector3.right * offset);
            else
                player.position += (Vector3.left * offset);
        }else{
            if(positive)
                player.position += (Vector3.up * offset);
            else
                player.position += (Vector3.down * offset);
        }
    }

    public void Open()
    {
        entranceCollider.enabled = true;
    }

    public void Close()
    {
        entranceCollider.enabled = false;
    }
}
