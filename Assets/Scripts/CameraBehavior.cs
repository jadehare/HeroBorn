using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavio : MonoBehaviour
{
    public Vector3 CamOffset = new Vector3(0, 1.2f, -2.6f);

    [SerializeField]
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // 相机更新使用LateUpdate以确保在所有物理更新后执行
    void LateUpdate()
    {
        transform.position = player.TransformPoint(CamOffset);
        transform.LookAt(player);
    }
}
