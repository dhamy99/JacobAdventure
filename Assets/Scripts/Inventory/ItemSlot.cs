using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    private ItemInfo currentItemInfo;
    [SerializeField] private RectTransform itemBackground;

    private void Awake()
    {
        currentItemInfo = GetComponentInChildren<ItemInfo>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Transform itemDroppedTransform = eventData.pointerDrag.transform;
        ItemInfo newItemInfo = itemDroppedTransform.GetComponent<ItemInfo>();

        //there is already data in this slot -> nothing
        if (itemBackground.childCount > 0)
        {
            currentItemInfo.transform.SetParent(newItemInfo.InitParent);
            currentItemInfo.transform.localPosition = Vector3.zero;

            //update destination slot with currentItemInfo data
            newItemInfo.InitParent.GetComponentInParent<ItemSlot>().currentItemInfo = currentItemInfo;

        }

        itemDroppedTransform.SetParent(itemBackground);
        itemDroppedTransform.localPosition = Vector3.zero;
        currentItemInfo = newItemInfo;
    }
}