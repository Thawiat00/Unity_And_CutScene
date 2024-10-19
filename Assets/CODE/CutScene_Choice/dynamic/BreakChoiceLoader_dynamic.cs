using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ????????? TextMeshPro ??? TMPro; ???

public class BreakChoiceLoader_dynamic : MonoBehaviour
{
    public BreakChoiceData_dynamic breakChoiceData; // ?????? JSON ???????
    public Button choiceButton1; // ??????? 1
    public Button choiceButton2; // ??????? 2
    public Button choiceButton3; // ??????? 3
    public Text dialogText; // UI Text ???????????????? (??? TextMeshPro ???)

    void Start()
    {
        LoadBreakChoiceScene();
        SetupButtons(); // ????????????????????????
    }

    void LoadBreakChoiceScene()
    {
        TextAsset json = Resources.Load<TextAsset>("mock_break_choice_scene"); // ???????? JSON
        if (json != null)
        {
            breakChoiceData = JsonUtility.FromJson<BreakChoiceData_dynamic>(json.text); // ?????????? JSON
            if (breakChoiceData.break_choice_scene.Count > 0)
            {
                // ????????????????????????????
                Debug.Log("Break choice scenes loaded successfully.");
            }
            else
            {
                Debug.LogError("No break choice scenes available in data.");
            }
        }
        else
        {
            Debug.LogError("Failed to load JSON file.");
        }
    }

    void SetupButtons()
    {
        if (breakChoiceData.break_choice_scene.Count > 0)
        {
            var firstScene = breakChoiceData.break_choice_scene[0].sceneData[0]; // ??????? scene ???

            // ??????????????????
            choiceButton1.GetComponentInChildren<Text>().text = firstScene.option_call_all[0].option_call_option_1[0]; // ????????????????? 1
            choiceButton2.GetComponentInChildren<Text>().text = firstScene.option_call_all[0].option_call_option_2[0]; // ????????????????? 2
            choiceButton3.GetComponentInChildren<Text>().text = firstScene.option_call_all[0].option_call_option_3[0]; // ????????????????? 3

            // ????? listener ???????
            choiceButton1.onClick.AddListener(() => UpdateDialogText(firstScene.option_call_all[0].option_call_option_1[1]));
            choiceButton2.onClick.AddListener(() => UpdateDialogText(firstScene.option_call_all[0].option_call_option_2[1]));
            choiceButton3.onClick.AddListener(() => UpdateDialogText(firstScene.option_call_all[0].option_call_option_3[1]));
        }
    }

    void UpdateDialogText(string text)
    {
        dialogText.text = text; // ??????????????? dialogText
    }
}
