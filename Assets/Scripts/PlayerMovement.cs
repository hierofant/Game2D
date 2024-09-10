using Assets.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0, 20)] private float JumpForce;
    [SerializeField, Range(0, 20)] private float speed;
    [SerializeField] private Transform colider;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private float jumpFactor;
    [SerializeField] private LayerMask ground;
    private PlayerInput playerinput;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerinput = GetComponent<PlayerInput>();
    }
    private void FixedUpdate()
    {
        Vector3 overLapCircle = colider.position;
        isGrounded = Physics2D.OverlapCircle(overLapCircle, jumpFactor, ground);
    }
    public void Movement(float horizontalDirect, bool isJumpButtonPressed)
    {
        if (isJumpButtonPressed)
        {
            Jump();
        }
        if (Mathf.Abs(horizontalDirect) > 0.01)
        {
            Move(horizontalDirect);
        }
    }
    private void Jump()
    {
        if (isGrounded == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }
    }
    private void Move(float direct)
    {
        rb.velocity = new Vector2(direct * speed, rb.velocity.y);
    }
}
