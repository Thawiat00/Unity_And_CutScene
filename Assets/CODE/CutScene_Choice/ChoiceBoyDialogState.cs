using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceBoyDialogState : IChoiceDialogState
{
    private ChoiceDialogManager manager;
    private string dialog;

    public ChoiceBoyDialogState(ChoiceDialogManager manager, string dialog)
    {
        this.manager = manager;
        this.dialog = dialog;
    }

    public void Enter()
    {
        manager.SetDialogText("Boy: " + dialog);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            manager.NextState();
        }
    }

    public void Exit() { }
}