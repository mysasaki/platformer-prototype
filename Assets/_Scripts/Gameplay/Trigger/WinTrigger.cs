using UnityEngine;

public class WinTrigger : ATrigger
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && LevelManager.Instance.State != LevelManager.GameState.Won)
        {
            if (_isTriggered) return;

            Debug.Log("Won game");
            _isTriggered = true;
            _triggerEvent.Raise();
        }
    }
}
