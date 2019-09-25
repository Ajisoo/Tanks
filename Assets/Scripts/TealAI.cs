using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TealAI : TankAI
{
    private readonly int max_bullets = 1;

    private Quaternion goal_rot;

    private bool direction;
    private float time_since_direction_change;
    private float time_since_last_shot;


    // Start is called before the first frame update
    void Start()
    {
        goal_rot = transform.GetChild(0).rotation;
        live_bullets = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time_since_last_shot -= Time.deltaTime;
        time_since_direction_change -= Time.deltaTime;
        Quaternion rot = transform.GetChild(0).rotation;
        Quaternion to_player = Quaternion.AngleAxis(-Mathf.Atan2(playerTank.transform.position.z - transform.position.z, playerTank.transform.position.x - transform.position.x) * PlayerAI.RAD_TO_DEG - 90, new Vector3(0, 1, 0)) * Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));
        float angle = Quaternion.Angle(rot, goal_rot);

        if (angle < 0.0005f)
        {
            goal_rot = Quaternion.AngleAxis(Random.Range(-30, 30), new Vector3(0, 1, 0)) * to_player;
            angle = Quaternion.Angle(rot, goal_rot);
            //Debug.Log(Quaternion.ToEulerAngles(to_player) + " " + Quaternion.Angle(rot, to_player));
        }

        Quaternion new_rot = Quaternion.Slerp(rot, goal_rot, 40 * Time.deltaTime / angle);
        if (angle < 40 * Time.deltaTime)
        {
            new_rot = goal_rot;
        }
        transform.GetChild(0).rotation = new_rot;

        //Debug.Log(angle + " " + Quaternion.Angle(rot, to_player) + " " + Quaternion.Angle(goal_rot, to_player));
        //Debug.Log(angle + " " + Quaternion.Angle(rot, to_player) + " " + Quaternion.Angle(goal_rot, to_player));
        if (time_since_last_shot < 0 && live_bullets < max_bullets && level.HasLineOfSight(playerTank.transform.position, transform.position) && Quaternion.Angle(to_player, goal_rot) < angle && Quaternion.Angle(new_rot, goal_rot) < Quaternion.Angle(to_player, goal_rot))
        {
            //Debug.Log("Shoot");
            time_since_last_shot = 0.7f;
            fast_bullet.GetComponent<BulletSpawner>().CreateBullet(gameObject, transform.position, to_player, 0);
        }
        if (time_since_direction_change < 0)
        {
            time_since_direction_change = (float)Random.Range(0, 20) / 7;
            direction = Random.Range(0, 2) == 1;
        }
        angle = transform.GetChild(1).rotation.eulerAngles.y + Time.deltaTime * (direction ? 20 : -20);
        transform.GetChild(1).rotation = Quaternion.AngleAxis(angle, new Vector3(0, 1, 0));
        float h = Mathf.Cos(-angle * Mathf.PI / 180);
        float v = Mathf.Sin(-angle * Mathf.PI / 180);
        float s = Mathf.Sqrt(2) / 3;
        (Vector3 new_pos, bool flip_x, bool flip_z) = level.CheckCollision(transform.position, new Vector3(transform.position.x + s * h * 12 * Time.deltaTime, 0, transform.position.z + s * v * 12 * Time.deltaTime), 0.48f, true);
        if (flip_x || flip_z)
        {
            angle = transform.GetChild(1).rotation.eulerAngles.y + Time.deltaTime * (direction ? 40 : -40);
            transform.GetChild(1).rotation = Quaternion.AngleAxis(angle, new Vector3(0, 1, 0));
        }
        transform.position = new_pos;
    }
}
