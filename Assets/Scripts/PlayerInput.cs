using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerInput : MonoBehaviour
    {
        private PlayerMovement PlayerMovement;
        public bool right;
        public bool death;
        public float horizontalDirect;
        public bool isJumpButtonPressed;
        private Animator animator;

        private void Awake()
        {
            PlayerMovement = GetComponent<PlayerMovement>();
            animator = GetComponent<Animator>();
        }
        void Update()
        {
            if (death != true)
            {
                horizontalDirect = Input.GetAxisRaw("Horizontal");
                isJumpButtonPressed = Input.GetButtonDown("Jump");
                PlayerMovement.Movement(horizontalDirect, isJumpButtonPressed);
                animator.SetBool("IsWalking", horizontalDirect != 0);
            }
            if (horizontalDirect < 0)
            {
                transform.rotation = Quaternion.Euler(0, -180, 0);
            }
            else if (horizontalDirect > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
