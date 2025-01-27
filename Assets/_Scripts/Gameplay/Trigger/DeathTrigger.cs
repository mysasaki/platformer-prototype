using UnityEngine;

public class DeathTrigger : ATrigger
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && LevelManager.Instance.State != LevelManager.GameState.Dead)
        {
            if (_isTriggered) return;
            Debug.Log("Game over");

            _isTriggered = true;
            _triggerEvent.Raise();
            LevelManager.OnPlayerDie.Raise();
        }
    }
}