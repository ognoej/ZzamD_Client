using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class floatingtext : MonoBehaviour
{



    float m_movespeed =0.33f;
    float m_destroyTime =1.0f;
    float m_alphaspeed = 0.5f;

    TextMeshPro m_text;

    public float m_damage;
    public bool m_iscritical = false;
    Color m_color;

    // Start is called before the first frame update
    void Start()
    {
        m_text = gameObject.GetComponent<TextMeshPro>();

        if (m_text==null)
        {
            print("없음");
        }
        m_text.text = Mathf.Round(m_damage).ToString();
        m_color = m_text.color;
        if(m_iscritical)
        {
            m_text.fontStyle = FontStyles.Bold;
            m_text.fontSize += 2.0f;
            m_color = new Color32(255, 0, 0,255);
            m_text.color = m_color;
        }
        else
        {
            m_color = new Color32(255, 255, 255,255);
            m_text.color = m_color;
        }
        Invoke("DestroyText", m_destroyTime);
    }
    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, m_movespeed * Time.deltaTime, 0));
        m_color.a = Mathf.Lerp(m_color.a, 0, Time.deltaTime * m_alphaspeed);
        m_text.color = m_color;
    }


    private void DestroyText()
    {

        Destroy(gameObject);
    }
}
