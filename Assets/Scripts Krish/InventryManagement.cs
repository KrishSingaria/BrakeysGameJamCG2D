using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventryManagement : MonoBehaviour
{
    public static InventryManagement instance;
    public GameObject player;
    public List<Item> items;
    public GameObject HandJoint;

    public GameObject InventryUI;
    public GameObject CrossHair;

    public TextMeshProUGUI itemText;
    public TextMeshProUGUI SelectionText;
    private Button selectionTextParent;
    public Image itemImage;
    public GameObject currItem;

    private int itemIndex;
    private int maxitems;
    private int equipedIndex;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        //PlayerPrefs.SetInt("Item", 0);
        selectionTextParent = SelectionText.transform.parent.GetComponent<Button>();
        maxitems = items.Count;
        equipedIndex = 0;
        InventrySetup();
        EquipBtn();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventryUI.SetActive(!InventryUI.activeSelf);
            CrossHair.SetActive(!CrossHair.activeSelf);
            FirstPersonController fpc = player.GetComponent<FirstPersonController>();
            fpc.enabled = !fpc.enabled;
            if (InventryUI.activeSelf) {
                InventrySetup();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q) && currItem != null)
        {
            Drop(currItem);
            currItem = null;
            items[equipedIndex].isEquipped = false;
            items.RemoveAt(equipedIndex);
            equipedIndex = 0;
            maxitems = items.Count;
            itemIndex = 0;
            PlayerPrefs.SetInt("Item", itemIndex);
            EquipBtn();
        }
    }
    void InventrySetup()
    {
        itemIndex = PlayerPrefs.GetInt("Item",0);
        Item item = items[itemIndex];
        SelectionText.text = "EQUIPPED";
        itemImage.sprite = item.Sprite;
        itemText.text = item.name;
        selectionTextParent.interactable = false;
    }
    public void InventryIndex(bool isRight)
    {
        void set()
        {
            PlayerPrefs.SetInt("Item", itemIndex);
            Item item = items[itemIndex];
            SelectionText.text = item.isEquipped ? "EQUIPPED" : "EQUIP";
            selectionTextParent.interactable = item.isEquipped ? false : true;
            itemText.text = item.name;
            itemImage.sprite = item.Sprite;
        }
        if (isRight)
        {
            if (itemIndex + 1 < maxitems)
            {
                itemIndex++;
                set();
            }
            else if (itemIndex + 1 == maxitems) { 
                itemIndex = 0;
                set();
            }
        }
        else
        {
            if (itemIndex - 1 >= 0)
            {
                itemIndex--;
                set();
            }
            else if (itemIndex -1 < 0)
            {
                itemIndex = maxitems-1;
                set();
            }
        }
    }
    public void EquipBtn()
    {
        items[equipedIndex].isEquipped = false;
        items[itemIndex].isEquipped = true;
        equipedIndex = itemIndex;
        SelectionText.text = "EQUIPPED";
        selectionTextParent.interactable = false;
        Equip(items[itemIndex].item);
    }
    
    void Equip(GameObject itemSpawned)
    {
        if (itemSpawned != null) 
        {
            GameObject item = Instantiate(itemSpawned);
            Destroy(currItem);
            currItem = item;
            Transform transform = item.GetComponent<Transform>();
            transform.SetParent(HandJoint.transform);

            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);

            Rigidbody body = item.GetComponent<Rigidbody>();
            body.isKinematic = true;
            BoxCollider collider = item.GetComponent<BoxCollider>();
            collider.enabled = false;
        }
        else
        {
            items[equipedIndex].isEquipped = false;
            items[0].isEquipped = true;
            Destroy(currItem);
        }
    }
    public void AddItemInInventry(Item item)
    {
        item.isEquipped = false;
        items.Add(item);
        maxitems = items.Count;
    }
    void Drop(GameObject item)
    {
        Transform transform = item.GetComponent<Transform>();
        transform.SetParent(null);

        Rigidbody body = item.GetComponent<Rigidbody>();
        body.isKinematic = false;
        BoxCollider collider = item.GetComponent<BoxCollider>();
        collider.enabled = true;
        body.velocity = player.GetComponent<Rigidbody>().velocity;

        float random = Random.Range(-1f, 1f);
        body.AddTorque(new Vector3(random, random, random)*10);
    }
}