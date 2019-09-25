using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownAI : TankAI
{
    private readonly int max_bullets = 1;

    private Quaternion goal_rot;


    // Start is called before the first frame update
    void Start()
    {
        goal_rot = transform.GetChild(0).rotation;
        live_bullets = 0;
    }

    // Update is called once per frame
    void Update()
    {

        Quaternion rot = transform.GetChild(0).rotation;
        Quaternion to_player = Quaternion.AngleAxis(-Mathf.Atan2(playerTank.transform.position.z - transform.position.z, playerTank.transform.position.x - transform.position.x) * PlayerAI.RAD_TO_DEG - 90, new Vector3(0, 1, 0)) * Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));
        float angle = Quaternion.Angle(rot, goal_rot);

        if (angle < 0.0005f)
        {
            goal_rot = Quaternion.AngleAxis(Random.Range(-90, 90), new Vector3(0,1,0)) * to_player;
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
        if ( live_bullets < max_bullets && level.HasLineOfSight(playerTank.transform.position, transform.position) && Quaternion.Angle(to_player, goal_rot) < angle && Quaternion.Angle(new_rot, goal_rot) < Quaternion.Angle(to_player, goal_rot))
        {
            //Debug.Log("Shoot");
            bullet.GetComponent<BulletSpawner>().CreateBullet(gameObject, transform.position, to_player, 1);
        }
    }
}
