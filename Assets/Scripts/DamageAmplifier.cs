using UnityEngine;

public class DamageAmplifier
{
    public enum AmplifierType
    {
        PlusClickDamage,
        ClickCritDamage,
        PassiveDamage,
        LevelOfTree
    }
    
    private AmplifierType Type { get; }
    private float Value => InitValue + IncreasePerLevel * Mathf.Clamp(Level - 1, 0, int.MaxValue);
    private float InitValue { get; }
    private float IncreasePerLevel { get; }
    private int Chance { get; }
    private float InitPrice { get; }
    private float IncreasePricePerLevel { get; }
    public int Level { get; private set; }
    public float Price => InitPrice + IncreasePricePerLevel * Level;
    public int Priority { get; }
    public bool IsPassive { get; }

    public DamageAmplifier(AmplifierType type, int priority, bool isPassive,
        float value, float increase, int initPrice, int priceIncrease, int chance = 100)
    {
        Type = type;
        Priority = priority;
        IsPassive = isPassive;
        InitValue = value;
        IncreasePerLevel = increase;
        InitPrice = initPrice;
        IncreasePricePerLevel = priceIncrease;
        Chance = chance;

        Level = PlayerPrefs.GetInt("Level" + Type.ToString(), 0);
    }

    public float CalculateDamage(float initDamage)
    {
        if (Level == 0)
        {
            return initDamage;
        }

        switch (Type)
        {
            case AmplifierType.PlusClickDamage:
                return initDamage + Value;

            case AmplifierType.PassiveDamage:
                return initDamage + Value;

            case AmplifierType.ClickCritDamage:
                if (Random.Range(0, 100) >= Chance) return initDamage;
                return initDamage * Value;
            
            case AmplifierType.LevelOfTree:
                return initDamage * Value * 2;
            default:
                return initDamage;
        }
    }
    
    public void LevelUp()
    {
        Level++;
        PlayerPrefs.SetInt("Level" + Type, value:Level);
    }
}