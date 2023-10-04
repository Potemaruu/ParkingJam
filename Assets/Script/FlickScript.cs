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
        //�J������Y����]�ʂ��擾
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraY = camera.transform.rotation.eulerAngles.y;
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
        else if (Input.GetMouseButtonUp(0) && carObject != null)
        {
            Rigidbody rb = carObject.GetComponent<Rigidbody>();

            Car car = carObject.GetComponent<Car>();

            //�X���C�v�̕������擾
            Vector2 releasePos = Input.mousePosition;
            Vector2 vec = releasePos - touchPos;

            //�J�����̌������l������   
            Quaternion rotation = Quaternion.Euler(0f, -cameraY, 0f);
            Vector3 rotad = rotation * carObject.transform.forward;

            //forward��vector2�ɕϊ�
            Vector2 fo;
            fo.x = rotad.x;
            fo.y = rotad.z;

            //forward�Ƃ̓��ς��O�ȏ�Ȃ�O�ɐi�߂�
            if (Vector2.Dot(vec.normalized, fo.normalized) > 0)
            {
                //rb.velocity = carObject.transform.forward * 20;
                car.moveVec = carObject.transform.forward * 0.05f;
            }
            else
            {
                //rb.velocity = -carObject.transform.forward * 20;
                car.moveVec = -carObject.transform.forward * 0.05f;

            }
            carObject = null;
        }
    }
}