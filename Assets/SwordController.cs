using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{

    public Transform attackPoint;
    public float damage = 30f;

    private float damageDefault;
    private int attackCount;
    Animator anim;
    private LayerMask enemyLayerMask;
    private InputManager inputManager;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyLayerMask = LayerMask.GetMask("Enemy");
        inputManager = GetComponent<InputManager>();
        damageDefault = 30f;
        EventManager.StartListening("OneHitDamage",MultiplyDamage);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("OneHitDamage",MultiplyDamage);
    }

    private void MultiplyDamage(bool isActive)
    {
        if (isActive)
        {
            damage*=1000f;   
        }
        else
        {
            damage = damageDefault;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (inputManager.PlayerInput.OnGround.Attack.IsPressed())
        {
            anim.SetBool("attacking", true);
            attackCount += 1;
            
            if (attackCount == 1)
            {
                SwingDamage();
            }
        }
        else if(!inputManager.PlayerInput.OnGround.Attack.IsPressed())
        {
            anim.SetBool("attacking", false);
            attackCount = 0;
        }
    }


    void SwingDamage()
    {
        Collider[] hitEnemy = Physics.OverlapSphere(attackPoint.position, 2f, enemyLayerMask);
        int dam = 0;
        foreach (Collider enemy in hitEnemy)
        {
            dam += 1;
            if (dam>1)
            {
                continue;
            }
            enemy.GetComponent<EnemyManager>().TakeDamage(damage, attackPoint.position);
        }
    }
}
