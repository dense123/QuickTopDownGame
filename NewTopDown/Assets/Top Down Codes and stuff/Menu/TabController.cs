using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{


    [SerializeField] private Image[] tabImages;
    [SerializeField] private GameObject[] pageImages; // Set as gameobject instead of image so that we can setActive
    int currentPageIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPageIndex = 0;
        for (int i = 0; i < tabImages.Length; i++)
        {
            pageImages[i].SetActive(false);
            tabImages[i].color = Color.grey;

        }
        pageImages[currentPageIndex].SetActive(true);
        tabImages[currentPageIndex].color = Color.white;
    }

    public void SetPageActive(int newIndex)
    {

        pageImages[currentPageIndex].SetActive(false);
        tabImages[currentPageIndex].color = Color.grey;

        currentPageIndex = newIndex;

        pageImages[currentPageIndex].SetActive(true);
        tabImages[currentPageIndex].color = Color.white;
    }

    public void SetPageActiveLeft(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            int newIndex = currentPageIndex - 1;
            SetPageActive(Mathf.Clamp(newIndex, 0, tabImages.Length - 1));
        }
    }

    public void SetPageActiveRight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            int newIndex = currentPageIndex + 1;
            SetPageActive(Mathf.Clamp(newIndex, 0, tabImages.Length - 1));
        }
    }
}
