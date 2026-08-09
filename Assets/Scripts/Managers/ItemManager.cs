using System;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    public static Action OnRefreshTextChanged;

    [SerializeField] private ProjectileBuff_Base[] projectileBuffs;

    private void Awake()
    {
        Instance = this;

        projectileBuffs = FindObjectsByType<ProjectileBuff_Base>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );
    }

    private void Start()
    {
        OnRefreshTextChanged += RefreshAllBuffTexts;
    }

    private void OnDestroy()
    {
        OnRefreshTextChanged -= RefreshAllBuffTexts;
    }

    public void RefreshAllBuffTexts()
    {
        if (projectileBuffs == null)
            return;

        foreach (ProjectileBuff_Base buff in projectileBuffs)
        {
            if (buff != null)
                buff.RefreshText();
        }
    }

    public void RefreshAllBuffTextsInvoke() => OnRefreshTextChanged?.Invoke();
}