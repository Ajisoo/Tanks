using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAI : MonoBehaviour
{
    public GameObject owner;
    public int bounces_remaining;
    private bool can_hit_owner;

    public float speed;
    public static readonly float DEG_TO_RAD = Mathf.PI / 180;

    // Start is called before the first frame update
    void Start()
    {
        can_hit_owner = false;
        float angle = -transform.rotation.eulerAngles.y + 90;
        transform.position = new Vector3(transform.position.x + Mathf.Cos(angle * DEG_TO_RAD) * 1, 1.8f, transform.position.z + Mathf.Sin(angle * DEG_TO_RAD) * 1);
        (Vector3 new_pos, bool flip_x, bool flip_z) = owner.GetComponent<TankAI>().level.CheckCollision(transform.position, new Vector3(transform.position.x + Mathf.Cos(angle * DEG_TO_RAD) * 6, 1.8f, transform.position.z + Mathf.Sin(angle * DEG_TO_RAD) * 6), 0.1f, false);
        transform.position = new_pos;
        float new_angle = angle;
        if (flip_x)
        {
            bounces_remaining--;
            new_angle = 180 - new_angle;
        }
        if (flip_z)
        {
            bounces_remaining--;
            new_angle = -new_angle;
        }
        if (bounces_remaining < 0)
        {
            Destroy(gameObject);
        }
        transform.rotation = Quaternion.AngleAxis(-(new_angle - angle), Vector3.up) * transform.rotation;
    }

    private void OnDestroy()
    {
        owner.GetComponent<TankAI>().active_bullets.Remove(gameObject);
        owner.GetComponent<TankAI>().live_bullets -= 1;
    }

    // Update is called once per frame
    void Update()
    {

        List<GameObject> tanks = owner.GetComponent<TankAI>().level.tanks;
        for (int i = 0; i < tanks.Count; i++)
        {
            //Debug.Log("Checking tank: " + i);
            Vector3 tank_pos = tanks[i].transform.position;
            //Debug.Log("TEST");
            if (!(tanks[i] == owner && !can_hit_owner) && Mathf.Abs(tank_pos.x - transform.position.x) < .58f * LevelLoader.ratio && Mathf.Abs(tank_pos.z - transform.position.z) < .58f * LevelLoader.ratio)
            {
                tanks[i].GetComponent<TankAI>().Kill();
                Destroy(gameObject);
            }
            List<GameObject> mines = tanks[i].GetComponent<TankAI>().active_mines;
            for (int j = 0; j < mines.Count; j++)
            {
                //Debug.Log("Checking mine: " + j);
                Vector3 mine_pos = mines[j].transform.position;
                if ((mine_pos.x - transform.position.x) * (mine_pos.x - transform.position.x) + (mine_pos.z - transform.position.z) * (mine_pos.z - transform.position.z) < (.3f * LevelLoader.ratio) * (.4f * LevelLoader.ratio))
                {
                    mines[i].GetComponent<MineAI>().boom();
                    Destroy(gameObject);
                }
            }
            List<GameObject> bullets = tanks[i].GetComponent<TankAI>().active_bullets;
            for (int j = 0; j < bullets.Count; j++)
            {
                if (bullets[j] == gameObject) continue;
                //Debug.Log("Checking bullet: " + j);
                Vector3 mine_pos = bullets[j].transform.position;
                if ((mine_pos.x - transform.position.x) * (mine_pos.x - transform.position.x) + (mine_pos.z - transform.position.z) * (mine_pos.z - transform.position.z) < (.3f * LevelLoader.ratio) * (.3f * LevelLoader.ratio))
                {
                    Destroy(bullets[j]);
                    Destroy(gameObject);
                }
            }
            //Debug.Log("Done checking tank");
        }

        float angle = -transform.rotation.eulerAngles.y + 90;
        (Vector3 new_pos, bool flip_x, bool flip_z) = owner.GetComponent<TankAI>().level.CheckCollision(transform.position, new Vector3(transform.position.x + Mathf.Cos(angle * DEG_TO_RAD) * speed * Time.deltaTime, 1.8f, transform.position.z + Mathf.Sin(angle * DEG_TO_RAD) * speed * Time.deltaTime), 0.1f, false);
        transform.position = new_pos;
        float new_angle = angle;
        if (flip_x)
        {
            can_hit_owner = true;
            bounces_remaining--;
            new_angle = 180 - new_angle;
        }
        if (flip_z)
        {
            can_hit_owner = true;
            bounces_remaining--;
            new_angle = -new_angle;
        }
        if (bounces_remaining < 0)
        {
            Destroy(gameObject);
        }
        transform.rotation = Quaternion.AngleAxis(-(new_angle - angle), Vector3.up) * transform.rotation;
    }
}
