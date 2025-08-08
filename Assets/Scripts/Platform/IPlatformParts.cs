using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using static UnityEngine.InputSystem.InputAction;

interface IPlatformParts
{
    void CollisionInteract(Rigidbody rb);
    void ViewInfo();
    static IPlatformParts Factory(PlatformType type,Transform tr)
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
public class JumpPlatform : IPlatformParts
{
    public void CollisionInteract(Rigidbody rb)
    {
        rb.AddForce(Vector3.up * 20f, ForceMode.Impulse);
    }

    public void ViewInfo()
    {
        Debug.Log(ResourceManager.GetInstance.interactionDatas["jumpPlatform"]);
    }
}
public class DashPlatform : IPlatformParts
{
    Transform tr;
    Rigidbody rb;
    Transform targetUI;
    public DashPlatform(Transform tr)
    {
        this.tr = tr;
        targetUI = GameObject.FindGameObjectsWithTag("UI").Where(x => x.gameObject.name == "DashPlatformPannel").First().transform;
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
    }
    public void ViewInfo()
    {
        Debug.Log(ResourceManager.GetInstance.interactionDatas["dashPlatform"]);

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