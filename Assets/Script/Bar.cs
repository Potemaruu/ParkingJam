using UnityEngine;

public class Bar : MonoBehaviour
{
    public GameObject pivot = null;

    private bool isRotation = false;
    private bool isUp = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //boxCol.transform.rotation = initRotation;
        if (isRotation)
        {
            if (isUp)
            {
                //è„è∏íÜ
                pivot.transform.Rotate(0.0f, 0.0f, -180.0f * Time.deltaTime);
                if (transform.rotation.eulerAngles.z <= 270.0f)
                {
                    isUp = false;
                }
            }
            else
            {
                //â∫ç~íÜ
                pivot.transform.Rotate(0.0f, 0.0f, 180.0f * Time.deltaTime);
                if (transform.rotation.eulerAngles.z >= 359.0f)
                {

                    isRotation = false;
                }
            }

        }
    }

    public void MyTriggerEnter()
    {
        isRotation = true;
        isUp = true;
    }
}