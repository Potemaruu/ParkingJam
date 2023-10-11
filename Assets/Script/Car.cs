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
    private float shaketime = 0.3f;
    void Start()
    {
        moveVec = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveVec * Time.deltaTime;
        float angley = transform.rotation.eulerAngles.y;
        if(shake)
        {
            Quaternion rotation;
            rotation = Quaternion.AngleAxis(Random.Range(-3.0f, 3.0f), shakeaxis);
            //if (transform.rotation.eulerAngles.y == 90.0f || transform.rotation.eulerAngles.y == 270.0f)
            //{
            //    rotation = Quaternion.AngleAxis(Random.Range(-3.0f, 3.0f), shakeaxis);
            //}
            //else
            //{
            //    rotation = Quaternion.AngleAxis(Random.Range(-3.0f, 3.0f), shakeaxis);
            //}
            rotation = Quaternion.Euler(rotation.eulerAngles.x, angley, rotation.eulerAngles.z);
            transform.rotation = rotation;
            shaketime -= Time.deltaTime;
            //揺れの終わり
            if (shaketime < 0)
            {
                shake = false;
                shaketime = 0.3f;
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //縁石に当たったら
        if(collision.gameObject.CompareTag("Curb"))
        {
            //跳ね返らせる
            gameObject.GetComponent<Rigidbody>().AddForce(moveVec.normalized * -4f, ForceMode.Impulse);
            Curb curb = collision.gameObject.GetComponent<Curb>();
            curb.Shake();
        }
        else
        {
            //車に当たったら
            if(collision.gameObject.CompareTag("Car"))
            {
				//跳ね返らせる
				gameObject.GetComponent<Rigidbody>().AddForce(moveVec.normalized * -4f, ForceMode.Impulse);

				Car car = collision.gameObject.GetComponent<Car>();
                if(car.onRoad)
                {
                    //絵文字を出す
                    CreateEmoji(sadface);
				}
				else
                {
                    if(moveVec == Vector3.zero)
                    {
                        //絵文字を出す
                        CreateEmoji(sadface);

                        //揺らす
                        foreach(ContactPoint contact in collision.contacts) 
                        {
                            shake = true;
                            shakeaxis = contact.normal;
                            shakeaxis = new Vector3(-shakeaxis.z, shakeaxis.y, shakeaxis.x);
                        }
					}
				}
            }
        }
        moveVec = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        //バーに当たったら
        if (other.gameObject.CompareTag("Bar"))
        {
            //絵文字を出す
            CreateEmoji(funface);
        }
	}

    public void SetCoin()
    {
        //コインを一度だけ生成
        if(!alreadyCoin)
        {
            GameObject newObject = Instantiate(coin, transform.position + transform.up * 4, Quaternion.identity);
            Destroy(newObject, 1.0f);
            alreadyCoin = true;
        }
	}

    private void CreateEmoji(GameObject ob)
    {
		//絵文字を出す
		GameObject newObject = Instantiate(ob, transform.position + transform.up * 4, Quaternion.identity);
		newObject.transform.parent = transform;
		Destroy(newObject, 0.7f);
	}
}
