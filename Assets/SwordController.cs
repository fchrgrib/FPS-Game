using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{

    GameObject sword;
    Animator anim;

    private void Awake()
    {
        sword = GetComponent<GameObject>();
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("attacking", true);
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("attacking", false);
        }
    }
}
