using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mops : MonoBehaviour
{
   public static GameObject floatingtext;

   public static mops Instance;

   public static List<GameObject> Player_list;
   public static List<GameObject> Enermy_list;

    private void Awake()
    {
        Instance = this;
        Player_list = new List<GameObject>();
        Enermy_list = new List<GameObject>();
        floatingtext = Resources.Load<GameObject>("Prefabs/floatingtext");
        if(floatingtext ==null)
        {
            Debug.Log("플로팅텍스트 불러오기 실패");
        }
    }
    public static void EleminateObject(GameObject _object,bool _isPteam)
    {
        if(_isPteam)
        {
            Player_list.Remove(_object);
            return;
        }
        else
        {
            Enermy_list.Remove(_object);
            return;
        }


    }
}
