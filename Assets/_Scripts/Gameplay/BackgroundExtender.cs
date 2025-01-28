using UnityEngine;

public class BackgroundExtender : MonoBehaviour
{
    private Transform _cameraTransform;
    private float _spriteWidth;
    private Transform[] _backgrounds;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
        
        var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        if (!spriteRenderer)
        {
            return;
        }
        
        _spriteWidth = spriteRenderer.bounds.size.x;
        _backgrounds = new Transform[transform.childCount];
        
        for (var i = 0; i < transform.childCount; i++)
        {
            _backgrounds[i] = transform.GetChild(i);
        }
    }

    private void Update()
    {
        float cameraX = _cameraTransform.position.x;

        foreach (Transform part in _backgrounds)
        {
            float distanceToCamera = part.position.x - cameraX;

            if (distanceToCamera > _spriteWidth)
            {
                RepositionPart(part, -_spriteWidth * _backgrounds.Length);
            }
            else if (distanceToCamera < -_spriteWidth)
            {
                RepositionPart(part, _spriteWidth * _backgrounds.Length);
            }
        }
    }

    private void RepositionPart(Transform part, float offset)
    {
        var position = part.position;
        
        position = new Vector3(position.x + offset, position.y, position.z);
        part.position = position;
    }
}