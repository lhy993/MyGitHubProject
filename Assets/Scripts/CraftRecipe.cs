using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Craft Recipe")]
public class CraftRecipe : ScriptableObject
{
    public List<CraftMaterial> materials;

    public Item resultItem;
    public int resultAmount;
}