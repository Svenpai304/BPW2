using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Item : MonoBehaviour
{
    public RawImage uiImage;
    public Item itemRef;
    public UI_ItemSlot currentSlot;

    private void Awake()
    {
        uiImage= GetComponent<RawImage>();
    }

    public void Setup(Item item)
    {
        itemRef = item;
        itemRef.gameObject.SetActive(false);
        uiImage.texture = itemRef.icon;
        itemRef.OnItemUse += OnHeldItemUse;
    }

    public void Use()
    {
        itemRef.ItemUse();
    }

    public void OnHeldItemUse(Item usedItem)
    {
        
    }

    public void DropItem(Vector3 dropPosition)
    {
        itemRef.gameObject.SetActive(true);
        itemRef.transform.position = dropPosition;
        currentSlot.ReleaseItem();
        currentSlot = null;
    }

    public void SetSlot(UI_ItemSlot slot)
    {
        currentSlot = slot;
    }

}
