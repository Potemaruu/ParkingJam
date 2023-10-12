using UnityEngine;

public class CarCount : MonoBehaviour
{
    public int carcount = 0;
    public GameObject paper = null;

    private GameObject ui = null;
    private RectTransform uitransform = null;
    private bool flg = false;
    // Start is called before the first frame update
    void Start()
    {
        Car[] carComponents = FindObjectsOfType<Car>();
        carcount = carComponents.Length;

        ui = GameObject.Find("Canvas");
        uitransform = ui.GetComponent<RectTransform>();
        ui.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (carcount == 0 && ui != null)
        {
            ui.SetActive(true);

            if (!flg)
            {
                flg = true;
                {
                    GameObject par = Instantiate(paper, ui.transform.position, Quaternion.identity);
                    par.transform.parent = ui.transform;
                    par.transform.localPosition = new Vector3(100, -uitransform.rect.height / 2, 0.0f);
                    par.transform.rotation = Quaternion.Euler(-90, par.transform.rotation.eulerAngles.y, par.transform.rotation.eulerAngles.z);
                    par.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    par.layer = LayerMask.NameToLayer("UI");
                    Destroy(par, 2.0f);
                }
                {
                    GameObject par = Instantiate(paper, Vector3.zero, Quaternion.identity);
                    par.transform.parent = ui.transform;
                    par.transform.localPosition = new Vector3(-100, -uitransform.rect.height / 2, 0.0f);
                    par.transform.rotation = Quaternion.Euler(-90, par.transform.rotation.eulerAngles.y, par.transform.rotation.eulerAngles.z);
                    par.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    par.layer = LayerMask.NameToLayer("UI");
                    Destroy(par, 2.0f);
                }
            }
        }
    }
}
