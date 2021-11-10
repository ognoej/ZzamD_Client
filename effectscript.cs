using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectscript : MonoBehaviour
{
    Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_animator.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void destroyobj()
    {
        Destroy(gameObject);
    }
}
