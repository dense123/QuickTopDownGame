using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour
{
    [SerializeField] public bool isInteractedWith;
    [SerializeField] public bool isHovering;
    [SerializeField] protected GameObject TextObjectName;
    GameObject LastHitCollider;


    protected virtual void Start()
    {
        //outline = GetComponent<Outline>();
        
    }

    protected virtual void Update()
    {
        if (isHovering)
        {
            if (TextObjectName != null)
            {
                TextObjectName.SetActive(true);
                TextObjectName.GetComponent<TextMeshPro>().SetText(Vector2.Distance(transform.position, GameManager.instance.Player.transform.position).ToString("0.00"));
            }
            else
                GameManager.instance.nullReference_debugLogWarning("Text object name", this.name);
        } else
            TextObjectName.SetActive(false);
    }

    protected void ActivateOutline()
    {
        //outline.enabled = true;
    }

    protected void DeactivateOutline()
    {
        //outline.enabled = false;
    }
}
