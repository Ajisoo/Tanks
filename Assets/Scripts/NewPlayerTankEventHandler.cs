using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerTankEventHandler : MonoBehaviour
{

    public GameObject main_tank;

    public delegate void OnNewPlayerTank(GameObject tank);
    public static event OnNewPlayerTank NewPlayerTank;

    public void Start()
    {
        if (main_tank != gameObject) NewPlayerTank(gameObject);
    }

}
