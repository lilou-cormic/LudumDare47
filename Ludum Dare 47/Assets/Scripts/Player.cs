using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerDisplay))]
public class Player : MonoBehaviour, ISpeed
{
    private PlayerDisplay PlayerDisplay;

    [SerializeField] SpriteRenderer SpriteRenderer = null;

    [SerializeField] Animator Animator;

    [SerializeField] float Speed = 5f;

    private float SpeedFactor = 1f;

    private void Awake()
    {
        PlayerDisplay = GetComponent<PlayerDisplay>();
    }

    Vector3 movement = Vector3.zero;

    private void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Animator.SetFloat("Speed", movement.magnitude);

        if (movement.magnitude > 0)
            SpriteRenderer.flipX = (movement.x < 0);
    }

    private void FixedUpdate()
    {
        transform.position += (movement.normalized * Speed * SpeedFactor * Time.deltaTime);
    }

    public void ChangeSpeed(float factor)
    {
        SpeedFactor = factor;
    }
}
