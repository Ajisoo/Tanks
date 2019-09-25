using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateMine(GameObject owner, Vector3 pos)
    {
        GameObject new_bullet = Instantiate(gameObject, new Vector3(pos.x, -1.68f, pos.z), Quaternion.identity);
        new_bullet.GetComponent<MineSpawner>().enabled = false;
        new_bullet.GetComponent<Cloneable>().enabled = true;
        new_bullet.GetComponent<MineAI>().enabled = true;
        new_bullet.GetComponent<MineAI>().owner = owner;
        owner.GetComponent<TankAI>().live_mines += 1;
        owner.GetComponent<TankAI>().active_mines.Add(new_bullet);
    }
}
