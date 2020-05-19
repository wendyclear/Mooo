using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventory : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject slotPrefab;
    public Transform slotPanel;
    public GameObject inventory;
    private PointerEventData _mouseInfo;
    private Item _itemDrag;
    private Item _itemDrop;

    private List<UIItem> _inventorySlots = new List<UIItem>();
    private List<Item> _inventoryItems = new List<Item>();
    private List<RaycastResult> _objectsHit = new List<RaycastResult>();
    private int numberOfSlots = 20;
    private bool _dragging = false;

    [SerializeField]
    private CanvasManager _canvasManager;

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

    public bool AddItem(string name, Sprite sprite, int maxAmount, int amount)
    {
        bool addedItem = false;
        foreach (Item item in _inventoryItems)
        {
            if (item.GetName() == name && item.GetAmount() < item.GetMaxAmount())  //maybe lambda izraz?
            {
                if (item.GetAmount() + amount < item.GetMaxAmount())
                {
                    item.AddAmount(amount);
                    amount = 0;
                }
                else
                {
                    amount = amount - (item.GetMaxAmount() - item.GetAmount());
                    item.SetMaxAmount();
                }
                if (amount == 0)
                {
                    addedItem = true;
                    break;
                }
            }
            else if (item.GetName() == null)
            {
                item.SetItem(name, sprite, amount, maxAmount);
                addedItem = true;
                break;
            }
        }
        RefreshUI();
        return addedItem;
    }

    public bool RemoveItem(string name)
    {
        bool removedItem = false;
        foreach (Item item in _inventoryItems)
        {
            if (item.GetName() == name && item.GetAmount() > 1)  //maybe lambda izraz?
            {
                item.RemoveAmount(1);
                removedItem = true;
                break;
            }
            else if (item.GetName() == name && item.GetAmount() == 1)
            {
                item.Reset();
                removedItem = true;
                break;
            }
        }
        RefreshUI();
        return removedItem;
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

    public int CountOf(string name)
    {
        int total = 0;
        foreach (Item item in _inventoryItems)
        {
            if (item.GetName() == name)
            {
                total += item.GetAmount();
            }
        }
        return total;
    }

    private void RefreshUI()
    {
        _canvasManager.ChangeAmmo();
        _canvasManager.ChangeBuckets();
    }

    private void SplitStack()
    {
        int freeSlotIndex = -1;
        Item _inventoryItem = GetClickedInventoryItem();
        if (_inventoryItem == null) return;
        if (_inventoryItem.GetAmount() >= 2)
        {
            freeSlotIndex = FindFreeSlot();
            if (freeSlotIndex > -1)
            {
                int otherHalf = _inventoryItem.Split();
                _inventoryItems[freeSlotIndex].SetItem(_inventoryItem.GetName(), _inventoryItem.GetSprite(), otherHalf, _inventoryItem.GetMaxAmount());

            }
        }
        ShowItems();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (inventory.activeSelf && Input.GetKey(KeyCode.LeftShift) && !_dragging)
        {
            SplitStack();
        }
    }

    private int FindFreeSlot()
    {
        int freeSlotIndex = -1;
        foreach (Item item in _inventoryItems)
        {
            if (item.GetName() == null)
            {
                freeSlotIndex = _inventoryItems.IndexOf(item);
                break;
            }
        }
        return freeSlotIndex;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _itemDrag = null;
        if (inventory.activeSelf && !Input.GetKey(KeyCode.LeftShift))
        {
            _itemDrag = GetClickedInventoryItem();
            _dragging = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _itemDrop = null;
        _dragging = false;
        if (_itemDrag == null)
        {
            return;
        }
        else
        {
            _itemDrop = GetClickedInventoryItem();
        }
        if (_itemDrop == null) Replace();
        else SwapOrMerge();
    }

    private Item GetClickedInventoryItem()
    {
        Item returnItem = null;
        _mouseInfo = new PointerEventData(EventSystem.current);
        _mouseInfo.position = Input.mousePosition;
        EventSystem.current.RaycastAll(_mouseInfo, _objectsHit);
        if (_objectsHit.Count > 2 && _objectsHit[2].gameObject.tag == "slot")
        {
            UIItem UIItemHit = _objectsHit[2].gameObject.transform.GetComponent<UIItem>();
            returnItem = _inventoryItems[UIItemHit.GetOrder()];
        }
        return returnItem;
    }


    private int GetClickedItemOrder()
    {
        int order = -1;
        _mouseInfo = new PointerEventData(EventSystem.current);
        _mouseInfo.position = Input.mousePosition;
        EventSystem.current.RaycastAll(_mouseInfo, _objectsHit);
        if (_objectsHit.Count == 0) return order;
        else if (_objectsHit[0].gameObject.tag == "slot")
        {
            UIItem UIItemHit = _objectsHit[0].gameObject.transform.GetComponent<UIItem>();
            order = UIItemHit.GetOrder();
        }
        else if (_objectsHit.Count == 1) order = -2;
        return order;
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    private void SwapOrMerge()
    {
        if (_itemDrag.GetName() == _itemDrop.GetName() && _itemDrop != _itemDrag )
        {
            _itemDrag.SetAmount(_itemDrop.Fit(_itemDrag.GetAmount()));
            if (_itemDrag.GetAmount() <= 0 ) _itemDrag.Reset();
            ShowItems();
        }
        else
        {
            Item temp = new Item();
            temp.SetItem(_itemDrag.GetName(), _itemDrag.GetSprite(), _itemDrag.GetAmount(), _itemDrag.GetMaxAmount());
            _itemDrag.SetItem(_itemDrop.GetName(), _itemDrop.GetSprite(), _itemDrop.GetAmount(), _itemDrop.GetMaxAmount());
            _itemDrop.SetItem(temp.GetName(), temp.GetSprite(), temp.GetAmount(), temp.GetMaxAmount());
            ShowItems();
        }
    }

    private void Replace()
    {
        int action = GetClickedItemOrder();
        if (action < -1 ) return;
        else if (action == -1)
        {
            _itemDrag.Reset();
            ShowItems();
            return;
        }
        else
        {
            _inventoryItems[action].SetItem(_itemDrag.GetName(), _itemDrag.GetSprite(), _itemDrag.GetAmount(), _itemDrag.GetMaxAmount());
            _inventoryItems[_inventoryItems.IndexOf(_itemDrag)].Reset();
            ShowItems();
        }

    }
}
