using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BulletScript : MonoBehaviour
{

    public GameObject target;
    public GameObject shooter;
    public float m_movespeed = 4f;
    public float m_boomRange = 0.7f;
    public bool m_isSkill = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (target.transform.position + new Vector3(0,0.5f,0)) - transform.position;
        dir.Normalize();
        gameObject.transform.localPosition += (dir * m_movespeed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < m_boomRange)
        {
            // 타겟과 슈터가 살아있을 때 데미지 계산 (수정 필요 : 슈터가 죽어도 타겟만 살아있으면 데미지계산으로)
            if (target != null && shooter != null)
            {
                if (m_isSkill == false)
                {
                    shooter.GetComponent<Object_Controller>().Damagecalc(target);
                }
                else
                {
                    shooter.GetComponent<Object_Controller>().SkillDamagecalc(target);
                }
            }
            Destroy(gameObject);
        }
    }
}
