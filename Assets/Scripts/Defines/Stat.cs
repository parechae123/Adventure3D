using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;

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
                hpBar.Invoke(((float)value / (float)maxHP));
                if (MaxHP < value)
                {
                    return;
                }
                if(value <= 0) { Player.input.DisablePlayer(); currHP = 0; }
                currHP = value;
            } 
        }
        public int MaxHP { get { return maxHP; } set { maxHP = value; } }

        private float moveSpeed;
        private float buffMoveSpeed;
        public float MoveSpeed { get { return buffMoveSpeed+moveSpeed; } set { moveSpeed = value; } }
        public float BuffMoveSpeed {set { buffMoveSpeed = value; } }

        private float jumpForce;
        public float JumpForce { get { return jumpForce; } set { jumpForce = value; } }
        public bool IsDie { get { return currHP <= 0; } }
        public Action<float> hpBar;
        public List<Buff> moveBuffs = new List<Buff>();
        public Stat(int hp, float moveSpeed, float jumpForce)
        {
            this.maxHP = hp;
            this.currHP = hp;
            this.moveSpeed = moveSpeed;
            this.jumpForce = jumpForce;
            GameManager.GetInstance.moveBuff += GetMoveSpeedBuff;
        }
        public void GetDamaged(int damage)
        {
            HP -= damage;
        }
        public void GetHeal(int val)
        {
            HP += val;
        }
        public void GetMoveSpeedBuff(int value,float duration) 
        {
            buffMoveSpeed += value;
            
            moveBuffs.Add(new Buff(value, duration, ReleaseMoveSpeedBuff));
        }
        public void ReleaseMoveSpeedBuff(int value) 
        {
            buffMoveSpeed -= value;
        }
    }
    public class Buff
    {
        public int itemValue;
        public float duration;
        public Action<int> action;
        public Buff(int itemValue, float duration, Action<int> action)
        {
            this.itemValue = itemValue;
            this.duration = duration;
            this.action = action;
        }
    }
}
