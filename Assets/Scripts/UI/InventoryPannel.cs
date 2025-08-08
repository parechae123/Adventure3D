using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class InventoryPannel : MonoBehaviour
{
    bool loadDone = false;
    private IEnumerator Start()
    {
        new Inventory(gameObject);
        yield return new WaitUntil(()=> Player.input.input != null);
        ResourceManager.GetInstance.LoadAsync<Item>("Potion", (item) =>
        {
            Inventory.AddItem(item,10);
        });
        Player.input.input.UI.Inventory.performed += OpenInventory;
        Player.input.input.UI.Enable();
        loadDone = true;
        gameObject.SetActive(false);
    }
    private void OpenInventory(CallbackContext cb)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    private void OnEnable()
    {
        if (!loadDone) return;
        Player.input.DisablePlayer();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private void OnDisable()
    {
        Player.input.EnablePlayer();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
public class Inventory
{
    private static Slot[] slots;
    public static Slot[] GetSlot
    {
        get { return slots; }
    }
    public Inventory(GameObject obj)
    {
        if (slots != null) return;
        slots = new Slot[obj.transform.childCount];
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            Button btn = obj.transform.GetChild(i).GetComponent<Button>();
            slots[i] = new Slot(btn.image, btn);
        }
    }
    public static void AddItem(Item item,int amount)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].Amount <= 0 && slots[i].ItemCode == -1)
            {
                slots[i].AddItem(item,amount);
                break;
            }
        }
    }
}
public class Slot
{
    private Image image;
    private Button button;
    private int itemCode;
    public int ItemCode { get { return itemCode; } }
    private int amount;
    public int Amount 
    { 
        get 
        { 
            return amount;
        } 
        set 
        {
            if (value <= 0)
            {
                itemValue = 0;
                itemCode = -1;
                duration = 0;
                image.sprite = null;
            }
            amount = value; 
        }
    }
    private int itemValue;
    private float duration;
    public Slot(Image image,Button btn)
    {
        this.image = image;
        this.button = btn;
        itemCode = -1;
        button.onClick.AddListener(UseItem);
    }
    //추후 아이템 사용 인터페이스 구현하여 사용하면 될듯
    public void AddItem(Item item,int amount = 1)
    {
        this.itemValue = item.ItemValue;
        itemCode = item.ItemCode;
        image.sprite = item.IconSprite;
        duration = item.Duration;
        Amount += amount;
    }
    public void UseItem()
    {
        if (Amount <= 0) return;
        Amount -= 1;
        GameManager.GetInstance.moveBuff.Invoke(itemValue,duration);
    }

}