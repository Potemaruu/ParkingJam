using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject nextRoad = null;
    public bool collisoinaxisX = false;

    private Transform nextPos = null;

    // Start is called before the first frame update
    void Start()
    {
        if (nextRoad != null)
        {
            nextPos = nextRoad.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerStay(Collider other)
	{
        if (nextPos == null)
        {
            return;
        }
		if(other.gameObject.CompareTag("Car"))
        {
            if(collisoinaxisX)
            {
                if(Mathf.Abs(transform.position.z - other.transform.position.z) < 0.55f)
                {
                    other.transform.LookAt(new Vector3(nextPos.position.x, other.transform.position.y, nextPos.position.z));
                    Car car = other.GetComponent<Car>();
                    car.moveVec = other.transform.forward * 20.0f;
                    car.onRoad = true;
                    car.SetCoin();

                }
            }
            else
            {
                if (Mathf.Abs(transform.position.x - other.transform.position.x) < 0.55f)
                {
                    other.transform.LookAt(new Vector3(nextPos.position.x, other.transform.position.y, nextPos.position.z));
                    Car car = other.GetComponent<Car>();
                    car.moveVec = other.transform.forward * 20.0f;
                    car.onRoad = true;
                    car.SetCoin();

                }

            }
		}
	}
}
