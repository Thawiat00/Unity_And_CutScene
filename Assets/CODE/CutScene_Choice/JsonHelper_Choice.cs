using System.Collections.Generic;
using System;
using UnityEngine;

// Debug helper class
public static class JsonHelper_Choice
{
    public static void DebugLogJson(string json)
    {
        Debug.Log("Trying to parse JSON: " + json);
    }
}

[Serializable]
public class DialogData_Choice
{
    public List<BreakChoiceScene_Choice> break_choice_scene_1_1;
    public List<BreakChoiceScene_Choice> break_choice_scene_1_2;
    public List<DialogScene_Choice> dialog_cs_OC;
}

[Serializable]
public class BreakChoiceScene_Choice
{
    public List<OptionCallAll_Choice> option_call_all;
    public List<OptionCallAllAnswer_Choice> option_call_all_answer;
}

[Serializable]
public class OptionCallAll_Choice
{
    public List<string> option_call_option_1;
    public List<string> option_call_option_2;
    public List<string> option_call_option_3;
}

[Serializable]
public class OptionCallAllAnswer_Choice
{
    public List<string> option_answer_option_1;
    public List<string> option_answer_option_2;
    public List<string> option_answer_option_3;
}

[Serializable]
public class DialogScene_Choice
{
    public string girl;
    public string boy;
    public List<string> cutscene_choice;
    public string End_dialog;
}