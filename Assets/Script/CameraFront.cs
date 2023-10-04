using UnityEngine;

public class CameraFront : MonoBehaviour
{
    private Camera mainCam;

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
        //�G�������J�����Ɍ�����
        transform.LookAt(mainCam.transform.position);
    }
}