using UnityEngine;

public class fixed_fps : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		Application.targetFrameRate = 60;
    }
}
