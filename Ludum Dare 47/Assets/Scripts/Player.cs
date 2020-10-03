using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerDisplay))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    private PlayerDisplay PlayerDisplay;

    [SerializeField] SpriteRenderer SpriteRenderer = null;

    [SerializeField] Animator Animator;

    [SerializeField] float Speed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerDisplay = GetComponent<PlayerDisplay>();
    }

    Vector2 movement = Vector2.zero;

    private void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        Animator.SetFloat("Speed", movement.magnitude);

        if (movement.magnitude > 0)
            SpriteRenderer.flipX = (movement.x < 0);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (movement.normalized * Speed * Time.deltaTime));
    }
}
