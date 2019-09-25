using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChild : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject copy = Instantiate(gameObject, new Vector3(0, 0, 0), Quaternion.identity);
        copy.GetComponent<SpawnChild>().enabled = false;
        copy.GetComponent<TankAI>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
