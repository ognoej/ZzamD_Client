using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneSingleton : MonoBehaviour
{
    public static GameSceneSingleton Instance;
    // 전체 리소스 싱글톤
    public static Dictionary<string, GameObject> SingletonObj_list;

    public static List<GameObject> Player_list;
    public static List<GameObject> Enermy_list;
    public static GameObject floatingtext;

    public static Dictionary<string, GameObject> SingletonEffect_list;
    public static Dictionary<string, GameObject> SingletonBullet_list;



    private void Awake()
    {
        Instance = this;
        Player_list = new List<GameObject>();
        Enermy_list = new List<GameObject>();
        floatingtext = Resources.Load<GameObject>("floatingtext");

        SingletonObj_list = new Dictionary<string, GameObject>();
        SingletonEffect_list = new Dictionary<string, GameObject>();
        SingletonBullet_list = new Dictionary<string, GameObject>();


        var temp = Resources.LoadAll<GameObject>("Prefabs");
        foreach (var i in temp)
        {
            SingletonObj_list.Add(i.name, i);
        }
        if (SingletonObj_list.Count!=0)
        {
            print("오브젝트 불러오기 완료");
        }


        var tempeffect= Resources.LoadAll<GameObject>("Effects");
        foreach (var i in tempeffect)
        {
            SingletonEffect_list.Add(i.name, i);
        }
        if (SingletonEffect_list.Count != 0)
        {
            print("이펙트 불러오기 완료");
        }

        var tempbullet = Resources.LoadAll<GameObject>("Bullets");
        foreach (var i in tempbullet)
        {
            SingletonBullet_list.Add(i.name, i);
        }
        if (SingletonBullet_list.Count != 0)
        {
            print("뷸렛 불러오기 완료");
        }


    }


    public static void EleminateObject(GameObject _object,bool _isPteam)
    {

        // 크리티컬섹션 만들어줘야함
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
