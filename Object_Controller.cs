using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Controller : MonoBehaviour
{
    private Animator m_animator;

    // 상태
    public string state = "idle";

    // 공격시간 카운터
    float m_attacktime = 0;

    // 적 아군 구분 boolean
    public bool isPTeam = true;

    // 타겟과의 거리
    float distancefromtarget = 0;

    SpriteRenderer m_renderer;

    float m_deathTime = 2.0f;

    // 타겟 오브젝트
    GameObject target;

    // 이동속도
    [SerializeField] public float m_speed = 0.5f;

    // 공격속도
    [SerializeField] public float m_attackspeed = 3.0f;

    // 공격 범위
    [SerializeField] public float m_attackrange = 0.7f;

    // 피통
    [SerializeField] public float m_hp = 10;

    // 데미지
    [SerializeField] public float m_damage = 5;
    
        // Start is called before the first frame update
        void Start()
    {
        m_animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            // 기본 애니메이션
            case "idle":
                {
                    if (isPTeam)
                    {
                        m_animator.SetInteger("AnimState", 0);

                        if (mops.Enermy_list.Count != 0)
                        {
                            StateFollow(search_nearobj(this.transform.position));

                        }
                        break;
                    }
                    else
                    {
                        m_animator.SetInteger("AnimState", 0);
                        if (mops.Player_list.Count != 0)
                        {
                            print("캐릭터가있다");
                            StateFollow(search_nearobj(this.transform.position));

                        }
                        break;
                    }
                }

            // 추적 애니메이션
            case "follow":
                {
                    if(!targetserch())
                    {
                        target = null;
                        state = "idle";
                        break;
                    }

                    if(targetinRange())
                    {
                        state = "attack";
                        break;
                    }

                    if (isPTeam)
                    {
                        m_animator.SetInteger("AnimState", 1);
                    }
                    else
                    {
                        m_animator.SetInteger("AnimState", 2);
                    }
                    chaseEnermy();
                    break;
                }

            // 공격 애니메이션
            case "attack":
                {
                    // 어택 idle 애니
                    m_animator.SetInteger("AnimState", 0);

                    // 어택 시간에 따라 공격애니메이션 키고 어택 함수
                    m_attacktime -= Time.deltaTime;
                    if (m_attacktime < 0)
                    {
                        m_animator.SetTrigger("Attack");
                        StartCoroutine(attacktarget());
                        m_attacktime = m_attackspeed;
                    }
                    if(!targetserch())
                    {
                        state = "idle";
                        break;
                    }
                    break;
                }

            // 죽음 애니메이션
            case "death":
                {

                    StartCoroutine(DeathandDestroy());
                    break;
                }
        }
    }
    
    void chaseEnermy()
    {
        Vector3 dir = target.transform.position - transform.position;
        gameObject.transform.localPosition += (dir * m_speed * Time.deltaTime);
    }
    bool targetserch()
    {
        if (isPTeam)
        {
            if (mops.Enermy_list.Count != 0)
            {
                target = search_nearobj(this.transform.position);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (mops.Player_list.Count != 0)
            {
                target = search_nearobj(this.transform.position);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    bool targetinRange()
    {
        if (distancefromtarget < m_attackrange)
            return true;

        else
            return false;
    }



    void StateFollow(GameObject _target)
    {
        target = _target;
        state = "follow";
    }
    
    GameObject search_farestobj(Vector3 my)
    {
        if (isPTeam)
        {
            GameObject enermy = mops.Enermy_list[0];
            distancefromtarget = Vector3.Distance(my, enermy.transform.position);
            foreach (GameObject i in mops.Enermy_list)
            {
                float distance = Vector3.Distance(my, i.transform.position);
                if (distance < distancefromtarget)
                {
                    enermy = i;
                    distancefromtarget = distance;
                }
            }
            return enermy;
        }
        else
        {
            GameObject enermy = mops.Player_list[0];
            distancefromtarget = Vector3.Distance(my, enermy.transform.position);
            foreach (GameObject i in mops.Player_list)
            {
                float distance = Vector3.Distance(my, i.transform.position);
                if (distance < distancefromtarget)
                {
                    enermy = i;
                    distancefromtarget = distance;
                }
            }
            return enermy;
        }
    }
    GameObject search_nearobj(Vector3 my)
    {
        if (isPTeam)
        {
            GameObject enermy = mops.Enermy_list[0];
            distancefromtarget = Vector3.Distance(my, enermy.transform.position);
            foreach (GameObject i in mops.Enermy_list)
            {
                float distance = Vector3.Distance(my, i.transform.position);
                if (distance > distancefromtarget)
                {
                    enermy = i;
                    distancefromtarget = distance;
                }
            }
            return enermy;
        }
        else
        {
            GameObject enermy = mops.Player_list[0];
            distancefromtarget = Vector3.Distance(my, enermy.transform.position);
            foreach (GameObject i in mops.Player_list)
            {
                float distance = Vector3.Distance(my, i.transform.position);
                if (distance > distancefromtarget)
                {
                    enermy = i;
                    distancefromtarget = distance;
                }
            }
            return enermy;
        }
    }

    IEnumerator attacktarget()
    {
        var temptarget = target.GetComponent<Object_Controller>();
        print(temptarget.m_hp);
        temptarget.m_hp -= m_damage;
        if(temptarget.m_hp < 0)
        {
            temptarget.state = "death";
            yield return null;
        }

        var renderer = target.GetComponent<SpriteRenderer>();
        yield return new WaitForSeconds(m_attackspeed*0.15f);
        renderer.color = new Color32(255, 60, 60, 255);
        yield return new WaitForSeconds(0.2f);
        renderer.color = new Color32(255, 255, 255, 255);
        yield return null;

    }

    IEnumerator DeathandDestroy()
    {
        m_animator.SetTrigger("Death");

        yield return new WaitForSeconds(m_deathTime);

        Destroy(gameObject);
        mops.EleminateObject(gameObject, isPTeam);

        yield return null;
    }

    //void attacktarget()
    //{
    //    target.GetComponent<Object_Controller>().m_hp -= m_damage;
    //    m_renderer = target.GetComponent<SpriteRenderer>();
    //    print("타겟색변경");
    //    m_renderer.color = new Color(255,60,60,255);
    //
    //    Invoke("colorchangetooriginal", 0.5f);
    //}
    //void colorchangetooriginal()
    //{
    //    m_renderer.color = new Color(255, 255, 255, 255);
    //}

}
