using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAI : MonoBehaviour
{

    public int live_bullets;
    public int live_mines;

    public List<GameObject> active_mines;
    public List<GameObject> active_bullets;
    public GameObject explosion;
    public GameObject bullet;
    public GameObject fast_bullet;
    public GameObject mine;
    public LevelLoader level;

    public GameObject playerTank;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Kill()
    {
        enabled = false;
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
        level.tanks.Remove(gameObject);

        GameObject expl = Instantiate(explosion, transform.position, Quaternion.AngleAxis(90, Vector3.up));
        expl.transform.localScale = new Vector3(6, 6, 6);
        expl.GetComponent<Cloneable>().enabled = true;
        Destroy(expl, 1);
    }
}
