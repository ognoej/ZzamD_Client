using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hplinescript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        GetComponent<RectTransform>().sizeDelta = GetComponentInParent<Slider>().gameObject.GetComponent<RectTransform>().sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
