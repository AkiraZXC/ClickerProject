using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class Clicker : MonoBehaviour
{
    public static Clicker Instance;
    [SerializeField] private TextMeshProUGUI money;


    public static float Money
    {
        get => PlayerPrefs.GetFloat("Money", 0);
        private set => PlayerPrefs.SetFloat("Money", value);
    }

    [SerializeField] private List<AmplifierPref> amplifierPrefs;
    private List<DamageAmplifier> _amplifiers;


    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        _amplifiers = new List<DamageAmplifier>()
        {
            new(DamageAmplifier.AmplifierType.PlusClickDamage, 0, false, 2, 1.5f, 10, 75),
            new(DamageAmplifier.AmplifierType.ClickCritDamage, 100, false, 2, 2f, 15, 100, 25),
            new(DamageAmplifier.AmplifierType.PassiveDamage, 0, true, 2, 1.25f, 20, 125),
            new(DamageAmplifier.AmplifierType.LevelOfTree, 101, true, 1, 100, 2500, 2500)
        };

        for (var i = 0; i < amplifierPrefs.Count; i++)
        {
            amplifierPrefs[i].SetData(_amplifiers[i]);
        }

        UpdateUI();
        StartCoroutine(PassiveMoney());
    }


    private IEnumerator PassiveMoney()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            DamageTarget(GetPassiveDamage());
        }
    }

    public void Click()
    {
        var damage = GetClickDamage();
        DamageTarget(GetClickDamage()); 
        DamageVisualiser.Instance.CreateClickEffect((int) damage);
    }
    
    private float GetClickDamage()
    {
        float moneyPerClick = 1;

        var sortedAmplifiers = _amplifiers.FindAll(x => !x.IsPassive);
        sortedAmplifiers.Sort((x, y) => x.Priority.CompareTo(y.Priority));

        foreach (var amplifier in sortedAmplifiers)
        {
            moneyPerClick = amplifier.CalculateDamage(moneyPerClick);
            return moneyPerClick;
        }
        
        return moneyPerClick;
    }

    
    private float GetPassiveDamage()
    {
        var sortedAmplifiers = _amplifiers.FindAll(x => x.IsPassive);
        sortedAmplifiers.Sort((x,y) => x.Priority.CompareTo(y.Priority));

        return sortedAmplifiers.Aggregate<DamageAmplifier, float>(0, (current, amplifier) => amplifier.CalculateDamage(current));
    }
    
    

    
    private void DamageTarget(float damage)
    {
        AddMoney(damage);
    }


    private void UpdateUI()
    {
        money.text = "Money:" + (int)Money;
    }

    public void AddMoney(float value)
    {
        Money += value; 
        UpdateUI();

        foreach (var pref in amplifierPrefs)
        {
            pref.UpdateUI();
        }
    }
}