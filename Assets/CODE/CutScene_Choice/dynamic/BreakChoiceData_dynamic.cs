using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BreakChoiceData_dynamic
{
    public List<BreakChoiceScene_dynamic> break_choice_scene; // List ?????? break_choice_scene
    public List<DialogData_dynamic> dialog_cs_OC; // List ?????? dialog data
}

[System.Serializable]
public class BreakChoiceScene_dynamic
{
    public string key; // ???? scene (???? break_choice_scene_1)
    public List<SceneData_dynamic> sceneData; // List ?????? scene data
}

[System.Serializable]
public class SceneData_dynamic
{
    public List<OptionCallAll_dynamic> option_call_all;
    public List<OptionCallAllAnswer_dynamic> option_call_all_answer;
}

[System.Serializable]
public class OptionCallAll_dynamic
{
    public List<string> option_call_option_1;
    public List<string> option_call_option_2;
    public List<string> option_call_option_3;
}

[System.Serializable]
public class OptionCallAllAnswer_dynamic
{
    public List<string> option_answer_option_1;
    public List<string> option_answer_option_2;
    public List<string> option_answer_option_3;
}

[System.Serializable]
public class DialogData_dynamic
{
    public List<string> cutscene_choice; // ?????? choices ??? scene
    public string End_dialog;
}
