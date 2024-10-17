using System.Collections.Generic;
using UnityEngine;

public class BreakChoiceBlueprintChoice : MonoBehaviour
{
    void Start()
    {
        // ???????? JSON
        TextAsset jsonFile = Resources.Load<TextAsset>("mock_break_choice_scene");

        if (jsonFile == null)
        {
            Debug.LogError("????????? JSON ????????????????????? Resources.");
            return;
        }

        // ???? JSON ??????? Dictionary
        try
        {
            Dictionary<string, object> jsonData = MiniJSON.Json.Deserialize(jsonFile.text) as Dictionary<string, object>;

            if (jsonData == null)
            {
                Debug.LogError("????????????? JSON ???.");
                return;
            }

            // ???????????? key 'break_choice_scene_1_1' ???????
            if (jsonData.ContainsKey("break_choice_scene_1_1"))
            {
                var breakChoiceScene = jsonData["break_choice_scene_1_1"] as List<object>;

                if (breakChoiceScene != null && breakChoiceScene.Count > 0)
                {
                    var firstScene = breakChoiceScene[0] as Dictionary<string, object>;

                    // ???????????? key 'option_call_all' ???????
                    if (firstScene != null && firstScene.ContainsKey("option_call_all"))
                    {
                        var optionCallAll = firstScene["option_call_all"] as List<object>;

                        // ???????????????????? option_call_all ?? Debug
                        Debug.Log("option_call_all ?????????????:");
                        foreach (var option in optionCallAll)
                        {
                            var optionData = option as Dictionary<string, object>;

                            foreach (var key in optionData.Keys)
                            {
                                var optionValues = optionData[key] as List<object>;
                                Debug.Log(key + ": " + string.Join(", ", optionValues));
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("????? key 'option_call_all' ?? break_choice_scene_1_1.");
                    }
                }
            }
            else
            {
                Debug.LogError("????? key 'break_choice_scene_1_1' ?? JSON.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("??????? JSON ??????????????: " + e.Message);
        }
    }
}
