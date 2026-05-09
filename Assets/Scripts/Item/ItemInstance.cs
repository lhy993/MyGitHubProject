using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInstance
{
    public Item item; // 기본 데이터

    //개별 데이터
    public int upgradeLevel;
    public int sword_dmg
    {
        get
        {
            return item.normalDmg * upgradeLevel;
        }
    }

    public ItemInstance(Item item)
    {
        this.item = item;
        this.upgradeLevel = 1;
    }
}
