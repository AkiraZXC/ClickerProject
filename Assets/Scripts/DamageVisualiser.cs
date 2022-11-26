using UnityEngine;

public class DamageVisualiser : MonoBehaviour
{
    public static DamageVisualiser Instance;

    [SerializeField] private DamageEffect effectPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void CreateClickEffect(int value)
    {
        var prefab = Instantiate(effectPrefab, transform, false);
        prefab.SetPosition(Input.mousePosition);
        prefab.SetValue(value);
    }
}