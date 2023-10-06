using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 5.0f * Time.deltaTime, transform.position.z);
		transform.Rotate(Vector3.up * 900.0f * Time.deltaTime);
	}
}
