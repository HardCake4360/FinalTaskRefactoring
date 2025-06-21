using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    public float mouseSensitivity = 400f; //마우스감도
    public float mouseClampRange;
    public GameObject camera;

    private float MouseY;
    private float MouseX;

    void Update()
    {
        Rotate();
    }
    private void Rotate()
    {

        MouseX += Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;

        MouseY -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        MouseY = Mathf.Clamp(MouseY, -mouseClampRange, mouseClampRange); //Clamp를 통해 최소값 최대값을 넘지 않도록함

        transform.localRotation = Quaternion.Euler(0f, MouseX, 0f);// 각 축을 한꺼번에 계산
        camera.transform.localRotation = Quaternion.Euler(MouseY, 0f, 0f);// 각 축을 한꺼번에 계산
    }
}
