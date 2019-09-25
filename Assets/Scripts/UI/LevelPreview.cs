using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPreview : MonoBehaviour
{
    [SerializeField] private int sceneIndex = 0;
    [SerializeField] private Vector2 activeSize = Vector2.one, inactiveSize = Vector2.one;
    private SpriteRenderer spriteRenderer = null;
    private bool active = false;
    private LevelSelecter levelSelecter = null;

    void Start()
    {
        levelSelecter = GetComponentInParent<LevelSelecter>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = inactiveSize;
    }

    public void SetSceneIndex(int sceneIndex)
    {
        this.sceneIndex = sceneIndex;
    }

    public int GetSceneIndex()
    {
        return sceneIndex;
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void SetActivated()
    {
        active = true;
        transform.localScale = activeSize;
    }

    public void SetInActive()
    {
        active = false;
        transform.localScale = inactiveSize;
    }

    private void OnMouseDown()
    {
        if (active)
            SceneManager.LoadScene(sceneIndex);
        else
            levelSelecter.SetActiveIndexByScene(sceneIndex);
    }
}
