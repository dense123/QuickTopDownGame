using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    Slot slot;
    Transform originalSlotPosition;
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
        if (transform.root.GetComponent<Canvas>() != null)
            canvasTopMostParent = transform.root;
        else
            Debug.LogWarning($"transform.root is not canvas, it's {transform.root.name}");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log(eventData);
        originalSlotPosition = slot.transform;
        canvasGroup.alpha = 0.8f;
        transform.SetParent(canvasTopMostParent);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.LogWarning(eventData);
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.LogWarning(eventData.pointerEnter);
        

        if(eventData.pointerEnter != null)
        {
            Slot droppedSlot;

            if (eventData.pointerEnter.GetComponent<Slot>() != null)
            {
                Debug.Log($"{this.name} slot");
                droppedSlot = eventData.pointerEnter.GetComponent<Slot>();
                originalSlotPosition.GetComponent<Slot>().CurrentItemInSlot = null;
                droppedSlot.CurrentItemInSlot = this.gameObject;
                droppedSlot.CurrentItemInSlot.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                originalSlotPosition = droppedSlot.transform;
                slot = droppedSlot;
                //transform.SetParent(originalSlotPosition);
            }
            else if (eventData.pointerEnter.GetComponent<Item>() != null)
            {
                Debug.Log($"{this.name} Item");
                Item overrideItem = eventData.pointerEnter.GetComponent<Item>();

                droppedSlot = overrideItem.GetComponentInParent<Slot>();
                if (droppedSlot == null)
                    Debug.LogWarning($"{droppedSlot} not assigned! <br> Parent for {overrideItem} is not slot!");

                droppedSlot.CurrentItemInSlot = null;
                slot.CurrentItemInSlot = null;

                overrideItem.transform.position = originalSlotPosition.position;
                overrideItem.transform.SetParent(originalSlotPosition);
                slot.CurrentItemInSlot = overrideItem.gameObject;

                originalSlotPosition = droppedSlot.transform;
                slot = droppedSlot;
            }
        }
        else
        {
        }
            transform.position = originalSlotPosition.position;
            transform.SetParent(originalSlotPosition);
        slot.CurrentItemInSlot = this.gameObject;
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
    }
    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    Debug.LogWarning(eventData.pointerEnter);
    //    Slot droppedSlot;
    //    Item overrideItem;

    //    if (eventData.pointerEnter != null)
    //    {
    //        if (eventData.pointerEnter.GetComponent<Slot>() != null)
    //        {
    //            droppedSlot = eventData.pointerEnter.GetComponent<Slot>();
    //            droppedSlot.CurrentItemInSlot = this.gameObject;
    //            originalSlotPosition = droppedSlot.transform;
    //            //transform.SetParent(originalSlotPosition);
    //        }
    //        else if (eventData.pointerEnter.GetComponent<Item>())
    //        {
    //            overrideItem = eventData.pointerEnter.GetComponent<Item>();

    //            droppedSlot = overrideItem.transform.parent.GetComponent<Slot>();

    //            overrideItem.transform.position = originalSlotPosition.position;
    //            overrideItem.transform.SetParent(originalSlotPosition);

    //            originalSlotPosition = droppedSlot.transform;
    //        }
    //    }
    //    else
    //    {
    //    }
    //    transform.position = originalSlotPosition.position;
    //    transform.SetParent(originalSlotPosition);
    //    canvasGroup.alpha = 1f;
    //    canvasGroup.blocksRaycasts = true;
    //}
}
