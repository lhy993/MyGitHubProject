using System;
using System.Collections.Generic;


[Serializable]
public class SaveData
{
    public string playerName;


    // 플레이어 정보
    public int level;
    public int exp;

    public int hp;
    public int mp;


    // 인벤토리
    public List<ItemData> inventory;


    // 진행도
    public int clearStage;


    // 스탯
    public StatData stat;
}



[Serializable]
public class ItemData
{
    public int itemID;

    public int count;


    // 강화
    public int reinforceLevel;


    // 장착 여부
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