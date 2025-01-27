using UnityEngine;

public class ScoreTrigger : ATrigger
{
    [Header("Data")]
    [SerializeField] private ScriptableInt _score;
    [SerializeField] private ScriptableInt _bestScore;
    
    [Header("Unique configuration")]
    [SerializeField] private bool _destroyOnCollide;
    [SerializeField] private int _scoreIncrease = 1;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && LevelManager.Instance.State == LevelManager.GameState.Play)
        {
            if (_isTriggered) return;

            _isTriggered = true;
            _triggerEvent.Raise();

            IncreaseScore();
        }

        if (_destroyOnCollide)
        {
            Destroy(gameObject);
        }
    }

    private void IncreaseScore()
    {
        _score.Value += _scoreIncrease;

        if (_bestScore.Value < _score.Value) _bestScore.Value = _score.Value;
    }
}