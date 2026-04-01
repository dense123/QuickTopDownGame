using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{


    [SerializeField] private Image[] tabImages;
    [SerializeField] private GameObject[] pageImages; // Set as gameobject instead of image so that we can setActive
    int currentPageIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPageIndex = 0;
        SetPageActive(currentPageIndex);
    }

    public void SetPageActive(int index)
    {
        for (int i = 0; i < tabImages.Length; i++)
        {
            pageImages[i].SetActive(false);
            tabImages[i].color = Color.grey;

        }
        currentPageIndex = index;
        pageImages[currentPageIndex].SetActive(true);
        tabImages[currentPageIndex].color = Color.white;

    }

    public void SetPageActiveLeft(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            int previousPageIndex = currentPageIndex;
            currentPageIndex--;
            if (0 <= currentPageIndex)
            {
                pageImages[previousPageIndex].SetActive(false);
                tabImages[previousPageIndex].color = Color.grey;

                pageImages[currentPageIndex].SetActive(true);
                tabImages[currentPageIndex].color = Color.white;
            }
            else if (currentPageIndex < 0)
            {
                currentPageIndex = 0;
            }
        }
    }

    public void SetPageActiveRight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            int previousPageIndex = currentPageIndex;
            currentPageIndex++;
            if (currentPageIndex < tabImages.Length)
            {
                pageImages[previousPageIndex].SetActive(false);
                tabImages[previousPageIndex].color = Color.grey;

                pageImages[currentPageIndex].SetActive(true);
                tabImages[currentPageIndex].color = Color.white;
            }
            else if (tabImages.Length - 1 < currentPageIndex)
            {
                currentPageIndex = tabImages.Length - 1;
            }
        }

    }
}
