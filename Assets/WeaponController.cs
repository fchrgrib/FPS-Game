using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject gun;
    public GameObject shotgun;
    public GameObject sword;

    bool previousStep;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            shotgun.SetActive(false);
            gun.SetActive(true);
            sword.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            shotgun.SetActive(true);
            gun.SetActive(false);
            sword.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            shotgun.SetActive(false);
            gun.SetActive(false);
            sword.SetActive(true);
        }

    }
}
