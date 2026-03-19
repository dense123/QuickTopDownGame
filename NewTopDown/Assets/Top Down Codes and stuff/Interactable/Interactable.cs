using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour
{
    [SerializeField] bool isInteractedWith;
    [SerializeField] bool isHovering;
    //Outline outline;


    protected virtual void Start()
    {
        //outline = GetComponent<Outline>();
    }

    protected virtual void Update()
    {
        
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
