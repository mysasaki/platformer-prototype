using UnityEngine;

public class BackgroundExtender : MonoBehaviour
{
    private Camera _camera;
    private float _spriteWidth;
    private Transform[] _backgrounds;

    private void Start()
    {
        _camera = Camera.main;
        
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
        //TODO: optmize
        float cameraX = _camera.transform.position.x;

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
        part.position = new Vector3(part.position.x + offset, part.position.y, part.position.z);
    }
}