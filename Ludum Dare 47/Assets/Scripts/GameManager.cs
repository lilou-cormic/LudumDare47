using PurpleCable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player Player = null;

    [SerializeField] SeasonBlock Spring = null;

    [SerializeField] SeasonBlock Summer = null;

    [SerializeField] SeasonBlock Autumn = null;

    [SerializeField] SeasonBlock Winter = null;

    private void Awake()
    {
        Spring.transform.position = Vector3.zero;
        Summer.transform.position = Vector3.zero;
        Autumn.transform.position = Vector3.zero;
        Winter.transform.position = Vector3.zero;

        Spring.gameObject.SetActive(true);
        Summer.gameObject.SetActive(false);
        Autumn.gameObject.SetActive(false);
        Winter.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        if (Player.transform.position.y > 6)
        {
            Player.transform.position += Vector3.down * 12;

            Spring.gameObject.SetActive(false);
            Summer.gameObject.SetActive(true);
            Autumn.gameObject.SetActive(false);
            Winter.gameObject.SetActive(false);
        }

        if (Player.transform.position.y < -6)
        {
            Player.transform.position += Vector3.up * 12;

            Spring.gameObject.SetActive(false);
            Summer.gameObject.SetActive(false);
            Autumn.gameObject.SetActive(false);
            Winter.gameObject.SetActive(true);
        }

        if (Player.transform.position.x > 11)
        {
            Player.transform.position += Vector3.left * 22;

            Spring.gameObject.SetActive(false);
            Summer.gameObject.SetActive(false);
            Autumn.gameObject.SetActive(true);
            Winter.gameObject.SetActive(false);
        }

        if (Player.transform.position.x < -11)
        {
            Player.transform.position += Vector3.right * 22;

            Spring.gameObject.SetActive(true);
            Summer.gameObject.SetActive(false);
            Autumn.gameObject.SetActive(false);
            Winter.gameObject.SetActive(false);
        }
    }
}
