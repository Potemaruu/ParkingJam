using UnityEngine;

public class BarCollision : MonoBehaviour
{
    private GameObject parent = null;
    private Bar bar = null;

    public GameObject particle = null;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        bar = parent.GetComponent<Bar>();
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        GameObject par = Instantiate(particle, parent.transform.position + Vector3.up * 5, Quaternion.identity);
        //par.transform.localScale = new Vector3();
        Destroy(par, 3.0f);
        Destroy(other.gameObject, 5.0f);
        //è’ìÀîªíË
        bar.MyTriggerEnter();
    }
}
