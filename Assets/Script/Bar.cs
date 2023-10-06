using UnityEngine;

public class Bar : MonoBehaviour
{
    public GameObject pivot = null;

    private bool isRotation = false;
    private bool isUp = false;
    private float waitTime = 0.5f;

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
                if (Mathf.Abs(180 - transform.rotation.eulerAngles.z) <= 90.0f)
                {
					waitTime -= Time.deltaTime;
					if (waitTime < 0)
					{
						isUp = false;
					}
				}
				else
                {
					pivot.transform.Rotate(0.0f, 0.0f, -180.0f * Time.deltaTime);
				}
			}
            else
            {
                //â∫ç~íÜ
				if (Mathf.Abs(90 - transform.rotation.eulerAngles.z) <= 90.0f)
				{
					 isRotation = false;
				}
                else
                {
					pivot.transform.Rotate(0.0f, 0.0f, 180.0f * Time.deltaTime);
				}
			}

        }
    }

    public void MyTriggerEnter()
    {
        isRotation = true;
        isUp = true;
        waitTime = 0.5f;
    }
}