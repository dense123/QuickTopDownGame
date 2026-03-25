using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    Slot slot;
    CanvasGroup canvasGroup;
    Transform canvasTopMostParent;


    private void Start()
    {
        if (GetComponentInParent<Slot>())
        {
            slot = GetComponentInParent<Slot>();
            slot.CurrentItemInSlot = this.gameObject;
        }
        else
            Debug.LogWarning($"{transform.parent.name} doesn't contain slot");

        canvasGroup = GetComponent<CanvasGroup>();
        //if (transform.root.GetChild(0).GetComponent<Canvas>() != null)
        //    canvasTopMostParent = transform.root.GetChild(0);
        //else
        //    Debug.LogWarning($"transform.root.GetChild(0) is not canvas, it's {transform.root.GetChild(0).name}");
    if (transform.root.GetComponent<Canvas>() != null)
            canvasTopMostParent = transform.root;
        else
            Debug.LogWarning($"transform.root is not canvas, it's {transform.root.name}");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log(eventData);
        canvasGroup.alpha = 0.8f;
        transform.SetParent(canvasTopMostParent);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null)
        {
            Slot droppedSlot;

            // If changing slots to an empty slot
            if (eventData.pointerEnter.GetComponent<Slot>() != null)
            {
                droppedSlot = eventData.pointerEnter.GetComponent<Slot>();
                slot.CurrentItemInSlot = null;
                droppedSlot.CurrentItemInSlot = this.gameObject;
                droppedSlot.CurrentItemInSlot.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot = droppedSlot;

            }
            // If item is in the slot
            else if (eventData.pointerEnter.GetComponent<ItemDragHandler>() != null)
            {
                ItemDragHandler overrideItem = eventData.pointerEnter.GetComponent<ItemDragHandler>();
                droppedSlot = overrideItem.GetComponentInParent<Slot>();

                // Switch positions first
                overrideItem.transform.position = slot.transform.position;
                transform.position = droppedSlot.transform.position;

                // Set override parent and assign slot the override gameobject
                overrideItem.transform.SetParent(slot.transform);
                slot.CurrentItemInSlot = overrideItem.gameObject;
                overrideItem.slot = slot;

                slot = droppedSlot;
            }
        }

        transform.position = slot.transform.position;
        transform.SetParent(slot.transform);
        slot.CurrentItemInSlot = this.gameObject;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
