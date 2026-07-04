using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;

    public List<Item> items = new List<Item>();

    private void Awake()
    {
        Instance = this;
    }

    public Item GetItem(int itemID)
    {
        return items.Find(item => item.itemID == itemID);
    }
}