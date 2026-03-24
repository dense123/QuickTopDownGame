using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Slot : MonoBehaviour
{

    private GameObject currentItemInThisSlot;
    [SerializeField] private GameObject text;
    TextMeshProUGUI currentText;

    public GameObject CurrentItemInSlot
    {
        get => currentItemInThisSlot;
        set
        {
            currentItemInThisSlot = value;
            if (value != null)
                currentText.SetText(value.name);
            else
                currentText.SetText(".");

        }
    }

    private void Start()
    {
        GameObject obj = Instantiate(text, transform);
        currentText = obj.GetComponent<TextMeshProUGUI>();
        currentText.transform.localPosition = Vector3.up * 2f;

    }

    private void Update()
    {
    }


}
