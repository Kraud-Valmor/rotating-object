using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ColorController : MonoBehaviour
{
    private Camera cam = null;

    [SerializeField] private Transform _coin;
    [SerializeField] private Material _copper;
    [SerializeField] private Material _silver;
    [SerializeField] private Material _gold;
    [SerializeField] private LayerMask _coinLayer;

    private PlayerInput _input;

    private void Awake()
    {
        _input = new PlayerInput();
    }

    private void Start()
    {
        cam = Camera.main;

        _input.Player.Click.performed += context => ChangeColor();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void ChangeColor()
    {
        var ray = cam.ScreenPointToRay(_input.Player.Position.ReadValue<Vector2>());

        if (Physics.Raycast(ray, out var hit, 100, _coinLayer))
        {
            var coinRenderer = _coin.GetComponent<Renderer>();
            var coinMaterialName = MaterialName(coinRenderer.material);

            coinRenderer.material = Next(coinMaterialName);
        }
    }

    private Material Next(string current)
    {
        if (current == _copper.name)
            return _silver;
        if (current == _silver.name)
            return _gold;
        if (current == _gold.name)
            return _copper;

        throw new InvalidOperationException($"Invalid material: {current}");
    }

    private static string MaterialName(Material x) => x.name.Replace(" (Instance)", "");
}