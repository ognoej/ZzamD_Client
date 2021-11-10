using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mopinfo : MonoBehaviour
{
    public bool generateMops(string _objname, bool _ismyteam)
    {
        switch (_objname)
        {
            case "hero":
                {
                    print("히어로생성콜");
                    var newobj = Instantiate(GameSceneSingleton.SingletonObj_list["hero"]) as GameObject;
                    var tempobjscript = newobj.GetComponent<Object_Controller>();
                    tempobjscript.state = "idle";

                    //오브젝트별 설정 추후 따로 함수 구현
                    objinfo newobjinfo;
                    newobjinfo.objname = "hero";
                    newobjinfo.movespeed = 1.0f;
                    newobjinfo.attackspeed = 2.0f;
                    newobjinfo.attackrange = 0.7f;
                    newobjinfo.hp = 100;
                    newobjinfo.mp = 10;
                    newobjinfo.skillsp = 10;
                    newobjinfo.dmg = 15;
                    newobjinfo.skilldmg = 25;
                    newobjinfo.criticalchance = 60;
                    newobjinfo.attackeffectname = "slash_red";
                    newobjinfo.skilleffectname = "slash_red";
                    newobjinfo.isbullet = false;
                    newobjinfo.bulletname = "";
                    newobjinfo.isskillsplash = false;
                    newobjinfo.skillrange = new Vector3(1.0f, 1.0f, 0);

                    // 오브젝트 생성위치 수정필요
                    newobj.transform.position = new Vector3(-3, 0, 0);

                    if (_ismyteam)
                    {
                        newobjinfo.isMyteam = true;
                        tempobjscript.m_objinfo = newobjinfo;
                        GameSceneSingleton.Player_list.Add(newobj);
                        print("아군 " + newobjinfo.objname + " 생성됨");
                    }
                    else
                    {
                        newobjinfo.isMyteam = false;
                        tempobjscript.m_objinfo = newobjinfo;
                        GameSceneSingleton.Enermy_list.Add(newobj);
                        print("적군 " + newobjinfo.objname + " 생성됨");
                    }

                    break;
                }

            case "bandit":
                {
                    var newobj = Instantiate(GameSceneSingleton.SingletonObj_list["bandit"]) as GameObject;
                    var tempobjscript = newobj.GetComponent<Object_Controller>();
                    tempobjscript.state = "idle";

                    //오브젝트별 설정 추후 따로 함수 구현
                    objinfo newobjinfo;
                    newobjinfo.objname = "bandit";
                    newobjinfo.movespeed = 1.5f;
                    newobjinfo.attackspeed = 3.0f;
                    newobjinfo.attackrange = 0.7f;
                    newobjinfo.hp = 100;
                    newobjinfo.mp = 10;
                    newobjinfo.skillsp = 10;
                    newobjinfo.dmg = 10;
                    newobjinfo.skilldmg = 15;
                    newobjinfo.criticalchance = 0;
                    newobjinfo.attackeffectname = "slash_red";
                    newobjinfo.skilleffectname = "slash_red";
                    newobjinfo.isbullet = false;
                    newobjinfo.bulletname = "";
                    newobjinfo.isskillsplash = false;
                    newobjinfo.skillrange = new Vector3(1.0f,1.0f,0);

                    // 오브젝트 생성위치 수정필요
                    newobj.transform.position = new Vector3(3, 0, 0);

                    if (_ismyteam)
                    {
                        newobjinfo.isMyteam = true;
                        tempobjscript.m_objinfo = newobjinfo;
                        GameSceneSingleton.Player_list.Add(newobj);
                        print("아군 " + newobjinfo.objname + " 생성됨");
                    }
                    else
                    {
                        newobjinfo.isMyteam = false;
                        tempobjscript.m_objinfo = newobjinfo;
                        GameSceneSingleton.Enermy_list.Add(newobj);
                        print("적군 " + newobjinfo.objname + " 생성됨");
                    }


                    break;
                }

            case "bowman":
                {
                    print("히어로생성콜");
                    var newobj = Instantiate(GameSceneSingleton.SingletonObj_list["bowman"]) as GameObject;
                    var tempobjscript = newobj.GetComponent<Object_Controller>();
                    tempobjscript.state = "idle";

                    //오브젝트별 설정 추후 따로 함수 구현
                    objinfo newobjinfo;
                    newobjinfo.objname = "bowman";
                    newobjinfo.movespeed = 1.0f;
                    newobjinfo.attackspeed = 2.0f;
                    newobjinfo.attackrange = 4f;
                    newobjinfo.hp = 100;
                    newobjinfo.mp = 10;
                    newobjinfo.skillsp = 2;
                    newobjinfo.dmg = 15;
                    newobjinfo.skilldmg = 20;
                    newobjinfo.criticalchance = 60;
                    newobjinfo.attackeffectname = "slash_red";
                    newobjinfo.skilleffectname = "fireboom";
                    newobjinfo.isbullet = true;
                    newobjinfo.bulletname = "Arrow";
                    newobjinfo.isskillsplash = true;
                    newobjinfo.skillrange = new Vector3(10.0f, 2.0f, 0);

                    // 오브젝트 생성위치 수정필요
                    newobj.transform.position = new Vector3(-5, 0, 0);

                    if (_ismyteam)
                    {
                        newobjinfo.isMyteam = true;
                        tempobjscript.m_objinfo = newobjinfo;
                        GameSceneSingleton.Player_list.Add(newobj);
                        print("아군 " + newobjinfo.objname + " 생성됨");
                    }
                    else
                    {
                        newobjinfo.isMyteam = false;
                        tempobjscript.m_objinfo = newobjinfo;
                        GameSceneSingleton.Enermy_list.Add(newobj);
                        print("적군 " + newobjinfo.objname + " 생성됨");
                    }

                    break;
                }


        }
        return true;
    }
}