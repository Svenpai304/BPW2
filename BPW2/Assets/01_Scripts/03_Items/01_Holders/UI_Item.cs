using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Item : MonoBehaviour
{
    public Image uiImage;
    public Item itemRef;
    public UI_ItemSlot currentSlot;

    private void Awake()
    {
        uiImage= GetComponent<Image>();
    }

    public void Setup(Item item)
    {
        itemRef = item;
        uiImage.sprite = itemRef.icon;
        itemRef.OnItemUse += OnHeldItemUse;
        itemRef.uiItem = this;
        itemRef.gameObject.SetActive(false);
        itemRef.OnItemAdd();
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
        itemRef.uiItem = null;
        itemRef.transform.position = dropPosition;
        itemRef.OnItemRelease();
        currentSlot.ReleaseItem();
        currentSlot = null;
    }

    public void DestroyItem()
    {
        Destroy(itemRef.gameObject);
        currentSlot.ReleaseItem();
        currentSlot = null;
    }

    public void SetSlot(UI_ItemSlot slot)
    {
        currentSlot = slot;
    }

}
