using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFront : MonoBehaviour
{

    Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        TurningImage();
    }

    // �I�u�W�F�N�g�̌��������C���J�����̕����Ɍ�����
    private void TurningImage()
    {
        transform.LookAt(mainCam.transform.position);
    }
}