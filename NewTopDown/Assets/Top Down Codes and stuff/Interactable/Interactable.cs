using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//[RequireComponent(typeof(Outline))]
public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private bool isInteractedWith;
    [SerializeField] private bool isHovering;
    [SerializeField] protected GameObject TextObjectName;
    Transform CharacterTransfrom;
    GameObject LastHitCollider;


    protected virtual void Awake()
    {
        
        Debug.Log("Awake");

        Debug.Log(GameManager.Instance);

    }

    protected virtual void Update()
    {

    }
    protected virtual void Start()
    {
        CharacterTransfrom = GameManager.Instance.Player.transform;
        Debug.Log("started");
    }

    private void OnEnable()
    {
        Debug.Log("enabled");
        //gameEvents.OnHovering += DisplayTextAboveItem;
    }

    private void OnDisable()
    {
        Debug.Log("disable");
        //gameEvents.OnHovering -= DisplayTextAboveItem;
    }
    void DisplayTextAboveItem()
    {
        if (TextObjectName != null)
        {
            TextObjectName.SetActive(true);
            TextObjectName.GetComponent<TextMeshPro>()
                .SetText(Vector2.Distance(transform.position, CharacterTransfrom.position).ToString("0.00"));
        }
        else
            GameManager.Instance.nullReference_debugLogWarning("Text object name", this.name);
    }

}
