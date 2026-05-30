using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forge : MonoBehaviour, iinteraction
{
    public UI_Battle Ui_Battle;
    public GameObject IMAGE;

    public CraftRecipe GoblinSwordRecipe;
    public void Interact()
    {   
        IMAGE.SetActive(true);
    }

    public void Text()
    {
        Ui_Battle.InteractionText("제작 하기");
    }

    public void Tip(bool e)
    {
    }
    public void ReturnBtn()
    {
        IMAGE.SetActive(false);
    }
    public void GoblinSwordCraft()
    {
        Inventory.instance.Craft(GoblinSwordRecipe);
    }

}
