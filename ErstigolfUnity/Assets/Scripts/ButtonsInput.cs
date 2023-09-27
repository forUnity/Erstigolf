//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Scripts/ButtonsInput.inputactions
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

public partial class @ButtonsInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @ButtonsInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ButtonsInput"",
    ""maps"": [
        {
            ""name"": ""Car"",
            ""id"": ""9e8a07ce-3286-460c-a922-673ee15cad78"",
            ""actions"": [
                {
                    ""name"": ""Red_Press"",
                    ""type"": ""Button"",
                    ""id"": ""58a75380-434a-4261-9303-1a98ad3f1f02"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Red_Release"",
                    ""type"": ""Button"",
                    ""id"": ""e7079715-ddeb-4d26-83f2-07475bd9fa78"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Black_Press"",
                    ""type"": ""Button"",
                    ""id"": ""3cfec8b9-c932-4644-abdf-c194aa26768f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Black_Release"",
                    ""type"": ""Button"",
                    ""id"": ""fd37b0c0-c7c7-47e1-a8f4-b3067770b237"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Blue"",
                    ""type"": ""Button"",
                    ""id"": ""44a97298-9b0a-4162-a77e-72b39ebfef0a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Blue_Release"",
                    ""type"": ""Button"",
                    ""id"": ""1588adf3-e9d6-4550-a27f-7790c3269d58"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Green"",
                    ""type"": ""Button"",
                    ""id"": ""a7979ffb-89a6-47fc-9bfa-902a13f62c24"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Green-Release"",
                    ""type"": ""Button"",
                    ""id"": ""35567f03-5175-4709-9d84-fd51ed66a333"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""White"",
                    ""type"": ""Button"",
                    ""id"": ""4e120b5a-f9a2-41da-b056-2b469348288d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""White_Release"",
                    ""type"": ""Button"",
                    ""id"": ""ecde083f-9d4f-4d0b-8a62-7db4d94e156c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Yellow"",
                    ""type"": ""Button"",
                    ""id"": ""0de40ad6-8755-4e97-a432-15287fbfdfa7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Yellow_Release"",
                    ""type"": ""Button"",
                    ""id"": ""0db4fc1f-abd7-42f6-a3fd-ace5b99fd523"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5aaf4771-2f8d-4a4e-9f34-9dcceabbadbf"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cfbbb2c4-cdec-4705-b2ab-2e54183aba15"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Red_Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b9d2079-320d-45fa-811b-410071897322"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Red_Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1887089f-c32b-43f8-a544-6a2dee1cd9b4"",
                    ""path"": ""<Keyboard>/u"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Black_Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a9b8a3e0-8b12-4f20-b4c4-050377a633ad"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Black_Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""203b01a1-ab99-480a-b9e2-5996b065768b"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Blue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""56f16002-ad0b-491d-b140-be7f922c885c"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Blue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dc04cf4e-4502-42de-8b7a-ec394033eda5"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Green"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2ad1ce0-efab-4b33-8ced-0254fc8396db"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Green"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec6303a0-06f7-4ca8-9400-141ac12e5628"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""White"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b3b8bb0-c200-4c0b-9457-b6dfb98f03a0"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""White"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3656bb38-69a4-42c1-9878-ced34fa89d32"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Yellow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""37d55fe4-5730-46a9-a5a5-0c6ce1031de7"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Yellow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8b53e3a-4039-46af-843c-cde8626b9b1d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Red_Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6e12e1aa-239f-412e-929d-493b5dde602c"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Red_Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""068fad42-ff05-4d1f-b842-ec8d7602026a"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Black_Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b672858d-92a9-4d3b-bffc-080fc784a497"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Black_Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9805660f-d683-447c-bea6-badbe5c1cdea"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Blue_Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""34cbe871-e828-4442-b4f1-2ad85be07e45"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Green-Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""54595542-7c71-4b1f-bb0d-e3f5fc1c1657"",
                    ""path"": ""<Keyboard>/o"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""White_Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7f328f98-77e2-428c-8c5a-a99d8eaac9c7"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Yellow_Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""87e16788-7bef-4f43-ba32-6a9d5a45f8d9"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Yellow_Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e84b093d-4f0a-4128-b74e-5ba292d71816"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea0abd57-2f10-488f-9f47-fc887c769eb1"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Car
        m_Car = asset.FindActionMap("Car", throwIfNotFound: true);
        m_Car_Red_Press = m_Car.FindAction("Red_Press", throwIfNotFound: true);
        m_Car_Red_Release = m_Car.FindAction("Red_Release", throwIfNotFound: true);
        m_Car_Black_Press = m_Car.FindAction("Black_Press", throwIfNotFound: true);
        m_Car_Black_Release = m_Car.FindAction("Black_Release", throwIfNotFound: true);
        m_Car_Blue = m_Car.FindAction("Blue", throwIfNotFound: true);
        m_Car_Blue_Release = m_Car.FindAction("Blue_Release", throwIfNotFound: true);
        m_Car_Green = m_Car.FindAction("Green", throwIfNotFound: true);
        m_Car_GreenRelease = m_Car.FindAction("Green-Release", throwIfNotFound: true);
        m_Car_White = m_Car.FindAction("White", throwIfNotFound: true);
        m_Car_White_Release = m_Car.FindAction("White_Release", throwIfNotFound: true);
        m_Car_Yellow = m_Car.FindAction("Yellow", throwIfNotFound: true);
        m_Car_Yellow_Release = m_Car.FindAction("Yellow_Release", throwIfNotFound: true);
        m_Car_Aim = m_Car.FindAction("Aim", throwIfNotFound: true);
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

    // Car
    private readonly InputActionMap m_Car;
    private List<ICarActions> m_CarActionsCallbackInterfaces = new List<ICarActions>();
    private readonly InputAction m_Car_Red_Press;
    private readonly InputAction m_Car_Red_Release;
    private readonly InputAction m_Car_Black_Press;
    private readonly InputAction m_Car_Black_Release;
    private readonly InputAction m_Car_Blue;
    private readonly InputAction m_Car_Blue_Release;
    private readonly InputAction m_Car_Green;
    private readonly InputAction m_Car_GreenRelease;
    private readonly InputAction m_Car_White;
    private readonly InputAction m_Car_White_Release;
    private readonly InputAction m_Car_Yellow;
    private readonly InputAction m_Car_Yellow_Release;
    private readonly InputAction m_Car_Aim;
    public struct CarActions
    {
        private @ButtonsInput m_Wrapper;
        public CarActions(@ButtonsInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Red_Press => m_Wrapper.m_Car_Red_Press;
        public InputAction @Red_Release => m_Wrapper.m_Car_Red_Release;
        public InputAction @Black_Press => m_Wrapper.m_Car_Black_Press;
        public InputAction @Black_Release => m_Wrapper.m_Car_Black_Release;
        public InputAction @Blue => m_Wrapper.m_Car_Blue;
        public InputAction @Blue_Release => m_Wrapper.m_Car_Blue_Release;
        public InputAction @Green => m_Wrapper.m_Car_Green;
        public InputAction @GreenRelease => m_Wrapper.m_Car_GreenRelease;
        public InputAction @White => m_Wrapper.m_Car_White;
        public InputAction @White_Release => m_Wrapper.m_Car_White_Release;
        public InputAction @Yellow => m_Wrapper.m_Car_Yellow;
        public InputAction @Yellow_Release => m_Wrapper.m_Car_Yellow_Release;
        public InputAction @Aim => m_Wrapper.m_Car_Aim;
        public InputActionMap Get() { return m_Wrapper.m_Car; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CarActions set) { return set.Get(); }
        public void AddCallbacks(ICarActions instance)
        {
            if (instance == null || m_Wrapper.m_CarActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CarActionsCallbackInterfaces.Add(instance);
            @Red_Press.started += instance.OnRed_Press;
            @Red_Press.performed += instance.OnRed_Press;
            @Red_Press.canceled += instance.OnRed_Press;
            @Red_Release.started += instance.OnRed_Release;
            @Red_Release.performed += instance.OnRed_Release;
            @Red_Release.canceled += instance.OnRed_Release;
            @Black_Press.started += instance.OnBlack_Press;
            @Black_Press.performed += instance.OnBlack_Press;
            @Black_Press.canceled += instance.OnBlack_Press;
            @Black_Release.started += instance.OnBlack_Release;
            @Black_Release.performed += instance.OnBlack_Release;
            @Black_Release.canceled += instance.OnBlack_Release;
            @Blue.started += instance.OnBlue;
            @Blue.performed += instance.OnBlue;
            @Blue.canceled += instance.OnBlue;
            @Blue_Release.started += instance.OnBlue_Release;
            @Blue_Release.performed += instance.OnBlue_Release;
            @Blue_Release.canceled += instance.OnBlue_Release;
            @Green.started += instance.OnGreen;
            @Green.performed += instance.OnGreen;
            @Green.canceled += instance.OnGreen;
            @GreenRelease.started += instance.OnGreenRelease;
            @GreenRelease.performed += instance.OnGreenRelease;
            @GreenRelease.canceled += instance.OnGreenRelease;
            @White.started += instance.OnWhite;
            @White.performed += instance.OnWhite;
            @White.canceled += instance.OnWhite;
            @White_Release.started += instance.OnWhite_Release;
            @White_Release.performed += instance.OnWhite_Release;
            @White_Release.canceled += instance.OnWhite_Release;
            @Yellow.started += instance.OnYellow;
            @Yellow.performed += instance.OnYellow;
            @Yellow.canceled += instance.OnYellow;
            @Yellow_Release.started += instance.OnYellow_Release;
            @Yellow_Release.performed += instance.OnYellow_Release;
            @Yellow_Release.canceled += instance.OnYellow_Release;
            @Aim.started += instance.OnAim;
            @Aim.performed += instance.OnAim;
            @Aim.canceled += instance.OnAim;
        }

        private void UnregisterCallbacks(ICarActions instance)
        {
            @Red_Press.started -= instance.OnRed_Press;
            @Red_Press.performed -= instance.OnRed_Press;
            @Red_Press.canceled -= instance.OnRed_Press;
            @Red_Release.started -= instance.OnRed_Release;
            @Red_Release.performed -= instance.OnRed_Release;
            @Red_Release.canceled -= instance.OnRed_Release;
            @Black_Press.started -= instance.OnBlack_Press;
            @Black_Press.performed -= instance.OnBlack_Press;
            @Black_Press.canceled -= instance.OnBlack_Press;
            @Black_Release.started -= instance.OnBlack_Release;
            @Black_Release.performed -= instance.OnBlack_Release;
            @Black_Release.canceled -= instance.OnBlack_Release;
            @Blue.started -= instance.OnBlue;
            @Blue.performed -= instance.OnBlue;
            @Blue.canceled -= instance.OnBlue;
            @Blue_Release.started -= instance.OnBlue_Release;
            @Blue_Release.performed -= instance.OnBlue_Release;
            @Blue_Release.canceled -= instance.OnBlue_Release;
            @Green.started -= instance.OnGreen;
            @Green.performed -= instance.OnGreen;
            @Green.canceled -= instance.OnGreen;
            @GreenRelease.started -= instance.OnGreenRelease;
            @GreenRelease.performed -= instance.OnGreenRelease;
            @GreenRelease.canceled -= instance.OnGreenRelease;
            @White.started -= instance.OnWhite;
            @White.performed -= instance.OnWhite;
            @White.canceled -= instance.OnWhite;
            @White_Release.started -= instance.OnWhite_Release;
            @White_Release.performed -= instance.OnWhite_Release;
            @White_Release.canceled -= instance.OnWhite_Release;
            @Yellow.started -= instance.OnYellow;
            @Yellow.performed -= instance.OnYellow;
            @Yellow.canceled -= instance.OnYellow;
            @Yellow_Release.started -= instance.OnYellow_Release;
            @Yellow_Release.performed -= instance.OnYellow_Release;
            @Yellow_Release.canceled -= instance.OnYellow_Release;
            @Aim.started -= instance.OnAim;
            @Aim.performed -= instance.OnAim;
            @Aim.canceled -= instance.OnAim;
        }

        public void RemoveCallbacks(ICarActions instance)
        {
            if (m_Wrapper.m_CarActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICarActions instance)
        {
            foreach (var item in m_Wrapper.m_CarActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CarActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CarActions @Car => new CarActions(this);
    public interface ICarActions
    {
        void OnRed_Press(InputAction.CallbackContext context);
        void OnRed_Release(InputAction.CallbackContext context);
        void OnBlack_Press(InputAction.CallbackContext context);
        void OnBlack_Release(InputAction.CallbackContext context);
        void OnBlue(InputAction.CallbackContext context);
        void OnBlue_Release(InputAction.CallbackContext context);
        void OnGreen(InputAction.CallbackContext context);
        void OnGreenRelease(InputAction.CallbackContext context);
        void OnWhite(InputAction.CallbackContext context);
        void OnWhite_Release(InputAction.CallbackContext context);
        void OnYellow(InputAction.CallbackContext context);
        void OnYellow_Release(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
    }
}
