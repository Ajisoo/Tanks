using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{

    public int[,] level_data;
    private List<GameObject>[,] level_objects;
    public List<GameObject> tanks;

    public GameObject full_block;
    public GameObject half_block;
    public GameObject full_cork;
    public GameObject half_cork;

    public GameObject playerTank;
    public GameObject brownTank;
    public GameObject grayTank;
    public GameObject tealTank;
    public GameObject yellowTank;

    private GameObject active_playerTank;

    public delegate void DestroyCopies();
    public static DestroyCopies reset;

    public static readonly float width = 22;
    public static readonly float height = 17;
    public static readonly float ratio = 2 * 63 / width;
    public static readonly float start_x = -width/2 * ratio + ratio/2;
    public static readonly float start_y = -height/2 * ratio + ratio / 2;

    private int current_level;
    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        level_data = new int[(int)width, (int)height];
        level_objects = new List<GameObject>[(int)width, (int)height];
        current_level = 8;
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (!tanks.Contains(active_playerTank))
            {
                // lose
                active = false;
                current_level--;
            }
            else if (tanks.Count == 1)
            {
                // win
                active = false;
            }
        }
        else
        {
            current_level++;
            LoadLevel(current_level);
        }
    }

    void LoadLevel(int level_num)
    {
        tanks = new List<GameObject>();
        level_data = new int[(int)width, (int)height];
        level_objects = new List<GameObject>[(int)width, (int)height];
        if (reset != null) reset();
        active = true;
        if (level_num == 1)
        {
            for (int i = 0; i < width; i++)
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

            Create(playerTank, 2, 10, 0, 0);
            Create(brownTank, (int)width - 3, 10, 180, 0);
        }
        if (level_num == 2)
        {

            Create(5, 5, -2);
            Create(6, 5, -2);
            Create(7, 5, -2);
            Create(8, 5, -3);
            Create(9, 5, 4);
            Create(10, 5, 5);
            Create(11, 5, 5);
            Create(12, 5, 4);
            Create(13, 5, 3);
            Create(14, 5, 2);
            Create(15, 5, 2);
            Create(16, 5, 2);

            Create(5, 11, 2);
            Create(6, 11, 2);
            Create(7, 11, 2);
            Create(8, 11, 3);
            Create(9, 11, 4);
            Create(10, 11, 5);
            Create(11, 11, 5);
            Create(12, 11, 4);
            Create(13, 11, -3);
            Create(14, 11, -2);
            Create(15, 11, -2);
            Create(16, 11, -2);

            Create(playerTank, 3, 3, 0, 0);
            Create(grayTank, (int)width - 4, (int)height - 4, 180, 0);
        }
        if (level_num == 3)
        {

            Create(10, 3, 3);
            Create(11, 3, -3);
            Create(12, 3, -2);
            Create(13, 3, -3);
            Create(14, 3, -2);
            Create(15, 3, -3);
            Create(16, 3, -2);
            Create(17, 3, 3);
            Create(18, 3, 2);

            Create(3, 13, 3);
            Create(4, 13, -3);
            Create(5, 13, -2);
            Create(6, 13, -3);
            Create(7, 13, -2);
            Create(8, 13, -3);
            Create(9, 13, -2);
            Create(10, 13, 3);
            Create(11, 13, 2);

            Create(10, 3, 3);
            Create(10, 4, 3);
            Create(10, 5, 3);
            Create(10, 6, 3);
            Create(10, 7, 3);
            Create(10, 8, 3);
            Create(11, 8, 3);
            Create(11, 9, 3);
            Create(11, 10, 3);
            Create(11, 11, 3);
            Create(11, 12, 3);
            Create(11, 13, 3);

            Create(playerTank, 2, 8, 0, 0);
            Create(grayTank, 18, 1, 90, -90);
            Create(grayTank, 5, 15, -90, -90);
            Create(brownTank, 19, 8, 180, 0);
        }
        if (level_num == 4)
        {
            for (int i = 0; i < 13; i++)
            {
                Create(i, 5, 1);
            }

            for (int i = 0; i < 13; i++)
            {
                Create((int)width - 1 - i, (int)height - 1 - 5, 1);
            }

            for (int i = 0; i < 10; i++)
            {
                Create((int)width - 1 - 7, i, 1);
            }
            for (int i = 0; i < 10; i++)
            {
                Create(7, (int)height - 1 - i, 1);
            }

            for (int i = 16; i < (int)width; i++)
            {
                Create(i, 5, 1);
            }

            for (int i = 16; i < (int)width; i++)
            {
                Create((int)width - 1 - i, (int)height - 1 - 5, 1);
            }

            for (int i = 13; i < (int)height; i++)
            {
                Create((int)width - 1 - 7, i, 1);
            }
            for (int i = 13; i < (int)height; i++)
            {
                Create(7, (int)height - 1 - i, 1);
            }

            Create(playerTank, 3, 2, 0, 0);
            Create(grayTank, 10, 8, 0, 0);
            Create(grayTank, 18, 8, 180, 0);
            Create(brownTank, 10, 14, -90, 90);
            Create(brownTank, 18, 14, -90, 90);
        }
        if (level_num == 5)
        {
            Create(3, 4, -7);
            Create(3, 5, 7);
            Create(2, 5, -7);

            Create(18, (int)height - 1 - 4, -7);
            Create(18, (int)height - 1 - 5, 7);
            Create(19, (int)height - 1 - 5, -7);

            Create(playerTank, 2, 3, 0, 0);
            Create(tealTank, 20, 8, 180, 0);
            Create(tealTank, 15, 15, -90, 90);
        }
        if (level_num == 6)
        {
            Create(4, 3, 3);
            Create(5, 3, 4);
            Create(6, 3, 3);
            Create(7, 3, 4);
            Create(8, 3, 3);
            Create(9, 3, 4);
            Create(10, 3, 3);
            Create(11, 3, 4);
            Create(12, 3, -3);
            Create(13, 3, 4);
            Create(14, 3, -3);
            Create(15, 3, 4);
            Create(16, 3, -3);
            Create(17, 3, 4);

            Create(4, 13, 4);
            Create(5, 13, -3);
            Create(6, 13, 4);
            Create(7, 13, -3);
            Create(8, 13, 4);
            Create(9, 13, -3);
            Create(10, 13, 4);
            Create(11, 13, 3);
            Create(12, 13, 4);
            Create(13, 13, 3);
            Create(14, 13, 4);
            Create(15, 13, 3);
            Create(16, 13, 4);
            Create(17, 13, 3);

            Create(10, 8, 1);
            Create(10, 9, 1);
            Create(10, 10, 1);
            Create(10, 11, 1);
            Create(10, 12, 1);
            Create(11, 4, 1);
            Create(11, 5, 1);
            Create(11, 6, 1);
            Create(11, 7, 1);
            Create(11, 8, 1);

            Create(playerTank, 2, 8, 0, 0);
            Create(grayTank, 17, 12, 180, 0);
            Create(tealTank, 19, 8, 180, 0);
            Create(tealTank, 20, 3, 180, 0);
            Create(grayTank, 17, 1, 90, 90);
        }
        if (level_num == 7)
        {
            Create(13, 4, 4);
            Create(14, 4, 3);
            Create(15, 4, 4);
            Create(16, 4, 3);
            Create(17, 4, 4);
            Create(18, 4, -3);
            Create(19, 4, 4);
            Create(20, 4, 3);
            Create(21, 4, 4);

            Create(13, 10, 4);
            Create(14, 10, 3);
            Create(15, 10, 4);
            Create(16, 10, 3);
            Create(17, 10, 4);
            Create(18, 10, -3);
            Create(19, 10, 4);
            Create(20, 10, 3);
            Create(21, 10, 4);

            Create(0, 6, 4);
            Create(1, 6, 3);
            Create(2, 6, 4);
            Create(3, 6, -3);
            Create(4, 6, 4);
            Create(5, 6, 3);
            Create(6, 6, 4);
            Create(7, 6, 3);
            Create(8, 6, 4);

            Create(0, 12, 4);
            Create(1, 12, 3);
            Create(2, 12, 4);
            Create(3, 12, -3);
            Create(4, 12, 4);
            Create(5, 12, 3);
            Create(6, 12, 4);
            Create(7, 12, 3);
            Create(8, 12, 4);

            Create(playerTank, 2, 2, 0, 0);
            Create(tealTank, 2, 9, 180, 0);
            Create(tealTank, 1, 15, 180, 0);
            Create(tealTank, 19, 14, 180, 0);
            Create(tealTank, 19, 1, 90, 90);
        }
        if (level_num == 8)
        {
            Create(3, 3, -3);
            Create(3, 4, -3);
            Create(3, 5, -3);
            Create(4, 5, -3);
            Create(4, 6, -3);
            Create(4, 7, 3);
            Create(4, 8, 3);
            Create(4, 9, 3);
            Create(4, 10, -3);
            Create(4, 11, -3);
            Create(3, 11, -3);
            Create(3, 12, -3);
            Create(3, 13, -3);

            Create(18, 3, -3);
            Create(18, 4, -3);
            Create(18, 5, -3);
            Create(17, 5, -3);
            Create(17, 6, -3);
            Create(17, 7, 3);
            Create(17, 8, 3);
            Create(17, 9, 3);
            Create(17, 10, -3);
            Create(17, 11, -3);
            Create(18, 11, -3);
            Create(18, 12, -3);
            Create(18, 13, -3);

            Create(8, 3, -3);
            Create(9, 3, -3);
            Create(10, 3, 3);
            Create(11, 3, 3);
            Create(12, 3, -3);
            Create(13, 3, -3);
            Create(8, 13, -3);
            Create(9, 13, -3);
            Create(10, 13, 3);
            Create(11, 13, 3);
            Create(12, 13, -3);
            Create(13, 13, -3);



            Create(playerTank, 1, 8, 0, 0);
            Create(yellowTank, 14, 4, 90, 90);
            Create(yellowTank, 15, 12, -90, 90);
            Create(yellowTank, 20, 8, 180, 0);
            Create(tealTank, 21, 14, 180, 0);
            Create(tealTank, 21, 2, 180, 0);
        }
        if (level_num == 9)
        {
            for (int i = 0; i < 7; i++)
            {
                Create(i, 8, i + 2);
                Create((int)width - 1 - i, 8, i + 2);
            }

            Create(10, 3, 5);
            Create(11, 3, 5);
            Create(11, 4, -5);
            Create(11, 5, -5);
            Create(11, 6, -5);
            Create(11, 7, -5);
            Create(11, 8, -5);
            Create(10, 8, -5);
            Create(10, 9, -5);
            Create(10, 10, -5);
            Create(10, 11, -5);
            Create(10, 12, -5);
            Create(10, 13, 5);
            Create(11, 13, 5);



            Create(playerTank, 2, 2, 0, 0);
            Create(yellowTank, 3, 13, 0, 0);
            Create(yellowTank, 19, 2, 180, 0);
            Create(grayTank, 7, 15, -90, 90);
            Create(grayTank, 13, 11, -90, 90);
            Create(grayTank, 14, 1, 90, 90);
            Create(grayTank, 19, 13, 180, 0);
        }
        // TODO finish the different levels.
    }

    void Create(GameObject tank, int x, int y, float theta, float phi) {
        GameObject new_tank = Instantiate(tank, new Vector3(start_x + ratio * x, 0, start_y + ratio * y), Quaternion.identity);
        if (tank == playerTank) active_playerTank = new_tank;
        else new_tank.GetComponent<TankAI>().playerTank = active_playerTank;
        new_tank.transform.GetChild(0).rotation = Quaternion.AngleAxis(-theta - 90, new Vector3(0, 1, 0)) * Quaternion.AngleAxis(90, new Vector3(0, 0, -1));
        new_tank.transform.GetChild(1).rotation = Quaternion.AngleAxis(phi, new Vector3(0, 1, 0));
        new_tank.GetComponent<TankAI>().enabled = true;
        new_tank.GetComponent<Cloneable>().enabled = true;
        tanks.Add(new_tank);
    }

    void Create(int x, int y, int tallness)
    {

        GameObject full = tallness > 0 ? full_block : full_cork;
        GameObject half = tallness > 0 ? half_block : half_cork;

        level_data[x, y] = tallness;

        tallness = Mathf.Abs(tallness);
        float y_buf = -1.4f;

        level_objects[x, y] = new List<GameObject>();

        if (tallness == 1)
        {
            level_objects[x, y].Add(Instantiate(half, new Vector3(start_x + ratio * x, y_buf + ratio / 4, start_y + ratio * y), Quaternion.AngleAxis(90, Vector3.right)));
        }
        if (tallness == 2)
        {
            level_objects[x, y].Add(Instantiate(full, new Vector3(start_x + ratio * x, y_buf + ratio / 2, start_y + ratio * y), Quaternion.identity));
        }
        if (tallness == 3)
        {
            level_objects[x, y].Add(Instantiate(half, new Vector3(start_x + ratio * x, y_buf + ratio/4, start_y + ratio * y), Quaternion.AngleAxis(90, Vector3.right)));
            level_objects[x, y].Add(Instantiate(full, new Vector3(start_x + ratio * x, y_buf + 4 * ratio/4, start_y + ratio * y), Quaternion.identity));
        }
        if (tallness == 4)
        {
            level_objects[x, y].Add(Instantiate(half, new Vector3(start_x + ratio * x, y_buf + ratio / 4, start_y + ratio * y), Quaternion.AngleAxis(90, Vector3.right)));
            level_objects[x, y].Add(Instantiate(full, new Vector3(start_x + ratio * x, y_buf + 4 * ratio / 4, start_y + ratio * y), Quaternion.identity));
            level_objects[x, y].Add(Instantiate(half, new Vector3(start_x + ratio * x, y_buf + 7 * ratio / 4, start_y + ratio * y), Quaternion.AngleAxis(90, Vector3.right)));
        }
        if (tallness == 5)
        {
            level_objects[x, y].Add(Instantiate(full, new Vector3(start_x + ratio * x, y_buf + 2 * ratio / 4, start_y + ratio * y), Quaternion.identity));
            level_objects[x, y].Add(Instantiate(half, new Vector3(start_x + ratio * x, y_buf + 5 * ratio / 4, start_y + ratio * y), Quaternion.AngleAxis(90, Vector3.right)));
            level_objects[x, y].Add(Instantiate(full, new Vector3(start_x + ratio * x, y_buf + 8 * ratio / 4, start_y + ratio * y), Quaternion.identity));
        }
        if (tallness == 6)
        {
            level_objects[x, y].Add(Instantiate(half, new Vector3(start_x + ratio * x, y_buf + 1 * ratio / 4, start_y + ratio * y), Quaternion.AngleAxis(90, Vector3.right)));
            level_objects[x, y].Add(Instantiate(full, new Vector3(start_x + ratio * x, y_buf + 4 * ratio / 4, start_y + ratio * y), Quaternion.identity));
            level_objects[x, y].Add(Instantiate(half, new Vector3(start_x + ratio * x, y_buf + 7 * ratio / 4, start_y + ratio * y), Quaternion.AngleAxis(90, Vector3.right)));
            level_objects[x, y].Add(Instantiate(full, new Vector3(start_x + ratio * x, y_buf + 9 * ratio / 4, start_y + ratio * y), Quaternion.identity));
        }
        if (tallness == 7)
        {
            level_objects[x, y].Add(Instantiate(half, new Vector3(start_x + ratio * x, y_buf + 1 * ratio / 4, start_y + ratio * y), Quaternion.AngleAxis(90, Vector3.right)));
            level_objects[x, y].Add(Instantiate(full, new Vector3(start_x + ratio * x, y_buf + 4 * ratio / 4, start_y + ratio * y), Quaternion.identity));
            level_objects[x, y].Add(Instantiate(half, new Vector3(start_x + ratio * x, y_buf + 7 * ratio / 4, start_y + ratio * y), Quaternion.AngleAxis(90, Vector3.right)));
            level_objects[x, y].Add(Instantiate(full, new Vector3(start_x + ratio * x, y_buf + 10 * ratio / 4, start_y + ratio * y), Quaternion.identity));
            level_objects[x, y].Add(Instantiate(half, new Vector3(start_x + ratio * x, y_buf + 13 * ratio / 4, start_y + ratio * y), Quaternion.AngleAxis(90, Vector3.right)));
        }
        // TODO finish different heights

        for (int i = 0; i < level_objects[x, y].Count; i++)
        {
            level_objects[x, y][i].GetComponent<Cloneable>().enabled = true;
        }
    }

    /*
     * Assumptions:
     * 
     */
    public (Vector3, bool, bool) CheckCollision(Vector3 pos1, Vector3 pos2, float size, bool tank)
    {
        pos1 = new Vector3(pos1.x, pos1.y, pos1.z);
        pos2 = new Vector3(pos2.x, pos2.y, pos2.z);
        //Debug.Log("Starting HasLineOfSight with parameters: " + pos1 + " " + pos2);
        pos1.x /= ratio;
        pos2.x /= ratio;
        pos1.z /= ratio;
        pos2.z /= ratio;
        pos1.x += width / 2;
        pos2.x += width / 2;
        pos1.z += height / 2;
        pos2.z += height / 2;

        float final_x = pos2.x;
        float final_z = pos2.z;
        int upper_lim = tank ? 0 : 1;
        bool flip_x = false;
        bool flip_z = false;

        if (pos1.x < pos2.x && (pos2.x + size >= width || ((int)(pos2.x + size) != (int)(pos1.x) &&  Mathf.Abs(GetLevelInfo((int)(pos2.x + size), (int)(pos1.z))) > upper_lim)))
        {
            if (tank) final_x = (int)(pos2.x + size) - size;
            else final_x = (int)(pos2.x + size) - size - ((pos2.x - pos1.x) - ((int)(pos2.x + size) - size - pos1.x));
            flip_x = true;
        }
        if (pos1.x > pos2.x && (pos2.x - size < 0 || ((int)(pos2.x - size) != (int)(pos1.x) &&  Mathf.Abs(GetLevelInfo((int)(pos2.x - size), (int)(pos1.z))) > upper_lim)))
        {
            if (tank) final_x = (int)(pos1.x) + size;
            else final_x = (int)(pos1.x) + size + ((pos1.x - pos2.x) - (pos1.x - ((int)(pos1.x) + size)));
            flip_x = true;
        }

        if (pos1.z < pos2.z && (pos2.z + size >= height || ((int)(pos2.z + size) != (int)(pos1.z) && Mathf.Abs(GetLevelInfo((int)(pos1.x), (int)(pos2.z + size))) > upper_lim)))
        {
            if (tank) final_z = (int)(pos2.z + size) - size;
            else final_z = (int)(pos2.z + size) - size - ((pos2.z - pos1.z) - ((int)(pos2.z + size) - size - pos1.z));
            flip_z = true;
        }
        if (pos1.z > pos2.z && (pos2.z - size < 0 || ((int)(pos2.z - size) != (int)(pos1.z) &&  Mathf.Abs(GetLevelInfo((int)(pos1.x), (int)(pos2.z - size))) > upper_lim)))
        {
            if (tank) final_z = (int)(pos1.z) + size;
            else final_z = (int)(pos1.z) + size + ((pos1.z - pos2.z) - (pos1.z - ((int)(pos1.z) + size)));
            flip_z = true;
        }

        return (new Vector3((final_x -= width / 2) * ratio, pos1.y, (final_z -= height / 2) * ratio), flip_x, flip_z);
    }

    public bool HasLineOfSight(Vector3 pos1, Vector3 pos2)
    {
        pos1 = new Vector3(pos1.x, pos1.y, pos1.z);
        pos2 = new Vector3(pos2.x, pos2.y, pos2.z);
        //Debug.Log("Starting HasLineOfSight with parameters: " + pos1 + " " + pos2);
        pos1.x /= ratio;
        pos2.x /= ratio;
        pos1.z /= ratio;
        pos2.z /= ratio;
        pos1.x += width / 2;
        pos2.x += width / 2;
        pos1.z += height / 2;
        pos2.z += height / 2;
        //Debug.Log("Updated positions: " + pos1 + " " + pos2);
        if (pos1.x == pos2.x)
        {
            //Debug.Log("Same x position, vertical line");
            bool flip = pos1.z < pos2.z;
            for (int i = (int)pos1.z; flip ? i <= (int)pos2.z : i >= (int)pos2.z; i += flip ? 1 : -1)
            {
                int info = GetLevelInfo((int)(pos1.x), i);
                if (info < 0 || info > 1) return false;
            }
        }
        else
        {
            float slope = (pos2.z - pos1.z) / (pos2.x - pos1.x);
            if (pos1.x > pos2.x)
            {
                Vector3 temp = pos1;
                pos1 = pos2;
                pos2 = temp;
            }
            bool flip = (slope > 0);
            for (int i = (int)pos1.z; flip ? i <= (int)(((int)(pos1.x + 1) - pos1.x) * slope + pos1.z) : i >= (int)(((int)(pos1.x + 1) - pos1.x) * slope + pos1.z); i += flip ? 1 : -1)
            {
                //Debug.Log("Limit is " + (int)(((int)(pos1.x + 1) - pos1.x) * slope + pos1.z) + " " + ((int)(pos1.x + 1) - pos1.x) + " " + slope);
                //Debug.Log("Adding (" + (int)pos1.x + ", " + i + ") to check");
                //Destroy(Instantiate(half_block, new Vector3(start_x + ratio * (int)(pos1.x), -1.4f + ratio / 2, start_y + ratio * i), Quaternion.identity), Time.deltaTime);
                int info = GetLevelInfo((int)(pos1.x), i);
                if (info < 0 || info > 1) return false;
            }
            for (int i = (int)(pos1.x) + 1; i < (int)(pos2.x); i++)
            {
                int y = (int)((i + 1 - pos1.x) * slope + pos1.z);
                //Debug.Log("Checking line with (" + i + ", " + (int)((i - pos1.x) * slope + pos1.z) + ") and (" + (i + 1) + ", " + y + ")");
                for (int j = (int)((i - pos1.x) * slope + pos1.z); flip ? j <= y : j >= y; j += flip ? 1 : -1)
                {
                    //Debug.Log("Adding (" + i + ", " + j + ") to check");
                    //Destroy(Instantiate(half_block, new Vector3(start_x + ratio * i, -1.4f + ratio / 2, start_y + ratio * j), Quaternion.identity), Time.deltaTime);
                    int info = GetLevelInfo(i, j);
                    if (info < 0 || info > 1) return false;
                }
            }
            for (int i = (int)(((int)pos2.x - pos1.x) * slope + pos1.z); flip ? i <= (int)(pos2.z) : i >= (int)(pos2.z); i += flip ? 1 : -1)
            {
                //Debug.Log("Adding (" + (int)(pos2.x) + ", " + i + ") to check");
                //Destroy(Instantiate(half_block, new Vector3(start_x + ratio * (int)(pos2.x), -1.4f + ratio / 2, start_y + ratio * i), Quaternion.identity), Time.deltaTime);
                int info = GetLevelInfo((int)(pos2.x), i);
                if (info < 0 || info > 1) return false;
            }
        }
        return true;
    }

    public void BoomLevel(Vector3 pos, float rad)
    {
        Vector2 updated_pos = new Vector2(pos.x - ratio / 2 - start_x, pos.z - ratio / 2 - start_y);
        for (int i = 0; i < (int)(width); i++)
        {
            for (int j = 0; j < (int)(height); j++)
            {
                if (GetLevelInfo(i, j) < 0 && (updated_pos.x - i * ratio) * (updated_pos.x - i * ratio) + (updated_pos.y - j * ratio) * (updated_pos.y - j * ratio) < rad * rad)
                {
                    level_data[i, j] = 0;
                    for (int k = 0; k < level_objects[i, j].Count; k++)
                    {
                        Destroy(level_objects[i, j][k]);
                    }
                }
            }
        }
    }

    int GetLevelInfo(int x, int y)
    {
        return level_data[x, y];
    }
}
