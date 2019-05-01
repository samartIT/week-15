using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryPopup : MonoBehaviour {
    [SerializeField] private Image[] itemIcons; // for 4 images
    [SerializeField] private Text[] itemLabels; // for 4 Labels

    [SerializeField] private Text curItemLabel;
    [SerializeField] private Button equipButton;
    [SerializeField] private Button useButton;

    private string _curItem;

    public void Refresh() {
        List<string> itemList = Managers.Inventory.GetItemList();

        //display inventory items
        int len = itemIcons.Length;
        for (int i = 0; i < len; i++) {
            if (i < itemList.Count) {
                itemIcons[i].gameObject.SetActive(true);
                itemLabels[i].gameObject.SetActive(true);

                string item = itemList[i];

                //load sprite from Resource/Icons
                Sprite sprite = Resources.Load<Sprite>("Icons/" + item);
                itemIcons[i].sprite = sprite;
                itemIcons[i].SetNativeSize();//resize image

                int count = Managers.Inventory.GetItemCount(item);
                string message = "x" + count;
                if (item == Managers.Inventory.equippedItem) {
                    message = "Equipped\n" + message;
                }
                itemLabels[i].text = message;

                //enable clicking on icons
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.AddListener((BaseEventData data) => {
                    OnItem(item); // Lambda fn, trigger of each item
                });

                EventTrigger trigger = itemIcons[i].GetComponent<EventTrigger>();
                trigger.triggers.Clear();
                trigger.triggers.Add(entry);
            } else {
                itemIcons[i].gameObject.SetActive(false);
                itemLabels[i].gameObject.SetActive(false);
            }
        }

        //display current selection
        if (!itemList.Contains(_curItem)) {
            _curItem = null;
        }
        if (_curItem == null) {
            curItemLabel.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(false);
            useButton.gameObject.SetActive(false);
        } else {
            curItemLabel.gameObject.SetActive(true);
            equipButton.gameObject.SetActive(true);
            if (_curItem == "health")
            {
                useButton.gameObject.SetActive(true);
            }
            else
            {
                useButton.gameObject.SetActive(false);
            }
            curItemLabel.text = _curItem + ":";
        }
    }
public void OnItem(string item)
    {
        _curItem = item;
        Refresh();
    }
public void OnEquip()
    {
        Managers.Inventory.EquipItem(_curItem);
        Refresh();
    }
public void OnUse()
    {
        Managers.Inventory.ConsumeItem(_curItem);
        if (_curItem == "health")
        {
            Managers.Player.ChangeHealth(25);
        }
        Refresh();
    }
}