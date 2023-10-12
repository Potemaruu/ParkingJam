using TMPro.EditorUtilities;
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
        //移動処理
        transform.position += moveVec * Time.deltaTime;
        if(shake)
        {
            Quaternion rotation;
            rotation = Quaternion.AngleAxis(Random.Range(-3.0f, 3.0f), shakeaxis);
            if (shakeaxis == transform.right || shakeaxis == -transform.right)
            {
                //左右から当たった
                rotation = Quaternion.AngleAxis(Random.Range(-3.0f, 3.0f), Vector3.forward);
            }
            else if(shakeaxis == transform.forward || shakeaxis == -transform.forward)
            {
                //前後から当たった
                rotation = Quaternion.AngleAxis(Random.Range(-3.0f, 3.0f), Vector3.right);
            }
            rotation = Quaternion.Euler(rotation.eulerAngles.x, angle.eulerAngles.y, rotation.eulerAngles.z);
            transform.rotation = rotation;
            shaketime -= Time.deltaTime;
            //揺れの終わり
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
        //縁石に当たったら
        if(collision.gameObject.CompareTag("Curb"))
        {
            //パーティクル生成
            GameObject par = Instantiate(smoke, transform.position + moveVec * 0.09f, Quaternion.identity);
            par.transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);
            Destroy(par, 2.0f);

            //跳ね返らせる
            gameObject.GetComponent<Rigidbody>().AddForce(moveVec.normalized * -5f, ForceMode.Impulse);
            Curb curb = collision.gameObject.GetComponent<Curb>();
            curb.Shake();
        }
        else
        {
            //車に当たったら
            if(collision.gameObject.CompareTag("Car"))
            {

                //跳ね返らせる
                gameObject.GetComponent<Rigidbody>().AddForce(moveVec.normalized * -5f, ForceMode.Impulse);

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
                            //shakeaxis = new Vector3(-shakeaxis.z, shakeaxis.y, shakeaxis.x);
                        }
					}
                    else
                    {
                        //パーティクル生成
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
