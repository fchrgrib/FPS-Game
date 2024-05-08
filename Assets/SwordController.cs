using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{

    GameObject sword;
    Animator anim;

    private InputManager inputManager;

    private void Awake()
    {
        sword = GetComponent<GameObject>();
        anim = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if (inputManager.PlayerInput.OnGround.Attack.IsPressed())
        {
            anim.SetBool("attacking", true);
        }
        else if(!inputManager.PlayerInput.OnGround.Attack.IsPressed())
        {
            anim.SetBool("attacking", false);
        }
    }
}
