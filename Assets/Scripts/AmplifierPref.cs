using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]

public class AmplifierPref : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI price;
    private DamageAmplifier _amplifier;
    private CanvasGroup _group;

    public void SetData(DamageAmplifier amplifier)
    {
        _group = GetComponent<CanvasGroup>(); 
        
        _amplifier = amplifier;
        UpdateUI();
    }

    public void UpdateUI()
    {
        level.text = "Level: " + _amplifier.Level;
        price.text = "Price: $" + (int)_amplifier.Price;

        _group.alpha = Clicker.Money >= _amplifier.Price ? 1 : 0.5f;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Clicker.Money < _amplifier.Price)
            return;
        
        Clicker.Instance.AddMoney(-_amplifier.Price);
        _amplifier.LevelUp();
        UpdateUI();
    }
}