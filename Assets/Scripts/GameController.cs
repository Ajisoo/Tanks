using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject playerTank;
    public GameObject brownTank;
    public GameObject full_block;
    public GameObject half_block;
    public GameObject full_boom;
    public GameObject half_boom;

    public static readonly float speed = 12;
    public static readonly float theta_speed = 40;
    public static readonly float rotation_speed = 100;
    public static readonly float width = 63;
    public static readonly float height = 42;
    public static readonly int block_width = 22;
    public static readonly int block_height = 17;
    public static readonly float block_size = 2 * width / block_width;

    List<Tank> tanks;

    int[,] level;
    List<GameObject> level_blocks;

    List<Bullet> bullets;

    public class Bullet
    {
        public GameObject main_bullet;
        public Tank owner;
        public int bounces_left;
        Quaternion direction;

        public void update()
        {
            
        }

    }

    public class Tank
    {
        public GameObject main_tank;
        public float theta;
        public float phi;
        public int bullets_out;
        public int mines_out;
        public bool dead;

        public float theta_int;
        public float phi_int;

        public void updateTheta()
        {
            if (theta_int < 0)
            {
                theta = theta_int + 360;
            }
            else
            {
                if (theta_int >= 180 && theta < 180 && theta_int - theta >= 180)
                {
                    if (theta - theta_int + 360 < theta_speed * Time.deltaTime)
                    {
                        theta = theta_int;
                    }
                    else
                    {
                        theta -= theta_speed * Time.deltaTime;
                        if (theta < 0) theta += 360;
                    }
                }
                else if (theta_int < 180 && theta >= 180 && theta - theta_int >= 180)
                {
                    if (theta_int - theta + 360 < theta_speed * Time.deltaTime)
                    {
                        theta = theta_int;
                    }
                    else
                    {
                        theta += theta_speed * Time.deltaTime;
                        if (theta >= 360) theta -= 360;
                    }
                }
                else
                {
                    if (Mathf.Abs(theta - theta_int) < theta_speed * Time.deltaTime)
                    {
                        theta = theta_int;
                    }
                    else
                    {
                        if (theta_int > theta)
                        {
                            theta += theta_speed * Time.deltaTime;
                        }
                        else
                        {
                            theta -= theta_speed * Time.deltaTime;
                        }
                    }
                }
            }
            main_tank.transform.GetChild(1).rotation = Quaternion.AngleAxis(-theta - 90, new Vector3(0, 1, 0)) * Quaternion.AngleAxis(90, new Vector3(0, 0, -1));
        }

        public void updatePhi()
        {
            if (float.IsNaN(phi_int)) return;

            float original_goal = phi_int;
            if (phi_int >= 270) phi_int -= 360;
            if (phi_int >= 90) phi_int -= 180;
            if (Mathf.Abs(phi_int - phi) < 0.0005f)// || Mathf.Abs(inv_goal_phi - phi) < 0.0005f)
            {
                if (original_goal < 90 || original_goal >= 270)
                {
                    main_tank.transform.position = new Vector3(main_tank.transform.position.x + Mathf.Cos(phi / 180 * Mathf.PI) * Time.deltaTime * speed, 0, main_tank.transform.position.z + Mathf.Sin(phi / 180 * Mathf.PI) * Time.deltaTime * speed);
                }
                else
                {
                    main_tank.transform.position = new Vector3(main_tank.transform.position.x + Mathf.Cos((phi + 180) / 180 * Mathf.PI) * Time.deltaTime * speed, 0, main_tank.transform.position.z + Mathf.Sin((phi + 180) / 180 * Mathf.PI) * Time.deltaTime * speed);
                }
            }
            else if (Mathf.Abs(phi_int - phi) < rotation_speed * Time.deltaTime || Mathf.Abs(phi_int - phi + 180) < rotation_speed * Time.deltaTime || Mathf.Abs(phi_int - phi - 180) < rotation_speed * Time.deltaTime)
            {
                phi = phi_int;
            }
            else
            {
                if (phi >= 0)
                {
                    if (phi_int < 0)
                    {
                        if (phi - phi_int >= phi_int - phi + 180)
                        {
                            phi += rotation_speed * Time.deltaTime;
                            if (phi >= 90) phi -= 180;
                        }
                        else
                        {
                            phi -= rotation_speed * Time.deltaTime;
                        }
                    }
                    else
                    {
                        if (phi >= phi_int)
                        {
                            phi -= rotation_speed * Time.deltaTime;
                        }
                        else
                        {
                            phi += rotation_speed * Time.deltaTime;
                            if (phi >= 90) phi -= 180;
                        }
                    }
                }
                else
                {
                    if (phi_int >= 0)
                    {
                        if (phi_int - phi >= phi - phi_int + 180)
                        {
                            phi -= rotation_speed * Time.deltaTime;
                            if (phi < -90) phi += 180;
                        }
                        else
                        {
                            phi += rotation_speed * Time.deltaTime;
                        }
                    }
                    else
                    {
                        if (phi >= phi_int)
                        {
                            phi -= rotation_speed * Time.deltaTime;
                            if (phi < -90) phi += 180;
                        }
                        else
                        {
                            phi += rotation_speed * Time.deltaTime;
                        }
                    }
                }
            }
            main_tank.transform.GetChild(0).rotation = Quaternion.AngleAxis(-phi, new Vector3(0, 1, 0));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        tanks = new List<Tank>();
        bullets = new List<Bullet>();
        level = new int[22, 17];
        level_blocks = new List<GameObject>();
        loadLevel(1);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < tanks.Count; i++)
        {
            tanks[i].updateTheta();
            tanks[i].updatePhi();
        }
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].update();
        }
    }

    int FindBlockX(GameObject obj)
    {
        return (int)((obj.transform.position.x - (-width + block_size / 2)) / block_size);
    }

    int FindBlockY(GameObject obj)
    {
        return (int)((obj.transform.position.z - (-height + block_size / 2)) / block_size);
    }

    void loadLevel(int levelNum)
    {

        ResetLists();

        if (levelNum == 1)
        {
            for (int i = 0; i < block_width; i++)
            {
                Create(i, 4, 2);
            }

            Create(5, 8, 2);
            Create(5, 9, 2);
            Create(5, 12, 2);
            Create(5, 13, 2);

            Create(11, 8, 2);
            Create(11, 9, 2);
            Create(11, 12, 2);
            Create(11, 13, 2);

            Create(11, 10, -2);
            Create(11, 11, -2);

            SetUpTank(playerTank, 2, 10, 0, 0);
            SetUpTank(brownTank, block_width - 3, 10, 180, 0);
        }
        else if (levelNum == 2)
        {

        }
    }

    void ResetLists()
    {
        for (int i = 0; i < tanks.Count; i++)
        {
            Destroy(tanks[i].main_tank);
        }
        tanks.Clear();

        for (int i = 0; i < bullets.Count; i++)
        {
            Destroy(bullets[i].main_bullet);
        }
        bullets.Clear();

        for (int i = 0; i < level_blocks.Count; i++)
        {
            Destroy(level_blocks[i]);
        }
        level_blocks.Clear();

        level = new int[22, 17];
    }

    void SetUpTank(GameObject tank, int x, int y, float theta, float phi)
    {
        tanks.Add(new Tank());
        float startX = -width + block_size / 2;
        float startY = -height + block_size / 2;
        tanks[tanks.Count - 1].main_tank = Instantiate(tank, new Vector3(startX + block_size * x, 0, startY + block_size * y), Quaternion.identity);
        tanks[tanks.Count - 1].theta = theta;
        tanks[tanks.Count - 1].phi = phi;
        tanks[tanks.Count - 1].dead = false;
    }

    void Create(int x, int y, int tallness)
    {
        float startX = -width + block_size / 2;
        float startY = -height + block_size / 2;

        GameObject full = tallness > 0 ? full_block : full_boom;
        GameObject half = tallness > 0 ? half_block : half_boom;

        level[x, y] = tallness;

        tallness = Mathf.Abs(tallness);

        if (tallness == 2)
        {
            level_blocks.Add(Instantiate(full, new Vector3(startX + block_size * x, 0, startY + block_size * y), Quaternion.identity));
        }
        if (tallness == 3)
        {
            level_blocks.Add(Instantiate(half, new Vector3(startX + block_size * x, 0, startY + block_size * y), Quaternion.identity));
            level_blocks.Add(Instantiate(full, new Vector3(startX + block_size * x, block_size / 4, startY + block_size * y), Quaternion.identity));
        }
    }

    // theta in [0, 360). If theta >= 360, snap instantly to theta
    // phi in [-90, 270).
    public void SendIntent(GameObject tank, float theta, float phi)
    {
        for (int i = 0; i < tanks.Count; i++)
        {
            if (tanks[i].main_tank == tank)
            {
                tanks[i].theta_int = theta;
                tanks[i].phi_int = phi;
            }
        }
    }

    public float GetTheta(GameObject tank)
    {
        for (int i = 0; i < tanks.Count; i++)
        {
            if (tanks[i].main_tank == tank)
            {
                return tanks[i].theta;
            }
        }
        return float.NaN;
    }
}
