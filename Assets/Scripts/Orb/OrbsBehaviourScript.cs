using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OrbsBehaviourScript : MonoBehaviour
{
    public float countdownDuration = 5f;
    public float detectionRadius = 5f;
    [SerializeField] private LayerMask playerLayerMask;

    private bool used;

    private void Start()
    {
        Invoke(nameof(RemoveOrb), countdownDuration);
    }

    protected void RemoveOrb()
    {
        if (used)
        {
            return;
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayerMask);
        if (hitColliders.Length != 1)
        {
            return;
        }

        PlayerManager playerManager = hitColliders[0].GetComponentInParent<PlayerManager>();
        PlayerEnter(playerManager);
        RemoveOrb();
        used = true;
    }

    protected abstract void PlayerEnter(PlayerManager playerManager);
}
