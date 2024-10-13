using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChoiceDialogState
{
    void Enter();
    void Update();
    void Exit();
}