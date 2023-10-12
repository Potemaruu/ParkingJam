using UnityEngine;

public class Car : MonoBehaviour
{
    public GameObject sadface;
    public GameObject funface;
    public GameObject coin;
    public Vector3 moveVec;
    public bool onRoad = false;
    public GameObject smoke;

    private bool alreadyCoin = false;
    private bool shake = false;
    private Vector3 shakeaxis = Vector3.zero;
    private float shaketime = 0.3f;
    private Quaternion angle;
    void Start()
    {
        moveVec = Vector3.zero;
        angle = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //�ړ�����
        transform.position += moveVec * Time.deltaTime;
        if(shake)
        {
            Quaternion rotation;
            rotation = Quaternion.AngleAxis(Random.Range(-3.0f, 3.0f), shakeaxis);
            if (shakeaxis == transform.right || shakeaxis == -transform.right)
            {
                //���E���瓖������
                rotation = Quaternion.AngleAxis(Random.Range(-3.0f, 3.0f), Vector3.forward);
            }
            else if(shakeaxis == transform.forward || shakeaxis == -transform.forward)
            {
                //�O�ォ�瓖������
                rotation = Quaternion.AngleAxis(Random.Range(-3.0f, 3.0f), Vector3.right);
            }
            rotation = Quaternion.Euler(rotation.eulerAngles.x, angle.eulerAngles.y, rotation.eulerAngles.z);
            transform.rotation = rotation;
            shaketime -= Time.deltaTime;
            //�h��̏I���
            if (shaketime < 0)
            {
                shake = false;
                shaketime = 0.3f;
                transform.rotation = angle;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //���΂ɓ���������
        if(collision.gameObject.CompareTag("Curb"))
        {
            //�p�[�e�B�N������
            GameObject par = Instantiate(smoke, transform.position + moveVec * 0.09f, Quaternion.identity);
            par.transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);
            Destroy(par, 2.0f);

            //���˕Ԃ点��
            gameObject.GetComponent<Rigidbody>().AddForce(moveVec.normalized * -5f, ForceMode.Impulse);
            Curb curb = collision.gameObject.GetComponent<Curb>();
            curb.Shake();
        }
        else
        {
            //�Ԃɓ���������
            if(collision.gameObject.CompareTag("Car"))
            {

                //���˕Ԃ点��
                gameObject.GetComponent<Rigidbody>().AddForce(moveVec.normalized * -5f, ForceMode.Impulse);

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
                            shake = true;
                            shakeaxis = contact.normal;
                            //shakeaxis = new Vector3(-shakeaxis.z, shakeaxis.y, shakeaxis.x);
                        }
					}
                    else
                    {
                        //�p�[�e�B�N������
                        GameObject par = Instantiate(smoke, transform.position + moveVec * 0.09f, Quaternion.identity);
                        par.transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);
                        Destroy(par, 2.0f);

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
