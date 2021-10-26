using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mops : MonoBehaviour
{
   public static mops Instance;

   public static List<GameObject> Player_list;
   public static List<GameObject> Enermy_list;

    private void Awake()
    {
        Instance = this;
        Player_list = new List<GameObject>();
        Enermy_list = new List<GameObject>();
        print("생성");
    }
}
