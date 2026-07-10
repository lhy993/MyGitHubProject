using UnityEngine;
using static Item;

public class EquipmentMgr : MonoBehaviour
{

    [HideInInspector]
    public InventorySlot equippedWeapon;

    private void Awake()
    {
        if (Shared.EquipmentMgr == null)
        {
            Shared.EquipmentMgr = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RefreshWeaponDamage()
    {
        foreach (InventorySlot slot in Shared.InventoryMgr.slots)
        {
            if (slot.itemInstance.isEquipped)
            {
                Shared.StatMgr.Sword_Dmg = slot.itemInstance.sword_dmg;
                return;
            }
        }

        Shared.StatMgr.Sword_Dmg = 1;
    }
    public void EquipWeapon(InventorySlot slot)
    {
        // 모든 무기의 장착 해제
        foreach (InventorySlot s in Shared.InventoryMgr.slots)
        {
            if (s.itemInstance.item.itemType == ItemType.Weapon)
            {
                s.itemInstance.isEquipped = false;
            }
        }

        // 선택한 무기 장착
        slot.itemInstance.isEquipped = true;

        // 공격력 적용
        RefreshWeaponDamage();
    }

    public int GetWeaponDamage()
    {
        if (equippedWeapon == null)
            return 0;

        return equippedWeapon.itemInstance.sword_dmg;
    }
}