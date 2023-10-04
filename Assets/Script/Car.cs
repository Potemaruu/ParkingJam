using UnityEngine;

public class Car : MonoBehaviour
{
    public GameObject sadface;
    public GameObject funface;
    public Vector3 moveVec;
    // Start is called before the first frame update
    void Start()
    {
        moveVec = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveVec;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //車か縁石に当たったら
        if(collision.gameObject.CompareTag("Curb") || collision.gameObject.CompareTag("Car"))
        {
            //跳ね返らせる
            gameObject.GetComponent<Rigidbody>().AddForce(moveVec * -80, ForceMode.Impulse);
            //絵文字を出す
            GameObject newObject = Instantiate(sadface, gameObject.transform.position + gameObject.transform.up * 4, Quaternion.identity);
            newObject.transform.parent = transform;
            Destroy(newObject, 1.0f);
        }
        moveVec = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        //バーに当たったら
        if (other.gameObject.CompareTag("Bar"))
        {
            //絵文字を出す
            GameObject newObject = Instantiate(funface, gameObject.transform.position + gameObject.transform.up * 4, Quaternion.identity);
            newObject.transform.parent = transform;
            Destroy(newObject, 1.0f);
        }

    }
}
