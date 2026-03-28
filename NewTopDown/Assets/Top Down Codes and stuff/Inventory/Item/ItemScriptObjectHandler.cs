using UnityEngine;
using UnityEngine.UI;

//[ExecuteAlways]
public class ItemScriptObjectHandler : MonoBehaviour
{
    public Item itemData;
    [SerializeField] private int itemID;
    [SerializeField] bool isUIItem;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Image spriteImage;
    [TextArea]
    public string description;


    void OnValidate()
    {
        Debug.LogWarning($"{this.name} Changed values. Now applying changes");
        //itemData = new Item();
        if (itemData == null)
        {
            return;
        }

        itemID = itemData.ID;
        this.name = $"{itemData.name} Parent";
        description = itemData.description.ToString();


        if (isUIItem == false)
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
                Debug.LogWarning("NO RIGIDBODY FOUND!!!!");
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogWarning($"Sprite Renderer has no reference under {this.name}. Will go through children now.");
                spriteRenderer = GetComponentInChildren<SpriteRenderer>();
                if (spriteRenderer == null)
                {
                    Debug.LogWarning($"Sprite Renderer has no reference under {this.name} child. DEBUG please");
                    return;
                }
                else
                    Debug.LogWarning($"Found in child");
            }
            spriteRenderer.sprite = itemData.artwork;
            rb.mass = itemData.mass;
            rb.linearDamping = itemData.linearDamping;
        }
        else
        {
            spriteImage = GetComponent<Image>();
            if (spriteImage == null)
            {
                Debug.LogWarning($"Sprite Image has no reference under {this.name}");
                spriteImage = GetComponentInChildren<Image>();
                if (spriteImage == null)
                {
                    Debug.LogWarning($"Sprite Image has no reference under {this.name} child. DEBUG please");
                    return;
                }
                else
                    Debug.LogWarning($"Found in child");
            }
            spriteImage.sprite = itemData.artwork;
        }

    }
}
