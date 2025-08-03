using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    public float MoveSpeed = 10f;
    public float RotationSpeed = 75f;
    private Rigidbody _rb;

    private float _vInput;
    private float _hInput;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if (_rb == null)
        {
            Debug.LogError("Rigidbody component not found on PlayerBehavior script.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 直接使用Transform组件进行移动和旋转

        // _vInput = Input.GetAxis("Vertical") * MoveSpeed;
        // _hInput = Input.GetAxis("Horizontal") * RotationSpeed;

        // transform.Translate(Vector3.forward * _vInput * Time.deltaTime);
        // transform.Rotate(Vector3.up * _hInput * Time.deltaTime);

    }


    // 
    void FixedUpdate()
    {
        _vInput = Input.GetAxis("Vertical") * MoveSpeed;
        _hInput = Input.GetAxis("Horizontal") * RotationSpeed;

        // 使用Rigidbody进行物理移动和旋转
        _rb.MovePosition(transform.position + transform.forward * _vInput * Time.deltaTime);

        Quaternion rotation = Quaternion.Euler(0, _hInput * Time.deltaTime, 0);
        _rb.MoveRotation(_rb.rotation * rotation);
    }
}
