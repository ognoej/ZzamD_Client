using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;


public class GameSceneMgr : MonoBehaviour
{

    Mopinfo m_generator;

    // Start is called before the first frame update
    void Start()
    {
        m_generator = new Mopinfo();
    }



    // Update is called once per frame
    void Update()
    {
     
    }



    public void Push_MakeobjBtn()
    {
        m_generator.generateMops("hero", true);
    }

    public void Push_MakebowmanBtn()
    {
        m_generator.generateMops("bowman", true);
    }

    public void Generate_EnermyBtn()
    {
        m_generator.generateMops("bandit", false);
    }

    
}
