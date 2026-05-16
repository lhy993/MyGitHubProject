using UnityEngine;
using UnityEngine.EventSystems;

public class Inventoryslot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

        if (draggableItem != null)
            draggableItem.parentAfterDrag = transform;
    }
}