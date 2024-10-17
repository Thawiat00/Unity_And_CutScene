using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BreakChoiceData
{
    public List<BreakChoiceScene> break_choice_scene_1_1;
    public List<BreakChoiceScene> break_choice_scene_1_2;
    public List<DialogCSOC> dialog_cs_OC;
}

[System.Serializable]
public class BreakChoiceScene
{
    public List<OptionCallAll> option_call_all;
    public List<OptionCallAllAnswer> option_call_all_answer;
}

[System.Serializable]
public class OptionCallAll
{
    public List<string> option_call_option_1;
    public List<string> option_call_option_2;
    public List<string> option_call_option_3;
}

[System.Serializable]
public class OptionCallAllAnswer
{
    public List<string> option_answer_option_1;
    public List<string> option_answer_option_2;
    public List<string> option_answer_option_3;
}

[System.Serializable]
public class DialogCSOC
{
    public List<string> cutscene_choice;
    public string End_dialog;
}
