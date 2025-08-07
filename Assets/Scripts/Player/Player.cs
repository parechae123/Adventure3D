using Defines;
using PlayerDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    Rigidbody rb;
    InputHandler input;
    IMoveHandler moveHandler;
    Stat stat;
    PlayerCam cam;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        stat = new Stat(100, 5f, 6f);
        moveHandler = new PlayerMoveHandler(rb,stat,transform);
        cam = new PlayerCam(transform);
        input = new InputHandler(moveHandler.OnMove, moveHandler.OnJump, cam.RotCamera);

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
    }
}
