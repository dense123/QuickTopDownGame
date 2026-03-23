using UnityEngine;

public class Slot : MonoBehaviour
{

    private GameObject currentItemInThisSlot;

    public GameObject CurrentItemInSlot
    {
        get => currentItemInThisSlot;
        set
        {
            currentItemInThisSlot = value;
        }
    }


}
