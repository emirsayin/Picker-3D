using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using UnityEngine.Events;

public class InputSignal : MonoSingleton<InputSignal>
{
    public UnityAction onDisableInput = delegate { };
    public UnityAction onEnableInput = delegate { };
    public UnityAction onFirstTimeTouchTaken = delegate { };
    public UnityAction onInputTaken = delegate { };
    public UnityAction onInputReleased = delegate { };
  
}
