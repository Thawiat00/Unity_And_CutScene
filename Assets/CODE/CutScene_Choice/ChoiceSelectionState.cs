using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceSelectionState : IChoiceDialogState
{
    private ChoiceDialogManager manager;
    private string choiceKey;
    private List<ChoiceBreakChoiceScene> choices;
    private int currentChoiceIndex = 0;

    public ChoiceSelectionState(ChoiceDialogManager manager, string choiceKey, Dictionary<string, List<ChoiceBreakChoiceScene>> breakChoiceScenes)
    {
        this.manager = manager;
        this.choiceKey = choiceKey;
        if (breakChoiceScenes.TryGetValue(choiceKey, out List<ChoiceBreakChoiceScene> sceneChoices))
        {
            this.choices = sceneChoices;
        }
        else
        {
            this.choices = new List<ChoiceBreakChoiceScene>();
            Debug.LogError($"Choice not found: {choiceKey}");
        }
    }

    public void Enter()
    {
        if (choices.Count > 0)
        {
            ShowOptions();
        }
        else
        {
            manager.NextState();
        }
    }

    public void Update()
    {
        // Handle input for selecting options
    }

    public void Exit() { }

    private void ShowOptions()
    {
        if (currentChoiceIndex < choices.Count && choices[currentChoiceIndex].option_call_all.Count > 0)
        {
            var optionCall = choices[currentChoiceIndex].option_call_all[0];
            List<string> options = new List<string>();
            options.AddRange(optionCall.option_call_option_1);
            options.AddRange(optionCall.option_call_option_2);
            options.AddRange(optionCall.option_call_option_3);
            manager.ShowOptions(options);
        }
    }

    public void SelectOption(int optionIndex)
    {
        if (currentChoiceIndex < choices.Count && choices[currentChoiceIndex].option_call_all_answer.Count > 0)
        {
            var answerOptions = choices[currentChoiceIndex].option_call_all_answer[0];
            List<string> answers = new List<string>();
            answers.AddRange(answerOptions.option_answer_option_1);
            answers.AddRange(answerOptions.option_answer_option_2);
            answers.AddRange(answerOptions.option_answer_option_3);

            if (optionIndex < answers.Count)
            {
                manager.SetDialogText(answers[optionIndex]);
            }
        }
        currentChoiceIndex++;
        if (currentChoiceIndex >= choices.Count)
        {
            manager.NextState();
        }
        else
        {
            ShowOptions();
        }
    }
}