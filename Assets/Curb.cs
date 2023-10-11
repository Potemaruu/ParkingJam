using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curb : MonoBehaviour
{
    private float shaketime = 0.3f;
    private bool shake = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shake)
        {
            Quaternion rotation;
            rotation = Quaternion.Euler(Random.Range(-3.0f, 3.0f), transform.rotation.eulerAngles.y, 0.0f);

            transform.rotation = rotation;

            shaketime -= Time.deltaTime;
            //óhÇÍÇÃèIÇÌÇË
            if (shaketime < 0)
            {
                shake = false;
                shaketime = 0.3f;
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            }
        }

    }

    public void Shake()
    {
        shake = true;
    }
}
