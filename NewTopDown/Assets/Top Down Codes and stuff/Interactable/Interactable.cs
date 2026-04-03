using System;
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
    Transform PlayerTransfrom;
    GameObject LastHitCollider;
    protected GameEvents gameEvents;

    //public void Init(PlayerRaycasting playerRay)
    //{
    //    playerRaycasting = playerRay;
    //    playerRaycasting.OnRaycastHit += DisplayTextAboveItem;
    //}

    protected virtual void Awake()
    {
        
        Debug.Log("Awake");

        Debug.Log(GameManager.instance);
        //gameEvents = GameManager.instance.gameEvents;

    }

    protected virtual void Start()
    {
        //outline = GetComponent<Outline>();
        PlayerTransfrom = GameManager.instance.Player.transform;
        Debug.Log("started");
    }

    protected virtual void Update()
    {
        //if (isHovering)
        //{
        //} else
        //    TextObjectName.SetActive(false);
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
                .SetText(Vector2.Distance(transform.position, PlayerTransfrom.position).ToString("0.00"));
        }
        else
            GameManager.instance.nullReference_debugLogWarning("Text object name", this.name);
    }

}
