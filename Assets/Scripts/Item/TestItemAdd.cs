using UnityEngine;

public class TestItemAdd : MonoBehaviour
{
    public Item Goblin;
    public Item Skeleton;
    public Item Trunk;
    public Item Sword;
    public Item Gold;
    public Item reviveItem;
    public CraftRecipe ironSwordRecipe;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Shared.InventoryMgr.AddItem(Goblin, 1);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Shared.InventoryMgr.AddItem(Skeleton, 1);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Shared.InventoryMgr.AddItem(Trunk, 1);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Shared.InventoryMgr.AddItem(Sword, 1);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Shared.InventoryMgr.AddItem(Gold, 1);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Shared.InventoryMgr.AddItem(reviveItem, 1);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Shared.InventoryMgr.Craft(ironSwordRecipe);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Shared.SaveMgr.Save();
        }
        
    }
}