using UnityEngine;

public class CarCount : MonoBehaviour
{
    public int carcount = 0;

    private GameObject ui = null;
    // Start is called before the first frame update
    void Start()
    {
        Car[] carComponents = FindObjectsOfType<Car>();
        carcount = carComponents.Length;

        ui = GameObject.Find("Canvas");
        ui.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (carcount == 0 && ui != null)
        {
            ui.SetActive(true);
        }
    }
}
