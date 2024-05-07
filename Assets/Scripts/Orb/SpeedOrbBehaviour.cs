using System.Collections;
using UnityEngine;

public class SpeedOrbBehaviour : OrbBehaviour
{
    private Coroutine _speedModifierCoroutine;
    private const float OriginalSpeed = 15f;

    protected override void ExecuteOrbPerk(PlayerManager playerManager, PlayerMovement playerMovement)
    {
        if (_speedModifierCoroutine != null)
        {
            // Stop the coroutine if it's already running
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