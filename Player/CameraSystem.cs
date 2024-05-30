using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public Vector3 firstPosition;

    void Start()
    {
        firstPosition = transform.position;
    }

    private void Update()
    {
        Vector3 inputDic = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W)) inputDic.z = +1f;
        if (Input.GetKey(KeyCode.S)) inputDic.z = -1f;
        if (Input.GetKey(KeyCode.A)) inputDic.x = -1f;
        if (Input.GetKey(KeyCode.D)) inputDic.x = +1f;

        Vector3 moveDir = transform.forward * inputDic.z + transform.right * inputDic.x;
        float moveSpeed = 25f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        float rotateDic = 0f;
        if (Input.GetKey(KeyCode.Q))
        {
            rotateDic = +1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotateDic = -1f;
        }

        float rotateSpeed = 90f;
        transform.eulerAngles += new Vector3(0, rotateDic * rotateSpeed * Time.deltaTime, 0);
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("OutSide"))
        {
            transform.position = firstPosition;
        }
    }
}