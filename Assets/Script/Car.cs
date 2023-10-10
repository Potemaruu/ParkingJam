using TMPro.EditorUtilities;
using UnityEngine;

public class Car : MonoBehaviour
{
    public GameObject sadface;
    public GameObject funface;
    public GameObject coin;
    public Vector3 moveVec;
    public bool onRoad = false;

    private bool alreadyCoin = false;
    private bool shake = false;
    private Vector3 shakeaxis = Vector3.zero;
    private float shaketime = 1.0f;
    void Start()
    {
        moveVec = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveVec * Time.deltaTime;

        if(shake)
        {
            Quaternion rotation = Quaternion.AngleAxis(10.0f, shakeaxis);
            transform.rotation = rotation;
            Debug.Log("����Ă�");

            shaketime -= Time.deltaTime;
            if(shaketime < 0 )
            {
                shake = false;
                shaketime = 1.0f;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //���΂ɓ���������
        if(collision.gameObject.CompareTag("Curb"))
        {
            //���˕Ԃ点��
            gameObject.GetComponent<Rigidbody>().AddForce(moveVec.normalized * -4f, ForceMode.Impulse);
        }
        else
        {
            //�Ԃɓ���������
            if(collision.gameObject.CompareTag("Car"))
            {
				//���˕Ԃ点��
				gameObject.GetComponent<Rigidbody>().AddForce(moveVec.normalized * -4f, ForceMode.Impulse);

				Car car = collision.gameObject.GetComponent<Car>();
                if(car.onRoad)
                {
                    //�G�������o��
                    CreateEmoji(sadface);
				}
				else
                {
                    if(moveVec == Vector3.zero)
                    {
                        //�G�������o��
                        CreateEmoji(sadface);

                        //�h�炷
                        foreach(ContactPoint contact in collision.contacts) 
                        {
                            Vector3 shakeaxis = contact.normal;
                            shakeaxis = new Vector3(-shakeaxis.z, 0.0f, shakeaxis.x);
                            shake = true;
                        }
					}
				}
            }
        }
        moveVec = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        //�o�[�ɓ���������
        if (other.gameObject.CompareTag("Bar"))
        {
            //�G�������o��
            CreateEmoji(funface);
        }
	}

    public void SetCoin()
    {
        //�R�C������x��������
        if(!alreadyCoin)
        {
            GameObject newObject = Instantiate(coin, transform.position + transform.up * 4, Quaternion.identity);
            Destroy(newObject, 1.0f);
            alreadyCoin = true;
        }
	}

    private void CreateEmoji(GameObject ob)
    {
		//�G�������o��
		GameObject newObject = Instantiate(ob, transform.position + transform.up * 4, Quaternion.identity);
		newObject.transform.parent = transform;
		Destroy(newObject, 0.7f);
	}
}
