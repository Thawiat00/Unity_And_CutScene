using System;
using System.Collections.Generic;

[Serializable]
public class ChoiceDialogOption
{
    public List<string> option_call_option_1;
    public List<string> option_call_option_2;
    public List<string> option_call_option_3;
}

[Serializable]
public class ChoiceDialogOptionAnswer
{
    public List<string> option_answer_option_1;
    public List<string> option_answer_option_2;
    public List<string> option_answer_option_3;
}

[Serializable]
public class ChoiceBreakChoiceScene
{
    public List<ChoiceDialogOption> option_call_all;
    public List<ChoiceDialogOptionAnswer> option_call_all_answer;
}

[Serializable]
public class ChoiceDialogCsOC
{
    public string girl;
    public string boy;
    public List<string> cutscene_choice;
    public string End_dialog;
}

[Serializable]
public class ChoiceDialogContainer
{
    public Dictionary<string, List<ChoiceBreakChoiceScene>> break_choice_scenes;
    public List<ChoiceDialogCsOC> dialog_cs_OC;
}