using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject nextRoad = null;
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
			if ((transform.position - other.transform.position).magnitude < new Vector3(0.6f, 0.0f, 0.0f).magnitude)
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
