using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class spbarScript : MonoBehaviour
{
    Transform p_transform;
    Slider m_Spbar;
    Object_Controller p_obj;


    // Start is called before the first frame update
    void Start()
    {
        p_transform = GetComponentInParent<Transform>();
        p_obj = GetComponentInParent<Object_Controller>();
        m_Spbar = GetComponentInChildren<Slider>();

        if (m_Spbar == null)
        {
            print("hp바 불러오기실패");
        }

    }



    // Update is called once per frame
    void Update()
    {
        this.transform.position = p_transform.position;
        m_Spbar.value = p_obj.m_sp / p_obj.m_maxsp;
    }

}
