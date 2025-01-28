using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Tutorial")] [SerializeField] private GameObject _tutorialContainer;
    
    [Header("Popup")] 
    [SerializeField] private Animator _popupWindow;
    [SerializeField] private TMP_Text _popupTitleText;
    [SerializeField] private TMP_Text _finalScoreText;

    [Header("Buttons")]
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _restartButton;

    [Header("Score Text")]
    [SerializeField] private TMP_Text _scoreText;
    
    [Header("Game Over")] 
    [SerializeField] private GameObject _scoreContainer; //Final score and best score container

    [Header("Player score Data")]
    [SerializeField] private ScriptableInt _score;
    
    [Header("SFX event")] 
    [SerializeField] private ScriptableEvent _onButtonPressed;

    [Header("Fade trigger")] 
    [SerializeField] private ScriptableEvent _triggerFadeInToMenu;
    [SerializeField] private ScriptableEvent _triggerFadeInToGame;
    [SerializeField] private ScriptableEvent _triggerFadeInToPause;
    [SerializeField] private ScriptableEvent _triggerFadeOut;

    private PlayerInputActions _inputActions;
    
    private void Start()
    {
        _pauseButton.onClick.AddListener(OnPressedPause);
        _resumeButton.onClick.AddListener(OnPressedResume);
        _quitButton.onClick.AddListener(OnPressedMenu);
        _restartButton.onClick.AddListener(OnPressedRestart);

        _score.OnValueChanged += UpdateScore;
        
        LevelManager.OnLevelFinish.Subscribe(FinishLevel);
        LevelManager.OnStartGame.Subscribe(StartGame);
        
        _scoreText.SetText("0");
        ShowTutorial(true);
        SetupInput();

    }

    private void OnDisable()
    {
        LevelManager.OnLevelFinish.Unsubscribe(FinishLevel);
        LevelManager.OnStartGame.Unsubscribe(StartGame);

        _inputActions.UI.Pause.performed -= TogglePause;
        _score.OnValueChanged -= UpdateScore;
    }
    
    private void SetupInput()
    {
        _inputActions = LevelManager.Instance.InputActions;
        _inputActions.UI.Pause.performed += TogglePause;
    }

    #region Button Input

    private void TogglePause(InputAction.CallbackContext _)
    {
        if (LevelManager.Instance.State == LevelManager.GameState.Play)
        {
            OnPressedPause();
        }
        else if (LevelManager.Instance.State == LevelManager.GameState.Paused)
        {
            OnPressedResume();
        }
    }
    
    private void OnPressedPause()
    {
        _onButtonPressed.Raise();
        LevelManager.OnPauseGame.Raise(true);
        Pause();
    }

    private void OnPressedMenu()
    {
        _onButtonPressed.Raise();
        _triggerFadeInToMenu.Raise();
    }

    private void OnPressedResume()
    {
        _onButtonPressed.Raise();
        _triggerFadeOut.Raise();
        LevelManager.OnPauseGame.Raise(false);
        ShowPopup(false);
    }

    private void OnPressedRestart()
    {
        _onButtonPressed.Raise();
        _triggerFadeInToGame.Raise();
    }
    
    #endregion
    
    #region UI
    private void UpdateScore()
    {
        _scoreText.SetText($"{_score.Value}");
    }

    private void ShowPopup(bool show)
    {
        _popupWindow.gameObject.SetActive(show);
    }
    
    private void ShowTutorial(bool show)
    {
        _tutorialContainer.SetActive(show);
        _scoreText.gameObject.SetActive(!show);
        _pauseButton.gameObject.SetActive(!show);
    }

    private void Pause()
    {
        _triggerFadeInToPause.Raise();

        _popupTitleText.SetText("Paused");
        _scoreContainer.SetActive(false);
        
        ShowPopup(true);
    }

    private void FinishLevel(bool won)
    {
        _triggerFadeInToPause.Raise();
        _pauseButton.interactable = false;

        var text = won ? "Congratulations" : "Game Over";
        
        _popupTitleText.SetText(text);
        _scoreContainer.SetActive(true);
        _resumeButton.gameObject.SetActive(false);
        
        _finalScoreText.SetText($"Score: {_score.Value}");
        
        ShowPopup(true);
    }

    private void StartGame()
    {
        ShowTutorial(false);
    }

    #endregion
}
