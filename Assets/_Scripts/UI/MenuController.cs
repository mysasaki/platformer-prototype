using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;
    [Header("SFX event")] 
    [SerializeField] private ScriptableEvent _onButtonPressed;
    [Header("Fade event")]
    [SerializeField] private ScriptableEvent _triggerFadeInToGame;

    private void Start()
    {
        SetupUI();
    }

    private void SetupUI()
    {
        _startButton.onClick.AddListener(StartGame);
        _exitButton.onClick.AddListener(ExitGame);
    }

    private void StartGame()
    {
        _onButtonPressed.Raise();
        _triggerFadeInToGame.Raise();
    }

    private void ExitGame()
    {
        _onButtonPressed.Raise();
        Application.Quit();
    }
}