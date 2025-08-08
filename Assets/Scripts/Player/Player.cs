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
        if(input==null)input = new InputHandler(moveHandler.OnMove, moveHandler.OnJump, cam.RotCamera,cam.InfoRay);
        else { Destroy(gameObject);Debug.LogError("중복된 플레이어 객체가 있습니다"); }
        Cursor.visible = false;
        stat.hpBar += GameObject.FindGameObjectWithTag("Canvas").transform.Find("HPBar").GetComponent<HPBar>().OnValueChange;
        GameManager.GetInstance.damageDict.Add(GetComponent<Collider>(), stat.GetDamaged);
    }
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (stat.moveBuffs != null&& stat.moveBuffs.Count>0)
        {
            for (int i = 0; i < stat.moveBuffs.Count; i++)
            {
                if (i < 0) break;
                stat.moveBuffs[i].duration -= Time.deltaTime;
                if (stat.moveBuffs[i].duration <= 0f)
                {
                    stat.moveBuffs[i].action.Invoke(stat.moveBuffs[i].itemValue);
                    stat.moveBuffs.RemoveAt(i);
                    i -= 1;
                }
            }
        }
        input.InputUpdate();
/*        if (input.MovePress())
        {
            moveHandler.OnMove(input.GetMoveDir());
        }*/
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3) moveHandler.IsGround(true);

        if (collision.gameObject.layer == 6) GameManager.GetInstance.interactionDict[collision.collider].Invoke(rb);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 3) moveHandler.IsGround(false);
    }
}
