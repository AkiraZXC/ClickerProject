using TMPro;
using UnityEngine;


public class DamageEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private CanvasGroup group;

    private void Update()
    {
        group.alpha = Mathf.Lerp(group.alpha, 0, Time.deltaTime);
        transform.position += Vector3.up * (Time.deltaTime * 5);
        
        if(group.alpha < 0.01)
            Destroy(gameObject);
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    public void SetValue(int value)
    {
        text.text = "+" + value;
    }
}
