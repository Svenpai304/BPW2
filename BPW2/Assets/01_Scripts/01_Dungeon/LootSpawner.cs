using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    public List<ItemDrop> possibleDrops = new List<ItemDrop>();

    private void Start()
    {
        int totalDropChance = 0;
        foreach (ItemDrop item in possibleDrops)
        {
            totalDropChance += item.dropChance;
        }

        int choice = UnityEngine.Random.Range(0, totalDropChance + 1);
        foreach (ItemDrop item in possibleDrops)
        {
            choice -= item.dropChance;
            if (choice <= 0)
            {
                SpawnItem(item);
                return;
            }
        }
    }

    public void SpawnItem(ItemDrop item)
    {
        if (item.item != null)
        {
            Instantiate(item.item, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }


    [Serializable]
    public class ItemDrop
    {
        public ItemDrop(GameObject _item, int _dropChance)
        {
            item = _item;
            dropChance = _dropChance;
        }

        public GameObject item;
        public int dropChance;
    } 
}
