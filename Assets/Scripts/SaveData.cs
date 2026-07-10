using System;
using System.Collections.Generic;


[Serializable]
public class SaveData
{
    public int level;
    public float need;
    public float exp;

    public float hp;
    public float mp;

    public int gold;

    public List<ItemData> inventory;


    public bool[] clearStage;

    public StatData stat;
}



[Serializable]
public class ItemData
{
    public int itemID;

    public int amount;


    public int upgradeLevel;


    public bool isEquipped;
}



[Serializable]
public class StatData
{
    public int dmgStat;
    public int mpStat;
    public int defStat;
    public int hpStat;

    public int statPoint;
}