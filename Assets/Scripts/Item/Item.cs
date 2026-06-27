using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public int itemID;

    public string itemName;
    public Sprite icon;
    public bool isStackable;
    public int price;
    public int normalDmg;
    public ItemType itemType;

    public enum ItemType
    {
        Consumable,
        Weapon
    }
}