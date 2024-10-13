using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceGirlDialogState : IChoiceDialogState
{
    private ChoiceDialogManager manager;
    private string dialog;

    public ChoiceGirlDialogState(ChoiceDialogManager manager, string dialog)
    {
        this.manager = manager;
        this.dialog = dialog;
    }

    public void Enter()
    {
        manager.SetDialogText("Girl: " + dialog);
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