using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemCountText;

    private Canvas canvas;
    private RectTransform itemRectTransform;
    private CanvasGroup canvasGroup;
    private Transform initParent; //orgin slot it's anchored to
    private Vector3 initPosition; //original position it's been anchored to
    private int itemCount;

    private ItemSO currentData;

    public Transform InitParent { get => initParent; }
    public Vector3 InitPosition { get => initPosition; }

    private void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        itemRectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

    }

    public void FeedData(ItemSO newItem)
    {
        itemImage.sprite = newItem.itemIcon;
        itemNameText.text = newItem.itemName;
        currentData = newItem;
        UpdateStackItem();
    }

    //starts when we begin dragging the element
    public void OnBeginDrag(PointerEventData eventData)
    {
        //stores original position and local position
        initParent = itemRectTransform.parent;
        initPosition = itemRectTransform.localPosition;

        //parent to canvas for a little bit
        itemRectTransform.SetParent(canvas.transform);

        //little transparency while moving the object
        canvasGroup.alpha = 0.5f;
        //stops reading another item's positions
        canvasGroup.blocksRaycasts = false;
    }

    //while dragging (ontriggerstay simile)
    public void OnDrag(PointerEventData eventData)
    {
        //generating transform moving by mouse position
        itemRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    //starts when we end dragging the element
    public void OnEndDrag(PointerEventData eventData)
    {
        //restores transparency and begins reading another item's positions
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;

        //failed dropping
        if (itemRectTransform.parent == canvas.transform)
        {
            itemRectTransform.SetParent(initParent);
            itemRectTransform.localPosition = initPosition;
        }
    }

    public void UpdateStackItem()
    {
        itemCount++;
        itemCountText.text = "x" + itemCount;
    }

    public void UseItem()
    {
        //PLACE TO ORDER HOW TO USE ITEMS
        if (currentData)
        {
            Debug.Log("Using " + currentData.itemName);
        }
    }
}
