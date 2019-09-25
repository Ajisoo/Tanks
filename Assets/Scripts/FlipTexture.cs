using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipTexture : MonoBehaviour
{
    public Material m1;
    public Material m2;

    private readonly float time_before_swap = 0.21f;
    private float remaining_time;
    private bool whichMat;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material = m1;
        remaining_time = time_before_swap;
        whichMat = false;
    }

    // Update is called once per frame
    void Update()
    {
        remaining_time -= Time.deltaTime;
        if (remaining_time < 0)
        {
            remaining_time = time_before_swap;
            whichMat = !whichMat;
            if (whichMat) gameObject.GetComponent<Renderer>().material = m1;
            else gameObject.GetComponent<Renderer>().material = m2;
        }
    }
}
