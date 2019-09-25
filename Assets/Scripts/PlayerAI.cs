using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : TankAI
{
    private static readonly int max_bullets = 5;
    private static readonly int max_mines = 2;
    public static readonly float RAD_TO_DEG = 180 / Mathf.PI;
    private float time_since_last_bullet;
    private float time_since_last_mine;

    // Update is called once per frame
    void Update()
    {
        time_since_last_mine -= Time.deltaTime;
        time_since_last_bullet -= Time.deltaTime;
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float x = cameraRay.origin.x - cameraRay.origin.y / cameraRay.direction.y * cameraRay.direction.x;
        float y = cameraRay.origin.z - cameraRay.origin.y / cameraRay.direction.y * cameraRay.direction.z - 1;

        x = x - transform.position.x;
        y = y - transform.position.z;

        // Set head rotation.
        transform.GetChild(0).rotation = Quaternion.AngleAxis(-Mathf.Atan2(y, x) * RAD_TO_DEG - 90, new Vector3(0, 1, 0)) * Quaternion.AngleAxis(-90, new Vector3(0,0,1));

        if (live_bullets < max_bullets && Input.GetButtonDown("Fire1") && time_since_last_bullet < 0)
        {
            bullet.GetComponent<BulletSpawner>().CreateBullet(gameObject, transform.position, transform.GetChild(0).rotation, 1);
            time_since_last_bullet = 0.25f;
        }

        if (live_mines < max_mines && Input.GetButtonDown("Fire2") && time_since_last_mine < 0)
        {
            time_since_last_mine = 1;
            mine.GetComponent<MineSpawner>().CreateMine(gameObject, transform.position);
        }

        float h = Input.GetAxis("Horizontal Movement");
        float v = Input.GetAxis("Vertical Movement");


        if (h == 0 && v == 0) return;
        Quaternion rot = transform.GetChild(1).rotation;
        Quaternion goal_rot = Quaternion.AngleAxis(Mathf.Atan2(-v,h) * RAD_TO_DEG, new Vector3(0, 1, 0));
        Quaternion goal_rot2 = Quaternion.AngleAxis(Mathf.Atan2(v, -h) * RAD_TO_DEG, new Vector3(0, 1, 0));
        if (Quaternion.Angle(goal_rot2, rot) < Quaternion.Angle(goal_rot, rot)) goal_rot = goal_rot2;
        float angle = Quaternion.Angle(rot, goal_rot);
        if (angle < 0.0005f)
        {
            float s = 1;
            if (h != 0 && v != 0) s = Mathf.Sqrt(2) / 2;
            (Vector3 new_pos, bool flip_x, bool flip_z) = level.CheckCollision(transform.position, new Vector3(transform.position.x + s * h * 12 * Time.deltaTime, 0, transform.position.z + s * v * 12 * Time.deltaTime), 0.48f, true);
            transform.position = new_pos;
            return;
        }
        if (angle < 200 * Time.deltaTime)
        {
            transform.GetChild(1).rotation = goal_rot;
            return;
        }
        transform.GetChild(1).rotation = Quaternion.Slerp(transform.GetChild(1).rotation, goal_rot, 200 * Time.deltaTime / angle);
    }

}
