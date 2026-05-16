using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public string InteractionText => "Press E to open door";

    public void Interact(GameObject player)
    {
        Debug.Log("Door opened!");
        // ¹® ¿­±â 
    }
}