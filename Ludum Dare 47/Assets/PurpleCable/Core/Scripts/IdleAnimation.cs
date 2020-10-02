using UnityEngine;

public class IdleAnimation : MonoBehaviour
{
    [SerializeField]
    private float Tick = 0.7f;

    [SerializeField]
    private Vector3 MinScale = new Vector3(0.95f, 1f, 1f);

    [SerializeField]
    private Vector3 MaxScale = new Vector3(1f, 1f, 1f);

    private Vector3 NormalScale = Vector3.zero;

    private float _timer = 0f;
    private int _scaleFactor = 1;

    private void Start()
    {
        NormalScale = transform.localScale;
    }

    private void Update()
    {
        _timer += Time.deltaTime * _scaleFactor;

        if (_timer >= Tick)
        {
            _timer = Tick;
            _scaleFactor = -1;
        }
        else if (_timer <= 0)
        {
            _timer = 0;
            _scaleFactor = 1;
        }

        transform.localScale = new Vector3(NormalScale.x * (MinScale.x + ((MaxScale.x - MinScale.x) * (_timer / Tick))), NormalScale.y * (MinScale.y + ((MaxScale.y - MinScale.y) * (_timer / Tick))), NormalScale.z * (MinScale.z + ((MaxScale.z - MinScale.z) * (_timer / Tick))));
    }
}
