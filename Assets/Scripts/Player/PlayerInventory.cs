using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<Item> inv;
    bool isOpen = false;
    [SerializeField] private RectTransform inventoryBox;
    [SerializeField] private RectTransform itemTemplate;
    [SerializeField] private RectTransform itemContainer;
    private List<GameObject> tmpItem = new List<GameObject>();

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            if (isOpen)
            {
                isOpen = false;
                GetComponent<PlayerControler>().enabled = true;
                GetComponent<PlayerInteract>().enabled = true;
                inventoryBox.gameObject.SetActive(isOpen);
            } else
            {
                isOpen = true;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<PlayerControler>().enabled = false;
                GetComponent<PlayerInteract>().enabled = false;
                updateInv();
                inventoryBox.gameObject.SetActive(isOpen);
            }
        }
    }

    private void updateInv()
    {
        foreach(GameObject o in tmpItem)
        {
            Destroy(o);
        }
        tmpItem.Clear();

        for (int i = 0;  i < inv.Count; ++i)
        {
            GameObject item = Instantiate(itemTemplate.gameObject, itemContainer);
            item.SetActive(true);
            item.GetComponent<Image>().sprite = inv[i].Icon;
            tmpItem.Add(item);
        }
    }

    public void addItem(Item item)
    {
        inv.Add(item);
    }

    public bool removeItem(Item item)
    {
        return inv.Remove(item);
    }
}
