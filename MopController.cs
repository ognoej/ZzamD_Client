using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;


public class MopController : MonoBehaviour
{
    [SerializeField]
    GameObject player_prefab;

    [SerializeField]
    GameObject mob_prefab;


    int level_timer = 0;
    int leve_map = 0;


    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
     
        

        

    }

    public void Push_MakeobjBtn(string charctername)
    {
        var newobj = Instantiate(player_prefab) as GameObject;
        var tempcompo = newobj.GetComponent<Object_Controller>();
        tempcompo.state = "idle";
        tempcompo.isPTeam = true;
        tempcompo.m_attackspeed = 2.5f;
        newobj.transform.position = new Vector3(-3, 0, 0);
        
        mops.Player_list.Add(newobj);
        print("아군추가");

    }

    public void Generate_EnermyBtn()
    {
        var newobj = Instantiate(mob_prefab) as GameObject;
        var tempcompo = newobj.GetComponent<Object_Controller>();
        tempcompo.state = "idle";
        tempcompo.isPTeam = false;
        tempcompo.m_attackspeed = 3f;
        newobj.transform.position = new Vector3(3, 0, 0);
        mops.Enermy_list.Add(newobj);
        print("적추가");
    }
}
