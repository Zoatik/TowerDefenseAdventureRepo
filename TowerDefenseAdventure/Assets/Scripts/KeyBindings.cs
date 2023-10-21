using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "KeyBindings", menuName = "KeyBindings", order = 0)]
public class KeyBindings : ScriptableObject {
    [Serializable]
    public class KeyBinding
    {
        public KeyBindingActions keyBindingAction;
        public KeyCode keyCode;
    }
    public KeyBinding[] keyBindings;
}