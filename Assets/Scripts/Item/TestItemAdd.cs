using UnityEngine;

public class TestItemAdd : MonoBehaviour
{
    public Item Goblin;
    public Item Skeleton;
    public Item Trunk;
    public Item Sword;
    public Item Gold;
    public Item reviveItem;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Inventory.instance.AddItem(Goblin, 1);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Inventory.instance.AddItem(Skeleton, 1);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Inventory.instance.AddItem(Trunk, 1);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Inventory.instance.AddItem(Sword, 1);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Inventory.instance.AddItem(Gold, 1);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Inventory.instance.AddItem(reviveItem, 1);
        }
    }
}