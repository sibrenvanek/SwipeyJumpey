using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldPreview : MonoBehaviour
{
    private SpriteRenderer spriteRenderer = null;
    public int sceneIndex { private get; set; }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    private void OnMouseDown()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
