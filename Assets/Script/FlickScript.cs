using UnityEngine;

public class FlickScript : MonoBehaviour
{
    GameObject camera = null;

    Vector2 touchPos;
    RaycastHit _hit;
    GameObject carObject = null;

    float cameraY;

    // Start is called before the first frame update
    void Start()
    {
        //カメラのY軸回転量を取得
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraY = camera.transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {

        //Unity上での操作
        if (Input.GetMouseButtonDown(0))
        {
            //クリックしたところにオブジェクトがあるか
            var _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit))
            {
                if(_hit.collider != null && _hit.collider.gameObject.CompareTag("Car"))
                {
                    // 当たったオブジェクトのタグがcarなら取得
                    carObject = _hit.collider.gameObject; 
                }
            }
            touchPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0) && carObject != null && Vector2.Distance(new Vector3(touchPos.x, touchPos.y, 0.0f), Input.mousePosition) > 40.0f)
        {
            Rigidbody rb = carObject.GetComponent<Rigidbody>();

            Car car = carObject.GetComponent<Car>();

            //スワイプの方向を取得
            Vector2 releasePos = Input.mousePosition;
            Vector2 vec = releasePos - touchPos;

            //カメラの向きを考慮する   
            Quaternion rotation = Quaternion.Euler(0f, -cameraY, 0f);
            Vector3 rotad = rotation * carObject.transform.forward;

            //vector2に変換
            Vector2 fo;
            fo.x = rotad.x;
            fo.y = rotad.z;

            //forwardとの内積が０以上なら前に進める
            if (Vector2.Dot(vec.normalized, fo.normalized) > 0)
            {
                car.moveVec = carObject.transform.forward * 20.0f;
            }
            else
            {
                car.moveVec = -carObject.transform.forward * 20.0f;
            }
            carObject = null;
        }
    }
}