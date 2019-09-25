using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineAI : MonoBehaviour
{

    public GameObject owner;
    private float time_remaining;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        time_remaining = 10;
    }
    private void OnDestroy()
    {
        owner.GetComponent<TankAI>().live_mines -= 1;
        owner.GetComponent<TankAI>().active_mines.Remove(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        time_remaining -= Time.deltaTime;

        if (time_remaining < 3)
        {
            gameObject.GetComponent<FlipTexture>().enabled = true;
        }

        if (time_remaining < 0)
        {
            boom();
        }

        List<GameObject> tanks = owner.GetComponent<TankAI>().level.tanks;
        for (int i = 0; i < tanks.Count; i++)
        {
            if (tanks[i] == owner) continue;

            Vector3 tank_pos = tanks[i].transform.position;
            if (Mathf.Abs(tank_pos.x - transform.position.x) < .78f * LevelLoader.ratio && Mathf.Abs(tank_pos.z - transform.position.z) < .78f * LevelLoader.ratio)
            {
                boom();
            }
        }
        // TODO else if tanks in range
    }

    public void boom()
    {
        List<GameObject> tanks = owner.GetComponent<TankAI>().level.tanks;
        for (int i = 0; i < tanks.Count; i++)
        {
            tanks[i].GetComponent<TankAI>().level.BoomLevel(transform.position, 3 * LevelLoader.ratio);
            Vector3 tank_pos = tanks[i].transform.position;
            if ((tank_pos.x - transform.position.x) * (tank_pos.x - transform.position.x) + (tank_pos.z - transform.position.z) * (tank_pos.z - transform.position.z) < (9 * LevelLoader.ratio * LevelLoader.ratio))
            {
                tanks[i].GetComponent<TankAI>().Kill();
            }
        }
        GameObject expl = Instantiate(explosion, transform.position, Quaternion.AngleAxis(90, Vector3.up));
        expl.GetComponent<Cloneable>().enabled = true;
        Destroy(expl, 1);
        Destroy(gameObject);
    }
}
