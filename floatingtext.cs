using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class floatingtext : MonoBehaviour
{

    public float movespeed =5;
    public float destroyTime =1.5f;

    public Text m_text;

    private Vector3 m_vector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_vector.Set(m_text.transform.position.x, m_text.transform.position.y + (movespeed * Time.deltaTime), m_text.transform.position.z);
        m_text.transform.position = m_vector;
        destroyTime -= Time.deltaTime;

        if(destroyTime <=0)
        {
            Destroy(this.gameObject);
        }
    }
}
