using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SimpleJoystick : MonoBehaviour
{
    [SerializeField] private float _xLimit;
    [SerializeField] private float _zLimit;
    [SerializeField] private RealPlayer _player;

    private Vector3 _previousPosition;
    private bool _isMouseDowned;
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isMouseDowned && Input.GetMouseButtonDown(0))
        {
            _previousPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            _isMouseDowned = true;
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isMouseDowned = false;
        }

        if (!_isMouseDowned) return;

        var distance = _camera.ScreenToWorldPoint(Input.mousePosition);

        var difference = -1f * (_previousPosition - distance);
        difference.y = 0;
        Debug.Log(difference);

        _previousPosition = distance;

        if (difference.x == 0 && difference.z == 0) return;

        if (difference.magnitude > _player.Speed * Time.deltaTime)
        {
            difference = difference.normalized * _player.Speed * Time.deltaTime;
        }

        transform.position += difference;

        if (transform.position.x > _xLimit)
        {
            transform.position = new Vector3(_xLimit, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -_xLimit)
        {
            transform.position = new Vector3(-_xLimit, transform.position.y, transform.position.z);
        }

        if (transform.position.z > _zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _zLimit);
        }
        else if (transform.position.z < -_zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -_zLimit);
        }
    }
}
