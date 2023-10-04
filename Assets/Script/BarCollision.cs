using UnityEngine;

public class BarCollision : MonoBehaviour
{
    private GameObject parent = null;
    private Bar bar = null;
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
        //è’ìÀîªíË
        bar.MyTriggerEnter();
    }
}
