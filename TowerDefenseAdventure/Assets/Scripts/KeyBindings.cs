using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "KeyBindings", menuName = "KeyBindings", order = 0), Serializable]
public class KeyBindings : ScriptableObject {
    [Serializable]
    public class KeyBinding
    {
        public KeyBindingActions keyBindingAction;
        public KeyCode keyCode;
    }
    public KeyBinding[] keyBindings;

    public void Init(KeyBinding[] keyBindings)
    {
        this.keyBindings = keyBindings;
    }

    public static KeyBindings CreateInstance(KeyBinding[] keyBindings)
    {
        KeyBindings _keyBindings = CreateInstance<KeyBindings>();
        _keyBindings.Init(keyBindings);
        return _keyBindings;
    }
}