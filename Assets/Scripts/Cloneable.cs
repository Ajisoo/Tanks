using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloneable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnEnable()
    {
        LevelLoader.reset += DestroySelf;
    }

    void OnDisable()
    {
        LevelLoader.reset -= DestroySelf;
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
