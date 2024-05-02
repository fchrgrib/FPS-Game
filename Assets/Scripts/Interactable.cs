using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    public String interactText;

    public void Interact(InputManager inputManager)
    {
        Action(inputManager);
    }

    protected abstract void Action(InputManager inputManager);

}
