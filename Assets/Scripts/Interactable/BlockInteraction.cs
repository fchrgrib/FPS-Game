using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInteraction : Interactable
{

    public BlockInteraction()
    {
        interactText = "Press E to finish this MF";
    }
    
    protected override void Action(InputManager inputManager)
    {
        Debug.Log("action called");
    }
    
}
