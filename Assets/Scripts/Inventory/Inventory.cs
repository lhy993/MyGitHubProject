using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class Inventory : MonoBehaviour
{
    public InventoryUI ui;
    public static Inventory instance;
    int random;
    int Max;
    int Need_Gold;
    int price;

    public List<InventorySlot> slots = new List<InventorySlot>();

    private void Awake()    
    {
        if (instance == null)
        {
            instance = this;
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
    public void UpgradeItem(InventorySlot slot /*,Toggle SAFE*/ )
    {
        int currentLevel = slot.itemInstance.upgradeLevel;
        /*if (SAFE.isOn && Shared.UserMgr.Safe >= 1 && Shared.UserMgr.gold >= Need_Gold)
        {
            Shared.UserMgr.Safe -= 1;
            Shared.UserMgr.gold -= Need_Gold;
            random = Random.Range(1, Max);
            if (random <= 10)
            {
                slot.itemInstance.upgradeLevel++;
            }
        }*/
        if (/*!SAFE.isOn &&*/ Shared.UserMgr.gold >= Need_Gold)
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
}
