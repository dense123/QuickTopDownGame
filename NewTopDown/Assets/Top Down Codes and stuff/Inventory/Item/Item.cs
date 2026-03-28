using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New Item" , menuName = "ITEMS")]
public class Item : ScriptableObject
{
    // This will link item ui and item irl
    //[HideInInspector]
    public int ID;
    public string ItemName;
    
    public Sprite artwork;

    [TextArea]
    public string description;

    public float mass;
    public float linearDamping;

    private void Awake()
    {
        ItemName = this.name;
    }
}
