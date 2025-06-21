using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    public float mouseSensitivity = 400f; //���콺����
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

        MouseY = Mathf.Clamp(MouseY, -mouseClampRange, mouseClampRange); //Clamp�� ���� �ּҰ� �ִ밪�� ���� �ʵ�����

        transform.localRotation = Quaternion.Euler(0f, MouseX, 0f);// �� ���� �Ѳ����� ���
        camera.transform.localRotation = Quaternion.Euler(MouseY, 0f, 0f);// �� ���� �Ѳ����� ���
    }
}
