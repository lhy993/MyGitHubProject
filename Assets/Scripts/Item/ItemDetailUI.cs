using UnityEngine;
using UnityEngine.UI;
using static Item;

public class ItemDetailUI : MonoBehaviour
{
    public Image icon;
    public Text nameText;
    public Text upgradeText;
    public GameObject upgradeButton;
    public Text DmgText;
    private InventorySlot currentSlot;
    public GameObject equipButton;
    public Text equipButtonText;

    public void Show(InventorySlot slot)
    {
        currentSlot = slot;
        gameObject.SetActive(true);

        icon.sprite = slot.itemInstance.item.icon;
        nameText.text = slot.itemInstance.item.itemName;

        //스택 여부에 따라 UI 변경
        if (slot.itemInstance.item.isStackable)
        {
            upgradeButton.SetActive(false); //버튼 숨김
            DmgText.gameObject.SetActive(false);
        }
        else
        {
            upgradeText.text = "강화: +" + slot.itemInstance.upgradeLevel;
            DmgText.text = $"{slot.itemInstance.sword_dmg}dmg";
            upgradeButton.SetActive(true); //버튼 보이기
            DmgText.gameObject.SetActive(true);
        }
        if (slot.itemInstance.item.itemType == ItemType.Weapon)
        {
            equipButton.SetActive(true);

            //장착 여부 검사
            if (EquipmentManager.instance.equippedWeapon == slot)
            {
                equipButtonText.text = "장착됨";
            }
            else
            {
                equipButtonText.text = "장착";
            }
        }
        else
        {
            equipButton.SetActive(false);
        }
    }
    public void OnClickEquip()
    {
        EquipmentManager.instance.EquipWeapon(currentSlot);

        Refresh();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    //강화 버튼
    public void OnClickUpgrade()
    {
        Inventory.instance.UpgradeItem(currentSlot);
        Refresh();
    }

    //판매 버튼
    public void OnClickSell()
    {
        int price = currentSlot.itemInstance.item.price;

        Inventory.instance.RemoveItem(currentSlot, 1);
        Shared.UserMgr.gold += price;

        if (currentSlot.amount <= 0)
        {
            Hide();
        }
        else
        {
            Refresh();
        }
    }

    void Refresh()
    {
        if (currentSlot == null) return;

        upgradeText.text = "강화: +" + currentSlot.itemInstance.upgradeLevel;

        DmgText.text = $"{currentSlot.itemInstance.sword_dmg} dmg";

        if (EquipmentManager.instance.equippedWeapon == currentSlot)
        {
            equipButtonText.text = "장착됨";
        }
        else
        {
            equipButtonText.text = "장착";
        }
    }

}