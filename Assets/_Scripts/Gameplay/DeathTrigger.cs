using UnityEngine;

public class DeathTrigger : Trigger
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && LevelManager.Instance.State != LevelManager.PlayerState.Dead)
        {
            Debug.Log("AAAAA");
            if (_isTriggered) return;

            _isTriggered = true;
            _triggerEvent.Raise();
            LevelManager.OnPlayerDie.Raise();
        }
    }
}