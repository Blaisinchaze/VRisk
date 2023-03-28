//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.1
//     from Assets/Interaction/Input/Input.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Input : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input"",
    ""maps"": [
        {
            ""name"": ""InputActionMap"",
            ""id"": ""5e1e67e7-5ece-4866-91f1-ed181fb0a924"",
            ""actions"": [
                {
                    ""name"": ""Move(Key/Stick)"",
                    ""type"": ""Value"",
                    ""id"": ""da85b0a9-3f2f-42a7-a561-54dbc31626eb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Move(Left_Hand)"",
                    ""type"": ""Value"",
                    ""id"": ""bf24ebfe-6159-49ae-a9a2-7719e326feee"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Move(Right_Hand)"",
                    ""type"": ""Value"",
                    ""id"": ""e4e8096c-7042-41c0-8d61-78d43282b789"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard [WASD]"",
                    ""id"": ""d2a782ae-abb5-45d0-89ff-af40a4c71206"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move(Key/Stick)"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard [Arrows]"",
                    ""id"": ""c1aadacd-a12e-417a-a1fa-3776071b7576"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move(Key/Stick)"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""98151641-72db-4efa-8b4c-8a3663cec05d"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move(Key/Stick)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b8df71b8-f720-4d3d-8478-42e5ad1bfdbe"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move(Key/Stick)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a2d04f24-af27-4553-9d62-adef606c7177"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move(Key/Stick)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0368fcfc-e360-4999-9f3a-45dc0ce54d31"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move(Key/Stick)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""301d4ccb-926f-4c42-bb6c-95c84e39fb46"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move(Key/Stick)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9eb9cb12-40c5-40bd-b5ea-90f2bd718828"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move(Key/Stick)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0c42012e-ac85-44ef-b791-95203cc6d477"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move(Key/Stick)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fb0ddd10-4255-4a6f-8197-6ac79006d137"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move(Key/Stick)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ef4953eb-a8b8-4e08-9175-9188d2ca7d20"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move(Key/Stick)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3515ffd1-20b0-43c9-a3a7-a45223967b97"",
                    ""path"": ""<XRController>{LeftHand}/joystick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move(Key/Stick)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""66f06ed1-08fa-48dd-9677-fe49dbbde511"",
                    ""path"": ""<XRController>{LeftHand}/deviceAcceleration"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move(Left_Hand)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8d4ed35-5abc-4e4f-afcf-3d99fc818823"",
                    ""path"": ""<XRController>{RightHand}/deviceAcceleration"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move(Right_Hand)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // InputActionMap
        m_InputActionMap = asset.FindActionMap("InputActionMap", throwIfNotFound: true);
        m_InputActionMap_MoveKeyStick = m_InputActionMap.FindAction("Move(Key/Stick)", throwIfNotFound: true);
        m_InputActionMap_MoveLeft_Hand = m_InputActionMap.FindAction("Move(Left_Hand)", throwIfNotFound: true);
        m_InputActionMap_MoveRight_Hand = m_InputActionMap.FindAction("Move(Right_Hand)", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // InputActionMap
    private readonly InputActionMap m_InputActionMap;
    private IInputActionMapActions m_InputActionMapActionsCallbackInterface;
    private readonly InputAction m_InputActionMap_MoveKeyStick;
    private readonly InputAction m_InputActionMap_MoveLeft_Hand;
    private readonly InputAction m_InputActionMap_MoveRight_Hand;
    public struct InputActionMapActions
    {
        private @Input m_Wrapper;
        public InputActionMapActions(@Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveKeyStick => m_Wrapper.m_InputActionMap_MoveKeyStick;
        public InputAction @MoveLeft_Hand => m_Wrapper.m_InputActionMap_MoveLeft_Hand;
        public InputAction @MoveRight_Hand => m_Wrapper.m_InputActionMap_MoveRight_Hand;
        public InputActionMap Get() { return m_Wrapper.m_InputActionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InputActionMapActions set) { return set.Get(); }
        public void SetCallbacks(IInputActionMapActions instance)
        {
            if (m_Wrapper.m_InputActionMapActionsCallbackInterface != null)
            {
                @MoveKeyStick.started -= m_Wrapper.m_InputActionMapActionsCallbackInterface.OnMoveKeyStick;
                @MoveKeyStick.performed -= m_Wrapper.m_InputActionMapActionsCallbackInterface.OnMoveKeyStick;
                @MoveKeyStick.canceled -= m_Wrapper.m_InputActionMapActionsCallbackInterface.OnMoveKeyStick;
                @MoveLeft_Hand.started -= m_Wrapper.m_InputActionMapActionsCallbackInterface.OnMoveLeft_Hand;
                @MoveLeft_Hand.performed -= m_Wrapper.m_InputActionMapActionsCallbackInterface.OnMoveLeft_Hand;
                @MoveLeft_Hand.canceled -= m_Wrapper.m_InputActionMapActionsCallbackInterface.OnMoveLeft_Hand;
                @MoveRight_Hand.started -= m_Wrapper.m_InputActionMapActionsCallbackInterface.OnMoveRight_Hand;
                @MoveRight_Hand.performed -= m_Wrapper.m_InputActionMapActionsCallbackInterface.OnMoveRight_Hand;
                @MoveRight_Hand.canceled -= m_Wrapper.m_InputActionMapActionsCallbackInterface.OnMoveRight_Hand;
            }
            m_Wrapper.m_InputActionMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveKeyStick.started += instance.OnMoveKeyStick;
                @MoveKeyStick.performed += instance.OnMoveKeyStick;
                @MoveKeyStick.canceled += instance.OnMoveKeyStick;
                @MoveLeft_Hand.started += instance.OnMoveLeft_Hand;
                @MoveLeft_Hand.performed += instance.OnMoveLeft_Hand;
                @MoveLeft_Hand.canceled += instance.OnMoveLeft_Hand;
                @MoveRight_Hand.started += instance.OnMoveRight_Hand;
                @MoveRight_Hand.performed += instance.OnMoveRight_Hand;
                @MoveRight_Hand.canceled += instance.OnMoveRight_Hand;
            }
        }
    }
    public InputActionMapActions @InputActionMap => new InputActionMapActions(this);
    public interface IInputActionMapActions
    {
        void OnMoveKeyStick(InputAction.CallbackContext context);
        void OnMoveLeft_Hand(InputAction.CallbackContext context);
        void OnMoveRight_Hand(InputAction.CallbackContext context);
    }
}
