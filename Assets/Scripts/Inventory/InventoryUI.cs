using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform slotParent;
    public GameObject slotPrefab;
    public ItemDetailUI detailUI;
    private void Start()
    {
        Inventory.instance.ui = this;
        UpdateUI();

    }

    public void UpdateUI()
    {
        // 기존 슬롯 삭제
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        // 새로 생성
        foreach (var slot in Inventory.instance.slots)
        {
            GameObject obj = Instantiate(slotPrefab, slotParent);
            Transform icon = obj.transform.Find("Icon");

            if (icon == null)
            {
                Debug.LogError("Icon null");
            }
            obj.transform.Find("Icon").GetComponent<Image>().sprite = slot.itemInstance.item.icon;
            Text amountText = obj.transform.Find("Amount").GetComponent<Text>();

            if (slot.itemInstance.item.isStackable)
            {
                //스택 아이템 → 개수 표시
                amountText.text = slot.amount.ToString();
            }
            else
            {
                //장비 → 강화 표시
                amountText.text = "+" + slot.itemInstance.upgradeLevel;
            }

            SlotUI slotUI = obj.GetComponent<SlotUI>();
            slotUI.Setup(slot, detailUI);
        }
    }
}