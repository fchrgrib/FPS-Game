using System.Collections;
using UnityEngine;

public class SpeedOrbBehaviour : OrbBehaviour
{
    private const float OriginalSpeed = 15f;
    private Coroutine _speedModifierCoroutine;

    protected override void ExecuteOrbPerk(PlayerManager playerManager, PlayerMovement playerMovement)
    {
        // Stop the coroutine if it's already running
        if (_speedModifierCoroutine != null)
        {
            StopCoroutine(_speedModifierCoroutine);
        }

        playerMovement.Speed *= 1.2f;

        _speedModifierCoroutine = StartCoroutine(RevertSpeedAfterDelay(playerMovement, OriginalSpeed, 15f));
    }

    private IEnumerator RevertSpeedAfterDelay(PlayerMovement playerMovement, float originalSpeed, float delay)
    {
        yield return new WaitForSeconds(delay);

        playerMovement.Speed = originalSpeed;

        // Reset the coroutine reference
        _speedModifierCoroutine = null;
    }
}