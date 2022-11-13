using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLevelDestroyerCommand : ICommand
{
    private Transform _levelHolder;



    public OnLevelDestroyerCommand(Transform levelHolder)
    {
        _levelHolder = levelHolder;
    }
    
    
    public void Execute()
    {
        Object.Destroy(_levelHolder.GetChild(0).gameObject);
    }

    public void Execute(int value)
    { 

    }
}
