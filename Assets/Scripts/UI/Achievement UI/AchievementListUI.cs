using UnityEngine;

public class AchievementListUI : MonoBehaviour
{
    [SerializeField] private Transform acheivementSlotParent;
    private AchievementSlotUI[] activeSlots;

    private void Start()
    {

    }

    public void RefreshAllSlots()
    {
        foreach (AchievementSlotUI slot in activeSlots)
        {
            if (slot != null)
            {
                slot.RefreshUI();
            }
        }
    }

    public void InitialzeAchievementSlot(string achievementName, Achievement_ListDataSO list)
    {
        for (int i = 0; i < list.achievementList.Length; i++)
        {
            GameObject slot = ObjectPool.Instance.Spawn(achievementName, acheivementSlotParent.position, Quaternion.identity, acheivementSlotParent);
            slot.GetComponent<AchievementSlotUI>().SetAchievementData(list.achievementList[i]);
        }

        activeSlots = acheivementSlotParent.GetComponentsInChildren<AchievementSlotUI>(true);
    }
}
