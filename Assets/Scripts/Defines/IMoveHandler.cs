using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Defines
{
    interface IMoveHandler
    {
        void OnMove(Vector3 vec);
        void OnJump();

        void IsGround(bool isOnGround);
    }
    public class PlayerMoveHandler : IMoveHandler
    {
        Rigidbody rb;
        Stat stat;
        Transform tr;
        bool isOnGround = false;
        public PlayerMoveHandler(Rigidbody rb, Stat stat,Transform tr)
        {
            this.rb = rb;
            this.stat = stat;
            this.tr = tr;
        }

        public void OnMove(Vector3 v)
        {
            /*v.y = rb.velocity.y;
            rb.velocity = v*stat.MoveSpeed;*/
            Vector3 dir = Vector3.zero;
            dir += tr.forward * v.z;
            dir += tr.right * v.x;
            dir = dir.normalized;
            tr.transform.position += (dir * stat.MoveSpeed)*Time.deltaTime;
        }
        public void OnJump()
        {
            if (isOnGround == false) return;
            isOnGround = false;
            rb.AddForce(Vector3.up * stat.JumpForce, ForceMode.Impulse);
        }

        public void IsGround(bool isOnGround)
        {
            this.isOnGround = isOnGround;
        }
    }
}
