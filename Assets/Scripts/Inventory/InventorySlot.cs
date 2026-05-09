[System.Serializable]
public class InventorySlot
{
    public ItemInstance itemInstance;
    public int amount;

    public InventorySlot(ItemInstance itemInstance, int amount)
    {
        this.itemInstance = itemInstance;
        this.amount = amount;
    }
}