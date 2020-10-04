using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TimerText = null;

    private void LateUpdate()
    {
        TimerText.text = Mathf.CeilToInt(GameManager.TimeLeft).ToString();
    }
}
