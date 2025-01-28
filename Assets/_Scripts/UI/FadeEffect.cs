using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FadeEffect : MonoBehaviour
{
    [SerializeField] private float _fadeToAlpha;
    [SerializeField] private float _duration;
    [SerializeField] private float _currentAlpha;

    private Image _image;
    private Color _color;
    private const float Tolerance = 0.001f; //Floating point tolerance

    private void Start()
    {
        _image = GetComponent<Image>();
        _color = _image.color;

        _fadeToAlpha = 0;
    }

    public void SetFadeToValue(float alpha)
    {
        _fadeToAlpha = alpha;
    }

    private void Update()
    {
        if (!ShouldFade())
        {
            return;
        }
        
        _currentAlpha = Mathf.MoveTowards(_currentAlpha, _fadeToAlpha, Time.unscaledDeltaTime / _duration);
        _color.a = _currentAlpha;
        _image.color = _color;
        
    }
    
    private bool ShouldFade()
    {
        return Mathf.Abs(_fadeToAlpha - _currentAlpha) > Tolerance;
    }
}
