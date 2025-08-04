using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    public float MoveSpeed = 10f;
    public float RotationSpeed = 75f;

    public float JumpVelocity = 5f;

    public float DistanceToGround = 0.1f;

    public LayerMask GroudLayer;

    private CapsuleCollider _col;

    private Rigidbody _rb;

    private float _vInput;
    private float _hInput;

    private bool _isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if (_rb == null)
        {
            Debug.LogError("Rigidbody component not found on PlayerBehavior script.");
        }

        _col = GetComponent<CapsuleCollider>();
        if (_col == null)
        {
            Debug.LogError("CapsuleCollider component not found on PlayerBehavior script.");
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

        //
        _isJumping |= Input.GetKeyDown(KeyCode.J);

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
        // 物理检测上一致，放在fixedupdate中判断。
        bool isGrounded = IsGrounded();
        if (isGrounded && _isJumping)
        {
            _rb.AddForce(Vector3.up * JumpVelocity, ForceMode.Impulse);
        }
        _isJumping = false;

    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);
        // 忽略Trigger碰撞器
        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, DistanceToGround, GroudLayer, QueryTriggerInteraction.Ignore);
        return grounded;
    }
}
