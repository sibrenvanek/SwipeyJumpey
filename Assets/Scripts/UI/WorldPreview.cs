using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldPreview : MonoBehaviour
{
    private SpriteRenderer spriteRenderer = null;
    [SerializeField] private int sceneIndex = 0;
    [SerializeField] private Vector2 activeSize, inactiveSize = Vector2.one;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = inactiveSize;
    }

    public void SetSceneIndex(int sceneIndex)
    {
        this.sceneIndex = sceneIndex;
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void SetActivated()
    {
        transform.localScale = activeSize;
    }

    public void SetInActive()
    {
        transform.localScale = inactiveSize;
    }

    private void OnMouseDown()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
