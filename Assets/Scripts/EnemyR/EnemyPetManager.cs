using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class EnemyPetManager : MonoBehaviour
{
    public List<GameObject> pets;
    
    private EnemyAttack enemyAttack;

    private int counter;

    private void Awake()
    {
        enemyAttack = GetComponentInChildren<EnemyAttack>();
        
        enemyAttack.attackDamageMultiplier += 0.2f * pets.Count;
        EventManager.StartListening("PetDeath", PetDeath);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("PetDeath", PetDeath);
    }

    private void PetDeath()
    {
        var randomPet = pets[Random.Range(0, pets.Count)];
        randomPet.GetComponentInChildren<EnemyPetBehavior>().MeDead();
        enemyAttack.attackDamageMultiplier -= 0.2f;
        pets.Remove(randomPet);

        StartCoroutine(KillPet(randomPet));
    }

    private IEnumerator KillPet(Object pet)
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            counter++;

            if (counter >= 3)
            {
                Destroy(pet);
                counter = 0;
                break;
            }
        }
    }
}