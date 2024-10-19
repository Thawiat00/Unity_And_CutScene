using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // TextMeshPro ?? TMPro; ???

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
        LoadBreakChoiceScene();
        SetupButtons(); // Set up the buttons
    }

    void LoadBreakChoiceScene()
    {
        TextAsset json = Resources.Load<TextAsset>("mock_break_choice_scene"); // Load JSON
        if (json != null)
        {
            breakChoiceData = JsonUtility.FromJson<BreakChoiceData_dynamic>(json.text); // Deserialize JSON
            if (breakChoiceData.break_choice_scene.Count > 0)
            {
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
            // Loop through all scenes (this can be customized)
            foreach (var scene in breakChoiceData.break_choice_scene)
            {
                var firstScene = scene.sceneData[0]; // Access the first scene of each BreakChoiceScene

                // Set button text for all choices
                choiceButton1.GetComponentInChildren<Text>().text = firstScene.option_call_all[0].option_call_option_1[0];
                choiceButton2.GetComponentInChildren<Text>().text = firstScene.option_call_all[0].option_call_option_2[0];
                choiceButton3.GetComponentInChildren<Text>().text = firstScene.option_call_all[0].option_call_option_3[0];

                // Add listeners for each button
                choiceButton1.onClick.AddListener(() => SelectOption(firstScene.option_call_all[0].option_call_option_1));
                choiceButton2.onClick.AddListener(() => SelectOption(firstScene.option_call_all[0].option_call_option_2));
                choiceButton3.onClick.AddListener(() => SelectOption(firstScene.option_call_all[0].option_call_option_3));
            }
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
