using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectController : MonoBehaviour
{
    private InputManager inputManager;
    private Animator anim;
    void Awake()
    {
        inputManager = GetComponent<InputManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inputManager.PlayerInput.OnGround.Inspect.IsPressed())
        {
            anim.SetBool("IsInspect", true);
        }
        else if (!inputManager.PlayerInput.OnGround.Inspect.IsPressed())
        {
            anim.SetBool("IsInspect", false);   
        }
    }
}
