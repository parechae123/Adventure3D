using Defines;
using PlayerDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    Rigidbody rb;
    public static InputHandler input;
    IMoveHandler moveHandler;
    Stat stat;
    PlayerCam cam;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        stat = new Stat(100, 5f, 6f);
        moveHandler = new PlayerMoveHandler(rb,stat,transform);
        cam = new PlayerCam(transform);
        if(input==null)input = new InputHandler(moveHandler.OnMove, moveHandler.OnJump, cam.RotCamera);
        else { Destroy(gameObject);Debug.LogError("중복된 플레이어 객체가 있습니다"); }
        Cursor.visible = false;
    }
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        input.InputUpdate();
/*        if (input.MovePress())
        {
            moveHandler.OnMove(input.GetMoveDir());
        }*/
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3) moveHandler.IsGround();

        if (collision.gameObject.layer == 6) GameManager.GetInstance.interactionDict[collision.collider].Invoke(rb);
    }
}
