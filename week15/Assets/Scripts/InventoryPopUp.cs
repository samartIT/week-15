using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryPopUp : MonoBehaviour
{
    [SerializeField] private Image[] itemIcons;
    [SerializeField] private Text[] itemLabels;

    [SerializeField] private Text curItemLabel;
    [SerializeField] private Button equipButtom;
    [SerializeField] private Button useButtom;

    private string _curITem;

    public void Refresh()
    {
        List<string> itemList = Managers.Inventory.GetItemList();

        //display
        int len = itemIcons.Length;
        for (int i = 0; i < len; i++)
        {
            if(i < itemList.Count) //check list
            {
                itemIcons[i].gameObject.SetActive(true);
                itemLabels[i].gameObject.SetActive(true);

                string item = itemList[i];

                //load sprite from resouse
                Sprite sprite = Resources.Load<Sprite>("Icon/" + item);
                itemIcons[i].sprite = sprite;
                itemIcons[i].SetNativeSize();

                int count = Managers.Inventory.GetItemCount(item);
                string message = "x" + count;
                if (item == Managers.Inventory.equippedItem)
                {
                    message = "Equipped\n" + message;
                }
                itemLabels[i].text = message;

                // enable clicking
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.AddListener((BaseEventData data) => { OnItem(item); });

                EventTrigger trigger = itemIcons[i].GetComponent<EventTrigger>();
                trigger.triggers.Clear();
                trigger.triggers.Add(entry);
            } else
            {
                itemIcons[i].gameObject.SetActive(false);
                itemLabels[i].gameObject.SetActive(false);
            }
        }

        // display current selection
        if (!itemList.Contains(_curITem))
        {
            _curITem = null;
        }

        if (_curITem == null)
        {
            curItemLabel.gameObject.SetActive(false);
            equipButtom.gameObject.SetActive(false);
            useButtom.gameObject.SetActive(false);
        } else
        {
            curItemLabel.gameObject.SetActive(true);
            equipButtom.gameObject.SetActive(true);
            if (_curITem == "health")
            {
                useButtom.gameObject.SetActive(true);
            } else
            {
                useButtom.gameObject.SetActive(false);
            }

            curItemLabel.text = _curITem + ":";
        }
    }

    public void OnItem(string item)
    {
        _curITem = item;
        Refresh();
    }

    public void OnEquip()
    {
        Managers.Inventory.EquipItem(_curITem);
        Refresh();
    }

    public void OnUse()
    {
        Managers.Inventory.ConsumeItem(_curITem);
        if (_curITem == "health")
        {
            Managers.Player.ChangeHealth(25);
        }
        Refresh();
    }
}
