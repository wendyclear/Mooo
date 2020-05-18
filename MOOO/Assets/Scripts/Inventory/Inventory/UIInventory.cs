using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform slotPanel;
    public GameObject inventory;

    private List<UIItem> _inventorySlots = new List<UIItem>();
    public List<Item> _inventoryItems = new List<Item>();
    private int numberOfSlots = 20;

    private void Awake()
    {
        for (int i = 0; i < numberOfSlots; i++)
        {
            //create cells in hierarchy
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            instance.GetComponentInChildren<UIItem>().SetOrder(i);
            _inventorySlots.Add(instance.GetComponentInChildren<UIItem>());
            _inventoryItems.Add(new Item());

        }

    }

    public void ShowItems()
    {
        foreach (Item item in _inventoryItems)
        {
            UIItem slot = _inventorySlots[_inventoryItems.IndexOf(item)];
            if (item.GetName() != null)
            {
                slot.SetCell(item.GetAmount(), item.GetSprite());
                slot.ShowCell();
            }
            else
            {
                slot.HideCell();
            }
        }
    }

    public bool AddItem(string name, Sprite sprite, int maxAmount)
    {
        bool addedItem = false;
        foreach(Item item in _inventoryItems)
        {
            if (item.GetName() == name && item.GetAmount() < item.GetMaxAmount())  //maybe lambda izraz?
            {
                item.AddAmount(1);
                addedItem = true;
                break;
            }
            else if (item.GetName() == null)
            {
                item.SetItem(name, sprite, 1, maxAmount);
                addedItem = true;
                break;
            }
        }
        return addedItem;
    }

    public void OpenInventory()
    {
        inventory.SetActive(true);
        ShowItems();
    }

    public void CloseInventory()
    {
        inventory.SetActive(false);
    }


}
