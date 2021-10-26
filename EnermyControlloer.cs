using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyControlloer : MonoBehaviour
{

    private Animator m_animator;
    public string state = "idle";
    public float m_speed = 0.5f;

    GameObject target;



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
                    if (mops.Player_list.Count != 0)
                    {
                        print("캐릭터가있다");
                        StateFollow(search_farestobj(this.transform.position));
                        m_animator.SetInteger("AnimState", 0);

                    }
                    break;
                }
            case "follow":
                {
                    targetserch();
                    m_animator.SetInteger("AnimState", 2);
                    chaseEnermy();
                    break;
                }
            case "attack":
                {
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
        print(dir);
        gameObject.transform.localPosition+= (dir * m_speed * Time.deltaTime);
        
    }
    void targetserch()
    {

    }


    void StateFollow(GameObject _target)
    {
        target = _target;
        state = "follow";
    }

    GameObject search_farestobj(Vector3 my)
    {
        GameObject enermy = mops.Player_list[0];
        float shortdis = Vector3.Distance(my, enermy.transform.position);
        foreach (GameObject i in mops.Player_list)
        {
            float distance = Vector3.Distance(my, i.transform.position);
            if (distance < shortdis)
            {
                enermy = i;
                shortdis = distance;
            }
        }
        return enermy;
    }
}
