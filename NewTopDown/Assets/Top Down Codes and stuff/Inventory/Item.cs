using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
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
        if (transform.root.GetComponent<Canvas>() != null)
            canvasTopMostParent = transform.root;
        else
            Debug.LogWarning($"transform.root is not canvas, it's {transform.root.name}");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log(eventData);
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
        if(eventData.pointerEnter != null)
        {
            Slot droppedSlot;

            if (eventData.pointerEnter.GetComponent<Slot>() != null)
            {
                Debug.Log($"{this.name} slot");
                droppedSlot = eventData.pointerEnter.GetComponent<Slot>();
                slot.CurrentItemInSlot = null;
                droppedSlot.CurrentItemInSlot = this.gameObject;
                droppedSlot.CurrentItemInSlot.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot = droppedSlot;
                
            }
            else if (eventData.pointerEnter.GetComponent<Item>() != null)
            {
                Item overrideItem = eventData.pointerEnter.GetComponent<Item>();
                droppedSlot = overrideItem.GetComponentInParent<Slot>();
                
                // Switch positions first
                overrideItem.transform.position = slot.transform.position;
                transform.position = droppedSlot.transform.position;

                // Set override parent and assign slot the override gameobject
                overrideItem.transform.SetParent(slot.transform.transform);
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
