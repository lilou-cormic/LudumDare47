using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float SpeedFactor = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponentInParent<ISpeed>()?.ChangeSpeed(SpeedFactor);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponentInParent<ISpeed>()?.ChangeSpeed(1);
    }
}
