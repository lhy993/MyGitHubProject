using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public string InteractionText => "Press E to open chest";

    public void Interact(GameObject player)
    {
        Debug.Log("Chest opened!");
        // 상자 열기 
    }
}