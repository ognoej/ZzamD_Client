using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public struct objinfo
{
    public string objname;
    public bool isMyteam;
    public float movespeed;
    public float attackspeed;
    public float attackrange;
    public float hp;
    public float mp;
    public float criticalchance;
    public int dmg;
    public float skilldmg;
    public string attackeffectname;
    public string skilleffectname;
    public bool isbullet;
    public string bulletname;
    public bool isskillsplash;
    public float skillsp;
    public Vector3 skillrange;

}
public class Object_Controller : MonoBehaviour
{
    // 프리팹 저장 애니메이터
    private Animator m_animator;
    // 오브젝트 설정 구조체
    public objinfo m_objinfo;

    #region 오브젝트 info
    // 적 아군 구분 boolean
    public bool m_isMyteam = true;
    // 이름
    private string m_name;
    // 이동속도
    private float m_movespeed = 2.0f;
    // 공격 범위
    private float m_attackrange = 0.7f;
    // 현재피
    public float m_hp = 10;
    // 피통
    public float m_maxhp = 10;
    // 현재 마나
    public float m_sp = 0;
    // 마나통
    public float m_maxsp = 10;
    // 스킬 소모sp
    public float m_skillsp = 10;
    // 데미지
    private int m_damage = 5;
    // 스킬데미지
    public float m_skilldamage = 10f;
    // 크리확률
    private float m_criticalchance = 0;
    // 크리데미지
    private float m_criticaldmg = 1.5f;
    // 일반공격 스플래쉬 여부
    public bool m_isAttackSplash = false;
    // 공격 당 sp 획득 값
    public float gainSpPerhit = 2.0f;
    // skill splash 여부
    public bool m_isSkillsplash = false;
    // skill range
    public Vector3 m_skillRange;
    // 공격 이펙트
    private string m_attackEffectName;
    // 스킬 이펙트
    private string m_skillEffectName;

    // 투사체 여부
    private bool m_isbullet = false;
    // 투사체 이펙트
    private string m_bulletname;
    #endregion

    #region 타겟 관련
    // 타겟 오브젝트 포인터
    GameObject target;
    // 타겟과의 거리
    float distancefromtarget = 0;
    #endregion

    #region State 및 시간관리

    public string state = "idle";

    // 투사체 딜레이
    private float m_bulletDelay = 0.3f;

    // 공격 속도 ( 공격 콜 사이 간격 )
    private float m_attackspeed = 3.0f;

    // 공격 시간 카운터
    float m_attacktime = 0;

    // 스킬 시간 카운터
    public float m_skillAniTime = 3.0f;

    // 죽음 시간 카운터
    float m_deathTime = 2.0f;
    private bool isdeath = false;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // 애니메이터 불러오기
        m_animator = gameObject.GetComponent<Animator>();
        // 프리팹 이름
        m_name = m_objinfo.objname;
        // 적아군구분
        m_isMyteam = m_objinfo.isMyteam;
        // 이동속도(높을수록 빠름)
        m_movespeed = m_objinfo.movespeed;
        // 공격속도(낮을수록 빠름)
        m_attackspeed = m_objinfo.attackspeed;
        // 어택범위(높을수록 멀리)
        m_attackrange = m_objinfo.attackrange;
        // 피통(높을수록 피통)
        m_hp = m_objinfo.hp;
        // 맥스피통
        m_maxhp = m_objinfo.hp;
        // 마나통
        m_maxsp = m_objinfo.mp;
        // 데미지(높을수록 강함)
        m_damage = m_objinfo.dmg;
        // 스킬 데미지
        m_skilldamage = m_objinfo.skilldmg;
        // 크리티컬확률(높을수록 확률)
        m_criticalchance = m_objinfo.criticalchance;
        // 스킬 범위
        m_skillRange = m_objinfo.skillrange;
        // 범위스킬여부
        m_isSkillsplash = m_objinfo.isskillsplash;
        // 투사체 오브젝트여부
        m_isbullet = m_objinfo.isbullet;
        // 투사체 이름
        m_bulletname = m_objinfo.bulletname;
        //스킬 소모값
        m_skillsp = m_objinfo.skillsp;

