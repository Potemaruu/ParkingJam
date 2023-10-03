using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlickScript : MonoBehaviour
{
    Vector2 touchPos;
    RaycastHit _hit;
    GameObject carObject = null;


    // Start is called before the first frame update
    void Start()
    {

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
        else if (Input.GetMouseButtonUp(0) && carObject != null)
        {
            Rigidbody rb = carObject.GetComponent<Rigidbody>();

            //スワイプの方向を取得
            Vector2 releasePos = Input.mousePosition;
            Vector2 vec = releasePos - touchPos;

            //forwardをvector2に変換
            Vector2 fo;
            fo.x = carObject.transform.forward.x;
            fo.y = carObject.transform.forward.z;


            //forwardとの内積が０以上なら前に進める
            if (Vector2.Dot(vec, fo) > 0)
            {
                //rb.AddForce(hitObject.transform.forward * 10, ForceMode.Impulse);
                rb.velocity = carObject.transform.forward * 20;

            }
            else
            {
                //rb.AddForce(-hitObject.transform.forward * 10, ForceMode.Impulse);
                rb.velocity = -carObject.transform.forward * 20;
            }


            carObject = null;
        }
    }
}