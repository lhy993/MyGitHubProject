using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Forge : MonoBehaviour, iinteraction
{
    public UI_Battle Ui_Battle;
    public GameObject IMAGE;

    public CraftRecipe GoblinSwordRecipe;
    public CraftRecipe SkeletonSwordRecipe; 
    public TextMeshPro TIP;
    public void Interact()
    {   
        IMAGE.SetActive(true);
    }

    public void Text()
    {
        Ui_Battle.InteractionText("제작 하기");
    }

    public void Tip()
    {
        TIP.text = "Forge";
    }
    public void ReturnBtn()
    {
        IMAGE.SetActive(false);
    }
    public void GoblinSwordCraft()
    {
        Shared.InventoryMgr.Craft(GoblinSwordRecipe);
    }
    public void SkeletonSwordCraft()
    {
        Shared.InventoryMgr.Craft(SkeletonSwordRecipe);
    }
    public void TextOn()
    {
        TIP.gameObject.SetActive(true);
    }
    public void TextOff()
    {
        TIP.gameObject.SetActive(false);
    }
}
