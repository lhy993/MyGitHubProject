using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;

    public InventorySlot equippedWeapon;

    private void Awake()
    {
        instance = this;
    }

    public void EquipWeapon(InventorySlot slot)
    {
        equippedWeapon = slot;

        Debug.Log(slot.itemInstance.item.itemName + " ÀåÂø");

        Shared.StatMgr.Sword_Dmg = 0;
    }

    public int GetWeaponDamage()
    {
        if (equippedWeapon == null)
            return 0;

        return equippedWeapon.itemInstance.sword_dmg;
    }
}