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
        //�Ԃ����΂ɓ���������
        if(collision.gameObject.CompareTag("Curb") || collision.gameObject.CompareTag("Car"))
        {
            //���˕Ԃ点��
            gameObject.GetComponent<Rigidbody>().AddForce(moveVec * -80, ForceMode.Impulse);
            //�G�������o��
            GameObject newObject = Instantiate(sadface, gameObject.transform.position + gameObject.transform.up * 4, Quaternion.identity);
            newObject.transform.parent = transform;
            Destroy(newObject, 1.0f);
        }
        moveVec = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        //�o�[�ɓ���������
        if (other.gameObject.CompareTag("Bar"))
        {
            //�G�������o��
            GameObject newObject = Instantiate(funface, gameObject.transform.position + gameObject.transform.up * 4, Quaternion.identity);
            newObject.transform.parent = transform;
            Destroy(newObject, 1.0f);
        }

    }
}
