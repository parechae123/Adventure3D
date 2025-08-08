using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace PlayerDefine
{
    public class InputHandler
    {
        // 인풋시스템 - C# 제너레이트
        public PlayerControl input;
        public Action<Vector3> OnMove;
        public Action OnJump;
        public Action<Vector2> OnMouseDelta;

        public Vector3 dirr;
        public InputHandler(Action<Vector3> moveAction,Action jumpAction, Action<Vector2> mouseAction)
        {
            input = new PlayerControl();           

            OnMove = moveAction;
            OnJump = jumpAction;
            OnMouseDelta = mouseAction;

            input.Player.Move.performed += Move;
            input.Player.Move.canceled += Move;
            input.Player.Jump.started += Jump;
            input.Player.Enable();
            input.Player.MouseDelta.performed += MouseDelta;
            input.Interactions.MouseDelta.performed += MouseDelta;
            //input.Player.Interaction.started += OnJump;
        }
        public void DisablePlayer()
        {
            input.Player.Disable();
        }
        public void EnablePlayer()
        {
            input.Player.Enable();
        }
        public void InputUpdate()
        {
            if (dirr == Vector3.zero) return;
            OnMove?.Invoke(dirr);
        }
        public void Init()
        {
            
        }
        void Move(CallbackContext cb)
        {
            Vector2 cbVec = cb.ReadValue<Vector2>();
            Vector3 dir = new Vector3(cbVec.x,0,cbVec.y);
            dirr = dir;
        }
        void Jump(CallbackContext cb)
        {
            OnJump?.Invoke();
        }
        void MouseDelta(CallbackContext cb)
        {
            OnMouseDelta.Invoke(cb.ReadValue<Vector2>());
        }

        /*KeyCode forward;
        KeyCode back;
        KeyCode right;
        KeyCode left;

        KeyCode jump;
        KeyCode inputInteractKey;
        Transform tr;
        public InputHandler(KeyCode forward,KeyCode back,KeyCode right,KeyCode left,KeyCode jump,KeyCode inputInteractKey,Transform tr)
        {
            this.forward = forward;
            this.back = back;
            this.right = right;
            this.left = left;
            this.jump = jump;
            this.inputInteractKey = inputInteractKey;
            this.tr = tr;
        }
        public bool MovePress()
        {
            return Input.GetKey(forward)|| Input.GetKey(back) || Input.GetKey(right) || Input.GetKey(left);
        }
        public Vector3 GetMoveDir()
        {
            float y = Input.GetKey(forward) ? 1f : Input.GetKey(back) ? -1f : 0f;
            float x = Input.GetKey(right) ? 1f : Input.GetKey(left) ? -1f : 0f;
            Vector3 dir = Vector3.zero;
            dir += tr.forward * y;
            dir += tr.right * x;
            return dir.normalized;
        }
        public bool JumpPress()
        {
            return Input.GetKeyDown(jump);
        }
        public bool InteractPress()
        {
            return Input.GetKeyDown(inputInteractKey);
        }*/
    }
}
