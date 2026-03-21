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
        slot = GetComponentInParent<Slot>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (transform.root.GetComponent<Canvas>() != null)
            canvasTopMostParent = transform.root;
        else
            Debug.LogWarning($"transform.root is not canvas, it's {transform.root}");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log(eventData);
        originalSlotPosition.position = transform.parent.position;
        canvasGroup.alpha = 0.6f;
        transform.SetParent(canvasTopMostParent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.LogWarning(eventData);
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        transform.position = originalSlotPosition.position;
    }

}
