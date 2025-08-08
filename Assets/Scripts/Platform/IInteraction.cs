using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using static UnityEngine.InputSystem.InputAction;
using TMPro;

interface IInteraction
{
    void CollisionInteract(Rigidbody rb);
    void ViewInfo();
    static IInteraction Factory(PlatformType type,Transform tr)
    {
        switch (type)
        {
            case PlatformType.jump:
                return new JumpPlatform();
            case PlatformType.dash:
                return new DashPlatform(tr);
            default:
                break;
        }
        return null;
    }
}
public class JumpPlatform : IInteraction
{
    public void CollisionInteract(Rigidbody rb)
    {
        rb.AddForce(Vector3.up * 20f, ForceMode.Impulse);
    }

    public void ViewInfo()
    {
        GameManager.GetInstance.printInfo.Invoke(ResourceManager.GetInstance.interactionDatas["jumpPlatform"]);
    }
}
public class DashPlatform : IInteraction
{
    Transform tr;
    Rigidbody rb;
    Transform targetUI;
    TextMeshProUGUI text;
    public DashPlatform(Transform tr)
    {
        this.tr = tr;
        targetUI = GameObject.FindGameObjectWithTag("Canvas").transform.Find("InteractionText");
        text = targetUI.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        targetUI.gameObject.SetActive(false);
    }
    public void CollisionInteract(Rigidbody rb)
    {
        rb.transform.position = tr.position+(Vector3.up*tr.localScale.y);
        if (this.rb == null) this.rb = rb;
        Player.input.DisablePlayer();
        Player.input.input.Interactions.LeftClick.performed += ClickAction;
        Player.input.input.Interactions.Enable();
        targetUI.gameObject.SetActive(true);
        text.text = $"{Player.input.input.Interactions.LeftClick.GetBindingDisplayString(0)} To Player Fire";
    }
    public void ViewInfo()
    {
        GameManager.GetInstance.printInfo.Invoke(ResourceManager.GetInstance.interactionDatas["dashPlatform"]);

    }
    private void ClickAction(CallbackContext cb)
    {
        Player.input.EnablePlayer();
        targetUI.gameObject.SetActive(false);
        Player.input.input.Interactions.Disable();
        Player.input.input.Interactions.LeftClick.performed -= ClickAction;
        rb.AddForce(Camera.main.transform.forward * 20f, ForceMode.Impulse);
    }
}