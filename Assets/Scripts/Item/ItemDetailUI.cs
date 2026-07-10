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

        if (slot.itemInstance.item.isStackable)
        {
            upgradeButton.SetActive(false);
            DmgText.gameObject.SetActive(false);
        }
        else
        {
            upgradeButton.SetActive(true);
            DmgText.gameObject.SetActive(true);
        }

        equipButton.SetActive(slot.itemInstance.item.itemType == ItemType.Weapon);

        Refresh();   //마지막에 한 번만 호출
    }
    public void OnClickEquip()
    {
        Shared.EquipmentMgr.EquipWeapon(currentSlot);

        Refresh();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    //강화 버튼
    public void OnClickUpgrade()
    {
        Shared.InventoryMgr.UpgradeItem(currentSlot);
        Refresh();
    }

    //판매 버튼
    public void OnClickSell()
    {
        if (!currentSlot.itemInstance.isEquipped)
        {
            int price = currentSlot.itemInstance.item.price;

            Shared.InventoryMgr.RemoveItem(currentSlot, 1);
            Shared.UserMgr.gold += price;
        }
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

        equipButtonText.text =
            currentSlot.itemInstance.isEquipped ? "장착됨" : "장착";
    }

}