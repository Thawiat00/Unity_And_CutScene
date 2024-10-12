using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlDialogState : IDialogState
{
    private DialogManager manager;
    private string dialog;

    public GirlDialogState(DialogManager manager, string dialog)
    {
        this.manager = manager;
        this.dialog = dialog;
    }

    public void Enter()
    {
        Debug.Log("Girl: " + dialog);
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