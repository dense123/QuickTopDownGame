using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{


    [SerializeField] private Image[] tabImages;
    [SerializeField] private GameObject[] pageImages; // Set as gameobject instead of image so that we can setActive

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetPageActive(0);
    }

    public void SetPageActive(int index)
    {
        for (int i = 0; i < tabImages.Length; i++)
        {
            pageImages[i].SetActive(false);
            tabImages[i].color = Color.grey;

        }

        pageImages[index].SetActive(true);
        tabImages[index].color = Color.white;

    }
}
