using UnityEngine;

public class ShugarCane : MonoBehaviour
{
    private Vector2 _normalScale;
    private Vector2 _clickedScale;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite sprite1;
    
    private static int ClickCount
    {
        get => PlayerPrefs.GetInt("ClickCount", 0);
        set => PlayerPrefs.SetInt("ClickCount", value);
    }

    private void Awake()
    {
        _normalScale = transform.localScale;
        _clickedScale = new Vector2(_normalScale.x - .05f, _normalScale.y - .05f);
        UpdateSprite();
    }

    private void OnMouseDown()
    {
        transform.localScale = _clickedScale;
        ClickCount++;
    }

    private void OnMouseUp()
    {
        transform.localScale = _normalScale;
        Clicker.Instance.Click();
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if(ClickCount >= 500)
        {
            spriteRenderer.sprite = sprite1;
        }
    }
}