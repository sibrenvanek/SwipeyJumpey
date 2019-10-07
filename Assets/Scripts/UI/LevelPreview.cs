using UnityEngine;

public class LevelPreview : MonoBehaviour
{
    [SerializeField] private string sceneName = "";
    [SerializeField] private Vector2 activeSize = Vector2.one, inactiveSize = Vector2.one;
    [SerializeField] private Color enabledColor = new Color(1f, 1f, 1f);
    [SerializeField] private Color disabledColor = new Color(0.8f, 0.8f, 0.8f);
    [SerializeField] private GameObject finishIndicator = null;
    private SpriteRenderer spriteRenderer = null;
    private Level level = null;

    void Awake()
    {
        level = ProgressionManager.Instance.GetLevel(sceneName);
        finishIndicator.SetActive(level.completed);
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = inactiveSize;
        SetColors();
    }

    public Level GetLevel()
    {
        return level;
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

    private void SetColors()
    {
        finishIndicator.SetActive(level.completed);

        if (level.unlocked)
            gameObject.GetComponent<SpriteRenderer>().color = enabledColor;
        else
            gameObject.GetComponent<SpriteRenderer>().color = disabledColor;
    }
}
