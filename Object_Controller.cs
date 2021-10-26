using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Controller : MonoBehaviour
{
    private Animator m_animator;
    public string state = "idle";

    float m_attacktime = 0;
    float m_attackspeed = 3.0f;

    public bool isPTeam = true;

    GameObject target;

    private float m_attackrange = 0.7f;

    [SerializeField] float m_speed = 0.5f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] float m_rollForce = 6.0f;
    [SerializeField] bool m_noBlood = false;
    [SerializeField] GameObject m_slideDust;

    float distancefromtarget = 0;

    private bool m_grounded = false;
    private bool m_rolling = false;
    private int m_facingDirection = 1;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;

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

            case "idle":
                {
                    if (isPTeam)
                    {
                        m_delayToIdle -= Time.deltaTime;
                        if (m_delayToIdle < 0)
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
                        print("적 추적중");
                        m_animator.SetInteger("AnimState", 2);
                    }
                    chaseEnermy();
                    break;
                }
            case "attack":
                {
                    // 어택 idle 애니
                    m_animator.SetInteger("AnimState", 0);

                    // 어택 시간에 따라 공격애니메이션 키고 어택 함수
                    m_attacktime -= Time.deltaTime;
                    if (m_attacktime < 0)
                    {
                        m_animator.SetTrigger("Attack");
                        m_attacktime = m_attackspeed;
                    }
                    break;
                }
            case "death":
                {
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

}
