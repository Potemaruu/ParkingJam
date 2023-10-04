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

    // オブジェクトの向きをメインカメラの方向に向ける
    private void TurningImage()
    {
        //絵文字をカメラに向ける
        transform.LookAt(mainCam.transform.position);
    }
}