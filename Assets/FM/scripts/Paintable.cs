using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintable : MonoBehaviour
{
    [SerializeField] private List<Renderer> _renderers;
    [SerializeField] private string _colorName;

    private MaterialPropertyBlock _propertyBlock;

    private void Awake()
    {
        _propertyBlock = new MaterialPropertyBlock();
    }

    public void SetColor(Color color)
    {
        foreach (Renderer renderer in _renderers)
        {
            renderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetColor(_colorName, color);
            renderer.SetPropertyBlock(_propertyBlock);
        }
    }

    public void Hide()
    {
        foreach (Renderer renderer in _renderers)
        {
            renderer.enabled = false;
        }
    }

    public void Show()
    {
        foreach (Renderer renderer in _renderers)
        {
            renderer.enabled = true;
        }
    }
}