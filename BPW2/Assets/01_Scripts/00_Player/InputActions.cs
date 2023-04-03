//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/01_Scripts/InputActions.inputactions
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

public partial class @InputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""Map"",
            ""id"": ""49a6f3c3-2252-475b-b3f8-826e264908b6"",
            ""actions"": [
                {
                    ""name"": ""Use"",
                    ""type"": ""Button"",
                    ""id"": ""893de645-03bf-4e44-aaae-dfe0dbf1a219"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectSlot1"",
                    ""type"": ""Button"",
                    ""id"": ""4051b399-626b-47d9-a489-f31a77d578bf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectSlot2"",
                    ""type"": ""Button"",
                    ""id"": ""bfd82d09-5de7-42c8-9a72-687591c47814"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectSlot3"",
                    ""type"": ""Button"",
                    ""id"": ""6df8c635-3377-4a35-b739-0a906aa596aa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Drop"",
                    ""type"": ""Button"",
                    ""id"": ""e91aa69d-8351-4751-8228-a3869ead116c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""9ba9df24-8363-4fe7-aad1-5d75689d5f2b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7d728c02-7cb7-4fb9-bd07-6996529ab25c"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Standard"",
                    ""action"": ""Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fef246e5-045a-4ab3-a253-7cf81d5b1911"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Standard"",
                    ""action"": ""SelectSlot1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f14ddeb-e52d-4a17-8a23-c4bb55749f1b"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Standard"",
                    ""action"": ""SelectSlot2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a398534-1892-4aad-a930-8ff156fc1302"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Standard"",
                    ""action"": ""SelectSlot3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f6068117-89af-42d6-86e5-cfa3d444f2ea"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Standard"",
                    ""action"": ""Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""94c059ec-0baf-4f09-8b26-6b5813bcfe9c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ed327b2f-552c-4978-9bd2-1865c57ba02b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5c27fcf7-861f-4bc5-82b4-e0de696f1dc8"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a41f3aef-0c29-42a9-9080-be18497cfff6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c4187213-7fad-4f70-9585-49b6b6c4e925"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Standard"",
            ""bindingGroup"": ""Standard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Map
        m_Map = asset.FindActionMap("Map", throwIfNotFound: true);
        m_Map_Use = m_Map.FindAction("Use", throwIfNotFound: true);
        m_Map_SelectSlot1 = m_Map.FindAction("SelectSlot1", throwIfNotFound: true);
        m_Map_SelectSlot2 = m_Map.FindAction("SelectSlot2", throwIfNotFound: true);
        m_Map_SelectSlot3 = m_Map.FindAction("SelectSlot3", throwIfNotFound: true);
        m_Map_Drop = m_Map.FindAction("Drop", throwIfNotFound: true);
        m_Map_Move = m_Map.FindAction("Move", throwIfNotFound: true);
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

    // Map
    private readonly InputActionMap m_Map;
    private IMapActions m_MapActionsCallbackInterface;
    private readonly InputAction m_Map_Use;
    private readonly InputAction m_Map_SelectSlot1;
    private readonly InputAction m_Map_SelectSlot2;
    private readonly InputAction m_Map_SelectSlot3;
    private readonly InputAction m_Map_Drop;
    private readonly InputAction m_Map_Move;
    public struct MapActions
    {
        private @InputActions m_Wrapper;
        public MapActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Use => m_Wrapper.m_Map_Use;
        public InputAction @SelectSlot1 => m_Wrapper.m_Map_SelectSlot1;
        public InputAction @SelectSlot2 => m_Wrapper.m_Map_SelectSlot2;
        public InputAction @SelectSlot3 => m_Wrapper.m_Map_SelectSlot3;
        public InputAction @Drop => m_Wrapper.m_Map_Drop;
        public InputAction @Move => m_Wrapper.m_Map_Move;
        public InputActionMap Get() { return m_Wrapper.m_Map; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MapActions set) { return set.Get(); }
        public void SetCallbacks(IMapActions instance)
        {
            if (m_Wrapper.m_MapActionsCallbackInterface != null)
            {
                @Use.started -= m_Wrapper.m_MapActionsCallbackInterface.OnUse;
                @Use.performed -= m_Wrapper.m_MapActionsCallbackInterface.OnUse;
                @Use.canceled -= m_Wrapper.m_MapActionsCallbackInterface.OnUse;
                @SelectSlot1.started -= m_Wrapper.m_MapActionsCallbackInterface.OnSelectSlot1;
                @SelectSlot1.performed -= m_Wrapper.m_MapActionsCallbackInterface.OnSelectSlot1;
                @SelectSlot1.canceled -= m_Wrapper.m_MapActionsCallbackInterface.OnSelectSlot1;
                @SelectSlot2.started -= m_Wrapper.m_MapActionsCallbackInterface.OnSelectSlot2;
                @SelectSlot2.performed -= m_Wrapper.m_MapActionsCallbackInterface.OnSelectSlot2;
                @SelectSlot2.canceled -= m_Wrapper.m_MapActionsCallbackInterface.OnSelectSlot2;
                @SelectSlot3.started -= m_Wrapper.m_MapActionsCallbackInterface.OnSelectSlot3;
                @SelectSlot3.performed -= m_Wrapper.m_MapActionsCallbackInterface.OnSelectSlot3;
                @SelectSlot3.canceled -= m_Wrapper.m_MapActionsCallbackInterface.OnSelectSlot3;
                @Drop.started -= m_Wrapper.m_MapActionsCallbackInterface.OnDrop;
                @Drop.performed -= m_Wrapper.m_MapActionsCallbackInterface.OnDrop;
                @Drop.canceled -= m_Wrapper.m_MapActionsCallbackInterface.OnDrop;
                @Move.started -= m_Wrapper.m_MapActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MapActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MapActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_MapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Use.started += instance.OnUse;
                @Use.performed += instance.OnUse;
                @Use.canceled += instance.OnUse;
                @SelectSlot1.started += instance.OnSelectSlot1;
                @SelectSlot1.performed += instance.OnSelectSlot1;
                @SelectSlot1.canceled += instance.OnSelectSlot1;
                @SelectSlot2.started += instance.OnSelectSlot2;
                @SelectSlot2.performed += instance.OnSelectSlot2;
                @SelectSlot2.canceled += instance.OnSelectSlot2;
                @SelectSlot3.started += instance.OnSelectSlot3;
                @SelectSlot3.performed += instance.OnSelectSlot3;
                @SelectSlot3.canceled += instance.OnSelectSlot3;
                @Drop.started += instance.OnDrop;
                @Drop.performed += instance.OnDrop;
                @Drop.canceled += instance.OnDrop;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public MapActions @Map => new MapActions(this);
    private int m_StandardSchemeIndex = -1;
    public InputControlScheme StandardScheme
    {
        get
        {
            if (m_StandardSchemeIndex == -1) m_StandardSchemeIndex = asset.FindControlSchemeIndex("Standard");
            return asset.controlSchemes[m_StandardSchemeIndex];
        }
    }
    public interface IMapActions
    {
        void OnUse(InputAction.CallbackContext context);
        void OnSelectSlot1(InputAction.CallbackContext context);
        void OnSelectSlot2(InputAction.CallbackContext context);
        void OnSelectSlot3(InputAction.CallbackContext context);
        void OnDrop(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
    }
}