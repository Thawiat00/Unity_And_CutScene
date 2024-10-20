using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; // TextMeshPro ?? TMPro; ???
using static DialogManager_choice;
using static UnityEngine.GraphicsBuffer;

public class BreakChoiceLoader_dynamic : MonoBehaviour
{
    public BreakChoiceData_dynamic breakChoiceData; // JSON Data
  
    public Button choiceButton1; // Choice Button 1
    public Button choiceButton2; // Choice Button 2
    public Button choiceButton3; // Choice Button 3
    public Text dialogText; // UI Text for dialog (or TextMeshPro)

    private List<string> currentDialogOptions; // To store the current dialog options
    private int currentDialogIndex = 0; // To keep track of the current dialog index

    void Start()
    {
        // LoadBreakChoiceScene();
        SetupButtons(); // Set up the buttons
    }

    void LoadBreakChoiceScene()
    {
        TextAsset json = Resources.Load<TextAsset>("mock_break_choice_scene"); // ???? JSON
        if (json != null)
        {
            breakChoiceData = JsonUtility.FromJson<BreakChoiceData_dynamic>(json.text); // ???? JSON ??????????
            if (breakChoiceData.break_choice_scene.Count > 0)
            {
                Debug.Log("Break choice scenes loaded successfully.");
                LogDialogTopics(); // ??????????????????????????????? dialog_cs_OC
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

    void LogDialogTopics()
    {
       

        foreach (var scene in breakChoiceData.dialog_cs_OC)
        {
            foreach (var dialog in scene.cutscene_choice)
            {
                Debug.Log("Dialog Topic: " + dialog); // ?????????? dialog
            }
        }
    }


    void SetupButtons()
    {
        LoadDataJson();

        CheckDataForUseCurrent(); // ?????????????????????????????

        UseFirstDataCurrentForFirstLoadButton();

    }

    void UseFirstDataCurrentForFirstLoadButton()
    {

        // ???????????????????? breakChoiceData.dialog_cs_OC ???????
        if (breakChoiceData.dialog_cs_OC[0].cutscene_choice.Count > 0)
        {
            // var firstDialog = breakChoiceData.dialog_cs_OC[0]; // ????????????? dialog ??????
            var firstDialog = breakChoiceData.dialog_cs_OC[0].cutscene_choice[0]; // ????????????? dialog ??????

            Debug.Log("firstDialog :" + firstDialog);
          //  Debug.Log("firstDialog :" + firstDialog);

            // ?????????????????? break_choice_scene ??? firstDialog
            LinkDataSameKey(firstDialog);
        }
    }

    void LinkDataSameKey(string firstDialog)
    {


        // ??????? break_choice_scene

        //foreach breakChoiceData.break_choice_scene
        foreach (var scene in breakChoiceData.break_choice_scene)
        {
            
          
                if (scene.key.Contains(firstDialog) == true)
                {
                   

                    Debug.Log("scene.key.Contains(dialogChoice) have :" + firstDialog);

                    int index = breakChoiceData.break_choice_scene.FindIndex(scene => scene.key.Contains(firstDialog));

                Debug.Log("breakChoiceData.break_choice_scene.FindIndex(scene => scene.key.Contains(firstDialog)); :" + index);


                // ????????????? sceneData ????????? index
                var matchedScene = breakChoiceData.break_choice_scene[index];

                Debug.Log("breakChoiceData.break_choice_scene[index] :" + matchedScene.key.ToString());

             
                // scene.key[index] = scene.sceneData[index].option_call_all
                string buttonText1 = matchedScene.sceneData[0].option_call_all[0].option_call_option_1[0];
                string buttonText2 = matchedScene.sceneData[0].option_call_all[0].option_call_option_2[0];
                string buttonText3 = matchedScene.sceneData[0].option_call_all[0].option_call_option_3[0];

                Debug.Log("buttonText1 :" + buttonText1);
                Debug.Log("buttonText2 :" + buttonText2);
                Debug.Log("buttonText3 :" + buttonText3);


                // ??????????????????????????????????????
                choiceButton1.GetComponentInChildren<Text>().text = buttonText1;
                choiceButton2.GetComponentInChildren<Text>().text = buttonText2;
                choiceButton3.GetComponentInChildren<Text>().text = buttonText3;

                // ??? break ?????????
                break;
            }    
               

        }


    }


    void LinkDataSameKey2(string firstDialog)
    {
        find_sceneData_with_string();

        /*
        // Loop ???? break_choice_scene ??????????????????
        foreach (var scene in breakChoiceData.break_choice_scene)
        {
            var firstScene = scene.sceneData[0]; // ??????? sceneData ???

            // ??????????????????????????????? option_call_option_ ?? firstScene
            if (firstScene.option_call_all.Count > 0)
            {
                choiceButton1.onClick.AddListener(() => SelectOption(firstScene.option_call_all[0].option_call_option_1));
                choiceButton2.onClick.AddListener(() => SelectOption(firstScene.option_call_all[0].option_call_option_2));
                choiceButton3.onClick.AddListener(() => SelectOption(firstScene.option_call_all[0].option_call_option_3));
            }
        }
        */
    }

    private void find_sceneData_with_string()
    {
        throw new NotImplementedException();
    }

   


    void LoadDataJson()
    {
     // ???????? JSON ??? Resources
        TextAsset json = Resources.Load<TextAsset>("mock_break_choice_scene"); // ????????????????????????????
        if (json != null)
        {
            // ???? JSON ??????????????????????
            breakChoiceData = JsonUtility.FromJson<BreakChoiceData_dynamic>(json.text);

          

            if (breakChoiceData != null && breakChoiceData.break_choice_scene.Count > 0)
            {
                Debug.Log("load JSON successandhavedata break_choice_scene.");

                // ???????????????????????????????????????????????????
                // ???? ??????? label ???????, ????? event listener ???????
            }
         
            else
            {
               Debug.LogError("notfound data break_choice_scene in JSON.");
            }

        }

        else
        {
            Debug.LogError("cannot dowload JSON .");

        }



    }


    private void CheckDataForUseCurrent()
    {
        // ??????????????????????????????????????
        if (breakChoiceData != null && breakChoiceData.dialog_cs_OC[0].cutscene_choice.Count > 0)
        {
            foreach (string choice in breakChoiceData.dialog_cs_OC[0].cutscene_choice)
            {
                Debug.Log("data dialog_cs_OC .cutscene_choice ready : " + choice); // ??????????? choice
            }

           // Debug.Log("data dialog_cs_OC .cutscene_choice ready :" + breakChoiceData.dialog_cs_OC[0].cutscene_choice.na);
            // ???????????????????????? ????????????????????????????????????????
            // ???? ????????????? dialog ?????????? scene
        }

        else
        {
            Debug.LogError("data dialog_cs_OC .cutscene_choice cannot use or have error in dowload");
        }


    }




    void SelectOption(List<string> selectedOption)
    {
        currentDialogOptions = selectedOption; // Store selected option
        currentDialogIndex = 1; // Start from the second line of the selected option
        UpdateDialogText(currentDialogOptions[currentDialogIndex]); // Display the second line

        // Hide the choice buttons
        choiceButton1.gameObject.SetActive(false);
        choiceButton2.gameObject.SetActive(false);
        choiceButton3.gameObject.SetActive(false);
    }

    void UpdateDialogText(string text)
    {
        dialogText.text = text; // Update dialogText
    }

    void Update()
    {
        // Check for spacebar input
        if (Input.GetKeyDown(KeyCode.Space) && currentDialogOptions != null)
        {
            if (currentDialogIndex < currentDialogOptions.Count - 1) // Check if there are more lines
            {
                currentDialogIndex++; // Move to the next line
                UpdateDialogText(currentDialogOptions[currentDialogIndex]); // Display the next line
            }
        }
    }
}
