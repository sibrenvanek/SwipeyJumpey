using UnityEngine;

public class LevelPreview : MonoBehaviour
{
    [SerializeField] private int sceneIndex = 0;
    [SerializeField] private Vector2 activeSize = Vector2.one, inactiveSize = Vector2.one;
    [SerializeField] private string levelName = "";
    [SerializeField] private Color enabledColor = new Color(1f, 1f, 1f);
    [SerializeField] private Color disabledColor = new Color(0.8f, 0.8f, 0.8f);
    [SerializeField] private int amountOfMainCollectables = 0;
    [SerializeField] private int amountOfSideCollectables = 0;
    [SerializeField] private GameObject finishIndicator = null;
    private SpriteRenderer spriteRenderer = null;
    private bool active = false;
    private bool unlocked = false;
    private bool completed = false;
    private LevelSelecter levelSelecter = null;

    void Start()
    {
        finishIndicator.SetActive(completed);
        levelSelecter = GetComponentInParent<LevelSelecter>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = inactiveSize;
    }

    public int GetAmountOfMainCollectables()
    {
        return amountOfMainCollectables;
    }

    public string GetLevelName()
    {
        return levelName;
    }

    public int GetAmountOfSideCollectables()
    {
        return amountOfSideCollectables;
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

    public void SetColors(bool unlocked, bool completed)
    {
        this.unlocked = unlocked;
        this.completed = completed;

        finishIndicator.SetActive(completed);

        if (unlocked)
            gameObject.GetComponent<SpriteRenderer>().color = enabledColor;
        else
            gameObject.GetComponent<SpriteRenderer>().color = disabledColor;
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