        // 이펙트이름저장
        if (m_objinfo.attackeffectname!=null)
            m_attackEffectName = m_objinfo.attackeffectname;

        //스킬이펙트 이름저장
        if (m_objinfo.skilleffectname != null)
            m_skillEffectName = m_objinfo.skilleffectname;

    }

    // Update is called once per frame
    void Update()
    {
        #region FSM
        switch (state)
        {
            #region 기본
            // 기본 애니메이션
            case "idle":
                {
                    if (m_isMyteam)
                    {
                        m_animator.SetInteger("Moving", 0);

                        if (GameSceneSingleton.Enermy_list.Count != 0)
                        {
                            StateFollow(search_nearobj(this.transform.position));

                        }
                        break;
                    }
                    else
                    {
                        m_animator.SetInteger("Moving", 0);
                        if (GameSceneSingleton.Player_list.Count != 0)
                        {
                            StateFollow(search_nearobj(this.transform.position));

                        }
                        break;
                    }
                }
            #endregion

            #region 이동
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

                    m_animator.SetInteger("Moving", 1);
                    chaseEnermy();
                    break;
                }
            #endregion

            #region 공격
            // 공격 애니메이션
            case "attack":
                {
                    // 어택 idle 애니
                    m_animator.SetInteger("Moving", 0);

                   
                    // 어택 시간에 따라 공격애니메이션 키고 어택 함수
                    m_attacktime -= Time.deltaTime;
                    if (m_attacktime < 0)
                    {
                        if (m_sp >= m_skillsp)
                        {
                            state = "skill";
                            m_attacktime = m_skillAniTime;
                            break;
                        }

                        m_animator.SetTrigger("Attack");
                        StartCoroutine("attacktarget");
                        m_attacktime = m_attackspeed;
                    }

                    if(!targetserch())
                    {
                        state = "idle";
                        m_attacktime = m_attackspeed;
                        break;
                    }
                    break;
                }
            #endregion

            #region 스킬
            // 스킬 애니메이션
            case "skill":
                {
                   //if (m_attacktime == m_skillAniTime)
                   //{
                        m_sp = 0;
                        m_animator.SetTrigger("Attack");
                        StartCoroutine("castSkill");
                  //  }
                  //  if(m_attacktime < 0)
                  //  {
                        print("공격으로 돌아감");
                        state = "attack";
                        break;
                  //    m_attacktime = m_attackspeed;
                  //  }
                  //  m_attacktime -= Time.deltaTime;
                  //  break;
                }
            #endregion

            #region 죽음
            // 죽음 애니메이션
            case "death":
                {
                    if (!isdeath)
                    {
                        isdeath = true;
                        StartCoroutine(DeathandDestroy());
                    }
                    break;
                }
            #endregion
        }
        #endregion
    }



    #region 이동
    void chaseEnermy()
    {
        Vector3 dir = target.transform.position - transform.position;
        dir.Normalize();
        gameObject.transform.localPosition += (dir * m_movespeed * Time.deltaTime);
    }
    #endregion

    #region 추적
    bool targetserch()
    {
        if (m_isMyteam)
        {
            if (GameSceneSingleton.Enermy_list.Count != 0)
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
            if (GameSceneSingleton.Player_list.Count != 0)
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
    

    // 가장 먼적 추적
    GameObject search_farestobj(Vector3 my)
    {
        if (m_isMyteam)
        {
            GameObject enermy = GameSceneSingleton.Enermy_list[0];
            distancefromtarget = Vector3.Distance(my, enermy.transform.position);
            foreach (GameObject i in GameSceneSingleton.Enermy_list)
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
            GameObject enermy = GameSceneSingleton.Player_list[0];
            distancefromtarget = Vector3.Distance(my, enermy.transform.position);
            foreach (GameObject i in GameSceneSingleton.Player_list)
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

    //가장 가까운적 추적
    GameObject search_nearobj(Vector3 my)
    {
        if (m_isMyteam)
        {
            GameObject enermy = GameSceneSingleton.Enermy_list[0];
            distancefromtarget = Vector3.Distance(my, enermy.transform.position);
            foreach (GameObject i in GameSceneSingleton.Enermy_list)
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
            GameObject enermy = GameSceneSingleton.Player_list[0];
            distancefromtarget = Vector3.Distance(my, enermy.transform.position);
            foreach (GameObject i in GameSceneSingleton.Player_list)
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

    #endregion

    #region 일반공격
    IEnumerator attacktarget()
    {
        // 투사체 발사여부
        if (m_isbullet == true)
        {
            yield return new WaitForSeconds(m_bulletDelay);
            var newbullet = Instantiate(GameSceneSingleton.SingletonBullet_list[m_bulletname]) as GameObject;
            var bulletscript = newbullet.GetComponent<BulletScript>();
            bulletscript.target = target;
            bulletscript.shooter = gameObject;
            bulletscript.m_movespeed = 4f;
            bulletscript.m_boomRange = 0.7f;
            bulletscript.m_isSkill = false;

            newbullet.transform.position = transform.position + new Vector3(0.3f,0.8f,0);
            yield break;
        }

        Damagecalc(target);
    }

    public void Damagecalc(GameObject _target)
    {

        // 데미지 계산
        float dmg;
        bool iscritical;

        //크리티컬 계산
        if (Random.Range(0, 100) < m_criticalchance)
        {
            dmg = Mathf.Round(m_damage * m_criticaldmg);
            iscritical = true;
            print("크리티컬");
        }
        else
        {
            dmg = m_damage;
            iscritical = false;
        }



        // Sp Gain
        if (m_sp < m_maxsp)
        {
            gainsp();
        }




        // 스플래쉬 일경우 타겟으로부터 레인지박스 계산
        if(m_isAttackSplash)
        {
            float x1 = _target.transform.position.x - (m_skillRange.x/2);
            float x2 = _target.transform.position.x + (m_skillRange.x/2);

            float y1 = _target.transform.position.y - (m_skillRange.y/2);
            float y2 = _target.transform.position.y + (m_skillRange.y/2);

            // 아군일경우
            if (m_isMyteam)
            {
                foreach (var i in GameSceneSingleton.Enermy_list)
                {
                    if(x1< i.transform.position.x &&
                       x2> i.transform.position.x &&
                       y1 < i.transform.position.y &&
                       y2 > i.transform.position.y)
                    {
                        i.GetComponent<Object_Controller>().DamageTaken(dmg, iscritical, m_attackEffectName);
                    }
                }
            }

            // 적팀일경우
            else
            {
                foreach (var i in GameSceneSingleton.Player_list)
                {
                    if (x1 < i.transform.position.x &&
                       x2 > i.transform.position.x &&
                       y1 < i.transform.position.y &&
                       y2 > i.transform.position.y)
                    {
                        i.GetComponent<Object_Controller>().DamageTaken(dmg, iscritical, m_attackEffectName);
                    }
                }
            }

            return;
        }//splash



        //스플래쉬 아닐경우
        _target.GetComponent<Object_Controller>().DamageTaken(dmg, iscritical,m_attackEffectName);
        return;
    }
#endregion

    #region 스킬
    IEnumerator castSkill()
    {
        // 투사체 발사여부
        if (m_isbullet == true)
        {
            yield return new WaitForSeconds(m_bulletDelay);
            var newbullet = Instantiate(GameSceneSingleton.SingletonBullet_list[m_bulletname]) as GameObject;
            var bulletscript = newbullet.GetComponent<BulletScript>();
            bulletscript.target = target;
            bulletscript.shooter = gameObject;
            bulletscript.m_movespeed = 4f;
            bulletscript.m_boomRange = 0.7f;
            bulletscript.m_isSkill = true;

            newbullet.transform.position = transform.position + new Vector3(0.3f, 0.8f, 0);
            yield break;
        }

        SkillDamagecalc(target);
    }



    public void SkillDamagecalc(GameObject _target)
    {
        print(gameObject.name + "의 스킬 발동");

        // 데미지 계산
        float dmg;
        bool iscritical;

        //크리티컬 계산
        if (Random.Range(0, 100) < m_criticalchance)
        {
            dmg = Mathf.Round(m_skilldamage * m_criticaldmg);
            iscritical = true;
            print("크리티컬");
        }

        else
        {
            dmg = m_skilldamage;
            iscritical = false;
        }


        // 스플래쉬 일경우 타겟으로부터 레인지박스 계산
        if (m_isSkillsplash)
        {
            float x1 = _target.transform.position.x - (m_skillRange.x / 2);
            float x2 = _target.transform.position.x + (m_skillRange.x / 2);

            float y1 = _target.transform.position.y - (m_skillRange.y / 2);
            float y2 = _target.transform.position.y + (m_skillRange.y / 2);

            // 아군일경우
            if (m_isMyteam)
            {
                foreach (var i in GameSceneSingleton.Enermy_list)
                {
                    if (x1 < i.transform.position.x &&
                       x2 > i.transform.position.x &&
                       y1 < i.transform.position.y &&
                       y2 > i.transform.position.y)
                    {
                        print("몇마리");
                        i.GetComponent<Object_Controller>().DamageTaken(dmg, iscritical, m_skillEffectName);
                    }
                }
            }

            // 적팀일경우
            else
            {
                foreach (var i in GameSceneSingleton.Player_list)
                {
                    if (x1 < i.transform.position.x &&
                       x2 > i.transform.position.x &&
                       y1 < i.transform.position.y &&
                       y2 > i.transform.position.y)
                    {
                        i.GetComponent<Object_Controller>().DamageTaken(dmg, iscritical, m_skillEffectName);
                    }
                }
            }

            return;
        }//splash


        //스플래쉬 아닐경우
        _target.GetComponent<Object_Controller>().DamageTaken(dmg, iscritical, m_skillEffectName);
        return;
    }
    #endregion

    #region 데미지 교환

    public void DamageTaken(float _dmg, bool iscritical, string m_attackEffectName)
    {
        m_hp -= _dmg;
        dmgtext(_dmg, iscritical);

        EffectGenerate(m_attackEffectName, transform.position);
        StartCoroutine("takeDamageCoroutin");
    }

    IEnumerator takeDamageCoroutin()
    {

        if (m_hp < 0)
        {
            state = "death";
            yield return null;
        }

        var renderer = GetComponent<SpriteRenderer>();
        yield return new WaitForSeconds(m_attackspeed * 0.15f);
        renderer.color = new Color32(255, 60, 60, 255);
        yield return new WaitForSeconds(0.2f);
        renderer.color = new Color32(255, 255, 255, 255);
        yield return null;

    }

    void gainsp(bool m_iscritical = false)
    {
        if ((m_maxsp - m_sp) < gainSpPerhit)
        {
            m_sp = m_maxsp;
            return;
        }
        m_sp += gainSpPerhit;
    }

    void dmgtext(float _dmg, bool iscritial)
    {
        var dmgtext = Instantiate(GameSceneSingleton.floatingtext);
        dmgtext.transform.SetParent(transform);
        dmgtext.transform.localPosition = new Vector3(0, GetComponent<BoxCollider2D>().size.y + 0.5f, 0);
        var dmgtext_set = dmgtext.GetComponent<floatingtext>();
        dmgtext_set.m_damage = _dmg;

        if (iscritial)
        {
            dmgtext_set.m_iscritical = true;
        }
    }
    #endregion

    #region 이펙트
    void EffectGenerate(string _effectname, Vector3 _targetposition)
    {
        var neweffect = Instantiate(GameSceneSingleton.SingletonEffect_list[_effectname]) as GameObject;
        neweffect.transform.position = _targetposition + new Vector3(0, 0.5f, 0);

    }

    #endregion

    #region 죽음
    IEnumerator DeathandDestroy()
    {
        m_animator.SetTrigger("Death");
        //리스트에서 선제거
        GameSceneSingleton.EleminateObject(gameObject, m_isMyteam);

        //데스타임 코루틴
        yield return new WaitForSeconds(m_animator.GetCurrentAnimatorStateInfo(0).length);

        Destroy(gameObject);

        yield return null;
    }
    #endregion

}
