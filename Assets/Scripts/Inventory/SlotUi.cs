using UnityEngine;
using UnityEngine.EventSystems;

public class SlotUI : MonoBehaviour, IPointerClickHandler
{
    public InventorySlot slot;
    public ItemDetailUI detailUI;

    public void Setup(InventorySlot slot, ItemDetailUI ui)
    {
        this.slot = slot;
        this.detailUI = ui;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        detailUI.Show(slot);
    }
}