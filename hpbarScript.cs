using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class hpbarScript : MonoBehaviour
{
    Transform p_transform;
    Slider m_Hpbar;
    Object_Controller p_obj;
    float m_unithp = 20f;
    float m_max_hp;

    public GameObject linebar;


    // Start is called before the first frame update
    void Start()
    {
        p_transform = GetComponentInParent<Transform>();
        p_obj = GetComponentInParent<Object_Controller>();
        m_Hpbar = GetComponentInChildren<Slider>();

        m_max_hp = p_obj.m_maxhp;
        if(m_Hpbar == null)
        {
            print("hp바 불러오기실패");
        }

        sethpbarline();
    }
    


    // Update is called once per frame
    void Update()
    {
        this.transform.position = p_transform.position;
        m_Hpbar.value = p_obj.m_hp / p_obj.m_maxhp ;
    }

    public void sethpbarline()
    {
        float scalex = (100f / m_unithp) / (m_max_hp / m_unithp);

        print(scalex);

        foreach(Transform i in linebar.transform)
        {
            i.gameObject.transform.localScale = new Vector3(scalex, 1, 1);
        }

    }



}
