using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject gun;
    public GameObject shotgun;
    public GameObject sword;

    int currentWeapon = 1;
    float scrollWheel;
    List<GameObject> weapons = new List<GameObject>();
    // Start is called before the first frame update
    private void Awake()
    {
        weapons.Add(gun);
        weapons.Add(shotgun);
        weapons.Add(sword);
    }

    void ChangeWeapon()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].SetActive((i + 1) == currentWeapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (scrollWheel > 0)
        {
            currentWeapon = (currentWeapon + 1) % 3 + 1;
            ChangeWeapon();

        }

        if (scrollWheel < 0)
        {
            currentWeapon -= 1;
            if (currentWeapon < 1)
            {
                currentWeapon = 3;
            }
            ChangeWeapon();
        }

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
