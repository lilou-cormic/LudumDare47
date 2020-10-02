using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchOnMouseOver : MonoBehaviour
{
    [SerializeField] float Tick = 0.3f;

    [SerializeField] Vector3 MaxScale = new Vector3(1f, 1f, 1f);

    [SerializeField] Transform TransformToScale = null;

    private Vector3 NormalScale = Vector3.zero;

    private float _timer = 0f;

    private bool _isMouseOver = false;

    private bool _isFullSize = false;

    private void Start()
    {
        if (TransformToScale == null)
            TransformToScale = transform;

        NormalScale = TransformToScale.localScale;
    }

    private void Update()
    {
        if (!_isMouseOver || _isFullSize)
            return;

        _timer += Time.deltaTime;

        if (_timer >= Tick)
        {
            _timer = Tick;
            _isFullSize = true;
            TransformToScale.localScale = new Vector3(NormalScale.x * MaxScale.x, NormalScale.y * MaxScale.y, NormalScale.z * MaxScale.z);
            return;
        }

        float x = Mathf.Lerp(NormalScale.x, NormalScale.x * MaxScale.x, _timer / Tick);
        float y = Mathf.Lerp(NormalScale.y, NormalScale.y * MaxScale.y, _timer / Tick);
        float z = Mathf.Lerp(NormalScale.z, NormalScale.z * MaxScale.z, _timer / Tick);

        TransformToScale.localScale = new Vector3(x, y, z);
    }

    private void OnMouseEnter()
    {
        _isMouseOver = true;
    }

    private void OnMouseExit()
    {
        _isMouseOver = false;

        TransformToScale.localScale = NormalScale;

        _timer = 0;

        _isFullSize = false;
    }
}
