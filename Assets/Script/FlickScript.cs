using UnityEngine;

public class FlickScript : MonoBehaviour
{
    GameObject mainCamera = null;

    Vector2 touchPos;
    RaycastHit _hit;
    GameObject carObject = null;

    float cameraY;

    // Start is called before the first frame update
    void Start()
    {
        //�J������Y����]�ʂ��擾
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraY = mainCamera.transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {

        //Unity��ł̑���
        if (Input.GetMouseButtonDown(0))
        {
            //�N���b�N�����Ƃ���ɃI�u�W�F�N�g�����邩
            var _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit))
            {
                if(_hit.collider != null && _hit.collider.gameObject.CompareTag("Car"))
                {
                    // ���������I�u�W�F�N�g�̃^�O��car�Ȃ�擾
                    carObject = _hit.collider.gameObject; 
                }
            }
            touchPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0) && carObject != null && Vector2.Distance(new Vector3(touchPos.x, touchPos.y, 0.0f), Input.mousePosition) > 20.0f)
        {
            Rigidbody rb = carObject.GetComponent<Rigidbody>();

            Car car = carObject.GetComponent<Car>();

            //�X���C�v�̕������擾
            Vector2 releasePos = Input.mousePosition;
            Vector2 vec = releasePos - touchPos;

            //�J�����̌������l������   
            Quaternion rotation = Quaternion.Euler(0f, -cameraY, 0f);
            Vector3 rotad = rotation * carObject.transform.forward;

            //vector2�ɕϊ�
            Vector2 fo;
            fo.x = rotad.x;
            fo.y = rotad.z;

            //forward�Ƃ̓��ς��O�ȏ�Ȃ�O�ɐi�߂�
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