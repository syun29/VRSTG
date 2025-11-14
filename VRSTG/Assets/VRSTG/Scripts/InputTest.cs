using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    private InputAction m_inputA;
    private InputAction m_inputB;
    private InputAction m_inputX;
    private InputAction m_inputY;
    private InputAction m_inputMenu;
    private InputAction m_inputThumbL;
    private InputAction m_inputThumbR;
    private InputAction m_inputTriggerBtnL;
    private InputAction m_inputTriggerBtnR;
    private InputAction m_inputGripBtnL;
    private InputAction m_inputGripBtnR;
    private InputAction m_inputTriggerL;
    private InputAction m_inputTriggerR;
    private InputAction m_inputGripL;
    private InputAction m_inputGripR;
    private InputAction m_inputStickL;
    private InputAction m_inputStickR;

    private void Awake()
    {
        PlayerInput input = GetComponent<PlayerInput>();
        input.currentActionMap.Enable();

        m_inputA = input.currentActionMap.FindAction("A");
        m_inputB = input.currentActionMap.FindAction("B");
        m_inputX = input.currentActionMap.FindAction("X");
        m_inputY = input.currentActionMap.FindAction("Y");
        m_inputMenu = input.currentActionMap.FindAction("Menu");
        m_inputThumbL = input.currentActionMap.FindAction("ThumbL");
        m_inputThumbR = input.currentActionMap.FindAction("ThumbR");
        m_inputTriggerBtnL = input.currentActionMap.FindAction("TriggerButtonL");
        m_inputTriggerBtnR = input.currentActionMap.FindAction("TriggerButtonR");
        m_inputGripBtnL = input.currentActionMap.FindAction("GripButtonL");
        m_inputGripBtnR = input.currentActionMap.FindAction("GripButtonR");

        m_inputTriggerL = input.currentActionMap.FindAction("TriggerL");
        m_inputTriggerR = input.currentActionMap.FindAction("TriggerR");
        m_inputGripL = input.currentActionMap.FindAction("GripL");
        m_inputGripR = input.currentActionMap.FindAction("GripR");

        m_inputStickL = input.currentActionMap.FindAction("StickL");
        m_inputStickR = input.currentActionMap.FindAction("StickR");
    }

    private void Update()
    {
        DebugCanVas.Print
        (
           "A:{0} | B:{1} | X:{2} | Y:{3} | M:{4}",
           m_inputA.IsPressed(),
           m_inputB.IsPressed(),
           m_inputX.IsPressed(),
           m_inputY.IsPressed(),
           m_inputMenu.IsPressed()
        );

        DebugCanVas.Print
        (
           "Thumb [L:{0} | R:{1}] Grip[ L:{2} | R:{3}] Trigger[ L:{4} | R:{5}]",
           m_inputThumbL.IsPressed(),
           m_inputThumbR.IsPressed(),
           m_inputGripBtnL.IsPressed(),
           m_inputGripBtnR.IsPressed(),
           m_inputTriggerBtnL.IsPressed(),
           m_inputTriggerBtnR.IsPressed()
        );

        DebugCanVas.Print
        (
           "Grip [L:{0} | R:{1}] Trigger[ L:{2} | R:{3}]",
          m_inputGripL.ReadValue<float>(),
          m_inputGripR.ReadValue<float>(),
          m_inputTriggerL.ReadValue<float>(),
          m_inputTriggerR.ReadValue<float>()
        );

        DebugCanVas.Print
       (
          "StickL:{0} | StickR:{1}",
         m_inputStickL.ReadValue<Vector2>(),
         m_inputStickR.ReadValue<Vector2>()
       );
    }
}
