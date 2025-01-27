using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public enum PlayerState
    {
        Tutorial, Play, Paused, Dead
    }
    
    #region Singleton
    private static LevelManager _instance;
    public static LevelManager Instance => _instance;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    #endregion
    
    #region Events
    //Player events
    public static Event OnPlayerDie = new();
    
    //Game loop events
    public static Event OnStartGame = new(); //Triggered after tutorial ends
    public static Event<bool> OnPauseGame = new();
    public static Event OnGameOver = new();
    #endregion
    
    [Header("Player Data")]
    [SerializeField] private ScriptableInt _score;

    private PlayerState _state;
    public PlayerState State => _state;

    private void OnEnable()
    {
        OnPlayerDie.Subscribe(GameOver);
        OnPauseGame.Subscribe(PauseGame);
    }

    private void Start()
    {
        StartGame();
        // SetupInput();
        //
        // _state = PlayerState.Tutorial;
        // _score.Value = 0;
        // Time.timeScale = 0;
    }

    private void SetupInput()
    {
        // _jumpButton = InputManager.Instance.GetButtonById("JumpButton");
        // if (_jumpButton != null) 
        // {
        //     _jumpButton.OnButtonReleasedEvent.Subscribe(StartGame);
        // }
        //
        // _jumpMouseButton = InputManager.Instance.GetMouseButtonById("MouseJumpButton");
        // if (_jumpMouseButton != null)
        // {
        //     _jumpMouseButton.OnButtonReleasedEvent.Subscribe(StartGame);
        // }
    }

    private void StartGame()
    {
        if (_state != PlayerState.Tutorial) return;

        _state = PlayerState.Play;
        
        OnStartGame.Raise();
        PauseGame(false);
    }

    private void GameOver()
    {
        if (_state == PlayerState.Dead) return;

        _state = PlayerState.Dead;
        OnGameOver.Raise();
    }

    private void PauseGame(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
        _state = pause ? PlayerState.Paused : PlayerState.Play;
    }

    private void OnDisable()
    {
        OnPlayerDie.Unsubscribe(GameOver);
        OnPauseGame.Unsubscribe(PauseGame);
    }
}
