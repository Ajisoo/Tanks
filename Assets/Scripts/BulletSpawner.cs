using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateBullet(GameObject owner, Vector3 pos, Quaternion rot, int bounces_available)
    {
        GameObject new_bullet = Instantiate(gameObject, pos, rot * Quaternion.AngleAxis(180, Vector3.forward) * Quaternion.AngleAxis(180, Vector3.up));
        new_bullet.GetComponent<BulletSpawner>().enabled = false;
        new_bullet.GetComponent<Cloneable>().enabled = true;
        new_bullet.GetComponent<BulletAI>().enabled = true;
        new_bullet.GetComponent<BulletAI>().owner = owner;
        new_bullet.GetComponent<BulletAI>().bounces_remaining = bounces_available;
        owner.GetComponent<TankAI>().live_bullets += 1;
        owner.GetComponent<TankAI>().active_bullets.Add(new_bullet);
    }
}
