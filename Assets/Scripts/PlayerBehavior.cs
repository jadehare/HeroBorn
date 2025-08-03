using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    public float MoveSpeed = 10f;
    public float RotationSpeed = 75f;


    private float _vInput;
    private float _hInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _vInput = Input.GetAxis("Vertical") * MoveSpeed;
        _hInput = Input.GetAxis("Horizontal") * RotationSpeed;

        transform.Translate(Vector3.forward * _vInput * Time.deltaTime);
        transform.Rotate(Vector3.up * _hInput * Time.deltaTime);

    }
}
