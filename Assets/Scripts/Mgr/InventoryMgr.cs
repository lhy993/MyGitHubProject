using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;
public class InventoryMgr : MonoBehaviour
{
    public InventoryUI ui;
    int random;
    int Max;
    int Need_Gold;
    int price;

    public List<InventorySlot> slots = new List<InventorySlot>();

    private void Awake()    
    {
        if (Shared.InventoryMgr == null)
        {
            Shared.InventoryMgr = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void AddItem(Item item, int amount = 1)
    {
        //스택 가능일 때만 기존 찾기
        if (item.isStackable)
        {
            foreach (var slot in slots)
            {
                if (slot.itemInstance.item == item)
                {
                    slot.amount += amount;

                    if (ui != null)
                        ui.UpdateUI();

                    return;
                }
            }
        }

        //새 인스턴스 생성
        ItemInstance newItem = new ItemInstance(item);

        slots.Add(new InventorySlot(newItem, amount));

        if (ui != null)
            ui.UpdateUI();
    }
    public void RemoveItem(InventorySlot slot, int amount = 1)
    {
        slot.amount -= amount;
        if (slot.amount <= 0)
        {
            slots.Remove(slot);
        }

        if (ui != null)
            ui.UpdateUI();
    }
    public bool RemoveItemByItem(Item item, int amount = 1)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].itemInstance.item == item)
            {
                slots[i].amount -= amount;

                if (slots[i].amount <= 0)
                {
                    slots.RemoveAt(i);
                }

                if (ui != null)
                    ui.UpdateUI();

                return true;
            }
        }

        return false; // 아이템 없음
    }
    public void UpgradeItem(InventorySlot slot )
    {
        int currentLevel = slot.itemInstance.upgradeLevel;

        if ( Shared.UserMgr.gold >= Need_Gold)
        {
            Shared.UserMgr.gold -= Need_Gold;
            random = Random.Range(1, Max);
            if (random <= 10)
            {
                slot.itemInstance.upgradeLevel++;
            }
            else if (slot.itemInstance.upgradeLevel >= 5)
            {
                random = Random.Range(1, 3);
                if (random == 1)
                {
                    slot.itemInstance.upgradeLevel--;
                }
            }
        }
        Upgrade(currentLevel);
        Shared.EquipmentMgr.RefreshWeaponDamage();
        if (ui != null)
            ui.UpdateUI();
    }
    public void Upgrade(int UpgradeLevel)
    {
        Max = (UpgradeLevel * 2) + 10;
        Need_Gold = UpgradeLevel* UpgradeLevel;
        UnityEngine.Debug.Log(Need_Gold);
        UnityEngine.Debug.Log(UpgradeLevel);
    }
    public int GetItemCount(Item item)
    {
        int count = 0;

        foreach (var slot in slots)
        {
            if (slot.itemInstance.item == item)
            {
                count += slot.amount;
            }
        }

        return count;
    }
    public bool HasItem(Item item, int amount)
    {
        return GetItemCount(item) >= amount;
    }
    public bool Craft(CraftRecipe recipe)
    {
        // 1. 재료 확인
        foreach (var material in recipe.materials)
        {
            if (GetItemCount(material.item) < material.amount)
            {
                Debug.Log(material.item.itemName + " 부족");
                return false;
            }
        }

        // 2. 재료 제거
        foreach (var material in recipe.materials)
        {
            RemoveItemByItem(material.item, material.amount);
        }

        // 3. 결과 지급
        AddItem(recipe.resultItem, recipe.resultAmount);

        Debug.Log(recipe.resultItem.itemName + " 제작 완료");

        return true;
    }
    public List<ItemData> GetSaveData()
    {
        List<ItemData> data = new List<ItemData>();


        foreach (InventorySlot slot in slots)
        {
            ItemData itemData = new ItemData();


            itemData.itemID =
                slot.itemInstance.item.itemID;


            itemData.amount =
                slot.amount;


            itemData.upgradeLevel =
                slot.itemInstance.upgradeLevel;

            itemData.isEquipped = 
                slot.itemInstance.isEquipped;

            data.Add(itemData);

        }


        return data;
    }
    public void LoadData(List<ItemData> data)
    {
        slots.Clear();

        foreach (ItemData itemData in data)
        {
            // itemID로 Item 찾기
            Item item = ItemDatabase.Instance.GetItem(itemData.itemID);

            if (item == null)
            {
                Debug.LogWarning($"ItemID {itemData.itemID}를 찾을 수 없습니다.");
                continue;
            }

            // ItemInstance 생성
            ItemInstance itemInstance = new ItemInstance(item);
            itemInstance.upgradeLevel = itemData.upgradeLevel;
            itemInstance.isEquipped = itemData.isEquipped;

            // InventorySlot 생성
            InventorySlot slot = new InventorySlot(itemInstance, itemData.amount);

            slots.Add(slot);
            Shared.EquipmentMgr.RefreshWeaponDamage();
        }

        if (ui != null)
            ui.UpdateUI();
    }
}
