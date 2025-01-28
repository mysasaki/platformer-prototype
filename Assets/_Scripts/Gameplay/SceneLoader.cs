using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField][Tooltip("Time interval before moving to next scene")] private float _duration;
    
    private float _currentTime;
    private int _sceneIndex;
    private bool _loadScene;

    private void Start()
    {
        _loadScene = false;
    }

    private void Update()
    {
        if (!_loadScene) return;
        
        _currentTime += Time.unscaledDeltaTime;

        if (_currentTime > _duration)
        {
            SceneManager.LoadScene(_sceneIndex);
        }
    }

    public void WaitAndLoadScene(int sceneIndex)
    {
        _loadScene = true;
        _sceneIndex = sceneIndex;
        _currentTime = 0;
    }
}