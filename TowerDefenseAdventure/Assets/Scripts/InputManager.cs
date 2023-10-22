using UnityEngine;

public class InputManager : MonoBehaviour
{
    public KeyBindings keyBindings;
    public KeyCode GetKeyFromAction(KeyBindingActions keyBindingAction)
    {
        foreach (var keyBinding in keyBindings.keyBindings)
        {
            if(keyBinding.keyBindingAction == keyBindingAction)
                return keyBinding.keyCode;
        }
        return KeyCode.None;
    }
    public bool GetKeyDown(KeyBindingActions keyBindingAction)
    {
        KeyCode keyCode = GetKeyFromAction(keyBindingAction);
        return Input.GetKeyDown(keyCode);
    }
    public bool GetKeyUp(KeyBindingActions keyBindingAction)
    {
        KeyCode keyCode = GetKeyFromAction(keyBindingAction);
        return Input.GetKeyUp(keyCode);
    }
    public bool GetKey(KeyBindingActions keyBindingAction)
    {
        KeyCode keyCode = GetKeyFromAction(keyBindingAction);
        return Input.GetKey(keyCode);
    }
}
