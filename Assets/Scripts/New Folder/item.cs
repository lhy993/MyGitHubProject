using UnityEngine;

public class item : MonoBehaviour, IInteractable
{
    public string InteractionText => "Press E to pick up item";

    public void Interact(GameObject player)
    {
        Debug.Log("Item picked up!");
        // 아이템 줍기
    }
}