using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public GameObject box;

    int currentHealth;

    // Start is called before the first frame update
    private void OnEnable()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamage(int amaount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= amaount;
        }
        else
        {
            Debug.Log("Box is dead");
            box.SetActive(false);
        }
        Debug.Log("Current Health: " + currentHealth);
    }
}
