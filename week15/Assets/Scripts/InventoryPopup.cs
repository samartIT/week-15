using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryPopup : MonoBehaviour
{
    [SerializeField] private Image[] itemIcons;
    [SerializeField] private Text[] itemlabels;

    [SerializeField] private Text curItemlabel;
    [SerializeField] private Button equipButton;
    [SerializeField] private Button useButton;

    private string _curItem;

    public void Refresh() {
        List<string> itemlist = Managers.Inventory.GetItemList();

        int len = itemIcons.Length;
        for (int i = 0; i < len; i++) {
            if (i < itemlist.Count)
            {
                itemIcons[i].gameObject.SetActive(true);
                itemlabels[i].gameObject.SetActive(true);

                string item = itemlist[i];

                Sprite sprite = Resources.Load<Sprite>("Icons/" + item);
                itemIcons[i].sprite = sprite;
                itemIcons[i].SetNativeSize();

                int count = Managers.Inventory.GetItemCount(item);
                string message = "x" + count;
                if (item == Managers.Inventory.equippedItem)
                {
                    message = "Equipped\n" + message;
                }
                itemlabels[i].text = message;

                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.AddListener((BaseEventData data) =>
                {
                    OnItem(item);
                });

                EventTrigger trigger = itemIcons[i].GetComponent<EventTrigger>();
                trigger.triggers.Clear();
                trigger.triggers.Add(entry);
            }
            else {
                itemIcons[i].gameObject.SetActive(false);
                itemlabels[i].gameObject.SetActive(false);
            }
           
        }

        if (!itemlist.Contains(_curItem)) {
            _curItem = null;
        }

        if (_curItem == null)
        {
            curItemlabel.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(false);
            useButton.gameObject.SetActive(false);
        }
        else {
            curItemlabel.gameObject.SetActive(true);
            equipButton.gameObject.SetActive(true);
            if (_curItem == "health")
            {
                useButton.gameObject.SetActive(true);
            }
            else {
                useButton.gameObject.SetActive(false);
            }

            curItemlabel.text = _curItem + ":" ;
        }
    }

    public void OnItem(string item) {
        _curItem = item;
        Refresh();
    }

    public void OnEquip() {
        Managers.Inventory.EquipItem(_curItem);
        Refresh();
    }

    public void OnUse() {
        Managers.Inventory.ConsumeItem(_curItem);
        if (_curItem == "health") {
            Managers.Player.ChangeHealth(25);
        }
        Refresh();
    }
}
