using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldPreview : MonoBehaviour
{
    [SerializeField] private int sceneIndex = 0;
    private SpriteRenderer spriteRenderer = null;
    public bool active { private get; set; }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSceneIndex(int sceneIndex)
    {
        this.sceneIndex = sceneIndex;
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    private void OnMouseDown()
    {
        if (active)
            SceneManager.LoadScene(sceneIndex);
    }
}
