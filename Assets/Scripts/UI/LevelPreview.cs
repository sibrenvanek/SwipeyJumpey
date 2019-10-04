using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPreview : MonoBehaviour
{
    [SerializeField] private int sceneIndex = 0;
    [SerializeField] private Vector2 activeSize = Vector2.one, inactiveSize = Vector2.one;
    [SerializeField] private string levelName = "";
    [SerializeField] private Color enabledColor = new Color(1f, 1f, 1f);
    [SerializeField] private Color disabledColor = new Color(0.8f, 0.8f, 0.8f);
    [SerializeField] private int amountCollectables = 0;
    private SpriteRenderer spriteRenderer = null;
    private bool active = false;
    private bool unlocked = false;
    private LevelSelecter levelSelecter = null;

    void Start()
    {
        levelSelecter = GetComponentInParent<LevelSelecter>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = inactiveSize;
    }

    public int GetAmountCollectables()
    {
        return amountCollectables;
    }

    public void SetSceneIndex(int sceneIndex)
    {
        this.sceneIndex = sceneIndex;
    }

    public int GetSceneIndex()
    {
        return sceneIndex;
    }

    public string GetName()
    {
        return levelName;
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

    public void SetUnlocked(bool value)
    {
        unlocked = value;
        if (value)
        {
            gameObject.GetComponent<SpriteRenderer>().color = enabledColor;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = disabledColor;
        }
    }

    public bool GetUnlocked()
    {
        return unlocked;
    }

    private void OnMouseDown()
    {
        if (unlocked && !active)
        {
            levelSelecter.SetActiveIndexByScene(sceneIndex);
        }
    }
}
