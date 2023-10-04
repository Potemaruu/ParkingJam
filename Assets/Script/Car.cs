using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public GameObject emozi;
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
        if(collision.gameObject.CompareTag("Curb"))
        {
            GameObject newObject = Instantiate(emozi, gameObject.transform.position + gameObject.transform.up * 4, Quaternion.identity);
            newObject.transform.parent = transform;
            Destroy(newObject, 1.0f);
            gameObject.GetComponent<Rigidbody>().AddForce(moveVec * -80, ForceMode.Impulse);
        }
        moveVec = Vector3.zero;

    }
}
