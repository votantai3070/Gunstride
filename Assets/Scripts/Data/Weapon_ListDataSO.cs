using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon List", menuName = "Hybrid Casual/Weapon Data/Weapon List")]
public class Weapon_ListDataSO : ScriptableObject
{
    public WeaponDataSO[] weaponList;

    public WeaponDataSO GetWeaponById(string saveId)
    {
        return weaponList.FirstOrDefault(item => item != null && item.weaponID == saveId);
    }

#if UNITY_EDITOR
    [ContextMenu("Auto-fill with all WeaponDataSO")]
    public void CollectItemsData()
    {
        string[] guids = AssetDatabase.FindAssets("t:WeaponDataSO");

        weaponList = guids
            .Select(guid => AssetDatabase.LoadAssetAtPath<WeaponDataSO>(AssetDatabase.GUIDToAssetPath(guid)))
            .Where(item => item != null)
            .ToArray();

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
#endif
}
