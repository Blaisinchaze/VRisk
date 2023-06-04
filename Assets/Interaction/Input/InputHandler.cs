using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Input input_asset { get; private set; }

    private void Awake()
    {
        input_asset = new Input();
    }

    private void OnEnable()
    {
        input_asset.Enable();
    }

    private void OnDisable()
    {
        input_asset.Disable();
    }
}
