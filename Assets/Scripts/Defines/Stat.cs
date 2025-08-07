using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Defines
{
    public class Stat
    {
        private int currHP;
        private int maxHP;
        public int HP 
        {
            get 
            { 
                return currHP;
            } 
            set 
            {
                if (MaxHP < value) return;
                
                currHP = value;
            } 
        }
        public int MaxHP { get { return maxHP; } set { maxHP = value; } }

        private float moveSpeed;
        public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

        private float jumpForce;
        public float JumpForce { get { return jumpForce; } set { jumpForce = value; } }
        public bool IsDie { get { return currHP <= 0; } }

        public Stat(int hp, float moveSpeed, float jumpForce)
        {
            this.maxHP = hp;
            this.currHP = hp;
            this.moveSpeed = moveSpeed;
            this.jumpForce = jumpForce;
        }
        public void GetDamaged(int damage)
        {
            HP -= damage;
        }
    }
}
