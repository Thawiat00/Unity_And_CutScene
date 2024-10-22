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
    [SerializeField]
    bool isdebug;
    [SerializeField]
    bool isdebug_load_index_cutscene_choic;

    [SerializeField]
    bool isdebug_button_click;

    [SerializeField]
    bool isdebug_spacebar_work;


    public BreakChoiceData_dynamic breakChoiceData; // JSON Data

    public enum button_choice_status
    {
        button_display,
        button_turnoff
    }

    public enum status_spacebar
    {
        spacebar_no_active,
        spacebar_working
    }

    // ?????????????????? GameState
    button_choice_status button_currentState;

    status_spacebar current_status_Spacebar;

    //set_current_list_display_to_dialog_text;
    public List<string> display_current_list_dialog;

    public List<string> keep_list_button1;
    public List<string> keep_list_button2;
    public List<string> keep_list_button3;


    

    public Button choiceButton1; // Choice Button 1
    public Button choiceButton2; // Choice Button 2
    public Button choiceButton3; // Choice Button 3
    public Text dialogText; // UI Text for dialog (or TextMeshPro)


    // 0. To store the current dialog options
    private List<string> currentDialogOptions; 


   
   // private int currentDialogIndex = 0; // To keep track of the current dialog index


    //1.have string save current index dialog
    [SerializeField]
    private int Text_current_Index ; // To keep track of the current dialog index start array[2]



    //2.have string save current index_order_dialog
    [SerializeField]
    private int index_order_dialog; // ??????????????????????????

    //3. make private int index_order_dialog to save data on method load order index in  breakChoiceData.dialog_cs_OC[0].cutscene_choice


    private void Update()
    {
        working_on_spacebar();
    }

    void working_on_spacebar()
    {
        // ?????????????????????? Spacebar ???????
        if (Input.GetKeyDown(KeyCode.Space) && current_status_Spacebar == status_spacebar.spacebar_working)
        {
            // ??????????????????????????? Spacebar
            if(isdebug_spacebar_work == true)
            {
                Debug.Log("spacebar work");


                Debug.Log("display_current_list_dialog[Text_current_Index] :" + display_current_list_dialog[Text_current_Index]);
                //Debug.Log("working +1 index on dialog in text :" + keep_data_button[Text_current_Index];);
            }

            dialogText.text = display_current_list_dialog[Text_current_Index];


            //  check_current_plus_index_on_spacebar();


            if (Text_current_Index + 1 >= display_current_list_dialog.Count)
            {
                if (isdebug_load_index_cutscene_choic == true)
                Debug.LogWarning("breakChoiceData.dialog_cs_OC[0].cutscene_choice[index_order_dialog] is more array have");

                return;
            }
            else
            {

                check_current_plus_index_on_spacebar();
            }

        }
    }

    private void check_current_plus_index_on_spacebar()
    {
      

        if (Text_current_Index <= display_current_list_dialog.Count)
        {
            if (isdebug_spacebar_work == true)
            Debug.Log("+1  Text_current_Index");

            Text_current_Index += 1;
        }
        else
        {
           
            return;
        }
     



    }

    void Start()
    {
        button_currentState = button_choice_status.button_display;
        current_status_Spacebar = status_spacebar.spacebar_no_active;

        LoadDataJson();

        load_variable_to_use();
        //  Load_variable_use();

        // load_index_cutscene_choice();

        // LoadBreakChoiceScene();
        SetupButtons(); // Set up the buttons

      


        SetupButton_listiner();
       
    }

   

    private void SetupButton_listiner()
    {
        SetupButtonWithDebug(display_current_list_dialog,keep_list_button1, "keep_list_button1", choiceButton1, "Button 1");
        SetupButtonWithDebug(display_current_list_dialog,keep_list_button2, "keep_list_button2", choiceButton2, "Button 2");
        SetupButtonWithDebug(display_current_list_dialog,keep_list_button3, "keep_list_button3", choiceButton3, "Button 3");
    }

    void SetupButtonWithDebug(List<string> keep_display_list_dialog_text ,List<string> keep_data_button,string data_button_name ,Button button, string buttonName)
    {
        button.onClick.AddListener(() =>
        {
            //if click have to recived button on click
            // Debug.Log("recived button :" Button.onClick.AddListener().ToSafeString());

            if (isdebug_button_click == true)
            Debug.Log(buttonName + " Clicked");

            //button is select on click link data 1,2,3
            if (isdebug_button_click == true) 
            Debug.Log( "link_data :" + data_button_name);


            //if click button have to show data index 1 (position 2 in real ) on debug
            if (isdebug_button_click == true)
            Debug.Log(" keep_data_button[Text_current_Index]:" + keep_data_button[Text_current_Index]);

            string recived_Text = keep_data_button[Text_current_Index];



            //
            set_current_main_group_data_display(keep_data_button);


            
            // Debug.Log("keep_display_list_dialog_text[Text_current_Index] : " + keep_display_list_dialog_text[Text_current_Index]);
            // dialogText.text = keep_display_list_dialog_text[Text_current_Index];





            //setactive_all_button when
            set_turnoff_active_all_button();



            plus_1_on_current_index();
            // if (isdebug_button_click == true)
            //Debug.Log("data index 1")
        });
    }

    private void plus_1_on_current_index()
    {
        Text_current_Index += 1;
    }

    private void set_current_main_group_data_display(List<string> set_group_display_data)
    {
        display_current_list_dialog = set_group_display_data;
       // display_current_list_dialog.Add()
       

        if (isdebug_button_click == true)
            Debug.Log("keep_display_list_dialog_text.Count : " +set_group_display_data.Count);

        // Debug.Log("keep_display_list_dialog_text[Text_current_Index] : " + keep_display_list_dialog_text[Text_current_Index]);
         dialogText.text = set_group_display_data[Text_current_Index];



    }

    void set_working_status_button()
    {
        //make status enum

        //if button active = true
        //can click button can not use spacebar move dialog

        //if button active = false
        //can use spacebar move dialog but can not click buuton
    }


    private void set_turnON_active_all_button()
    {
        if(button_currentState == button_choice_status.button_turnoff)
        {
            if (isdebug_button_click == true)
            Debug.Log("set_turnON_active_all_button() is working");

            choiceButton1.gameObject.SetActive(true);
            choiceButton2.gameObject.SetActive(true);
            choiceButton3.gameObject.SetActive(true);

            button_currentState = button_choice_status.button_display;

            current_status_Spacebar = status_spacebar.spacebar_no_active;
        }

       
    }

    private void set_turnoff_active_all_button()
    {

        if (button_currentState == button_choice_status.button_display)
        {
            if (isdebug_button_click == true)
            Debug.Log("set_turnoff_active_all_button() is working");

            choiceButton1.gameObject.SetActive(false);
            choiceButton2.gameObject.SetActive(false);
            choiceButton3.gameObject.SetActive(false);
            //this.gameObject.
            // choiceButton1.

            //button enum status
            button_currentState = button_choice_status.button_turnoff;

            //spacebar enum status
            current_status_Spacebar = status_spacebar.spacebar_working;
        }
    }

    /*
    private void Load_variable_use()
    {
        //foreach breakChoiceData.break_choice_scene
        foreach (var scene in breakChoiceData.break_choice_scene)
        {


            if (scene.key.Contains(firstDialog) == true)
            {

                if (isdebug == true)
                    Debug.Log("scene.key.Contains(dialogChoice) have :" + firstDialog);

                int index = breakChoiceData.break_choice_scene.FindIndex(scene => scene.key.Contains(firstDialog));

                if (isdebug == true)
                    Debug.Log("breakChoiceData.break_choice_scene.FindIndex(scene => scene.key.Contains(firstDialog)); :" + index);


                // ????????????? sceneData ????????? index
                var matchedScene = breakChoiceData.break_choice_scene[index];

                if (isdebug == true)
                    Debug.Log("breakChoiceData.break_choice_scene[index] :" + matchedScene.key.ToString());


                // scene.key[index] = scene.sceneData[index].option_call_all
                string buttonText1 = matchedScene.sceneData[0].option_call_all[0].option_call_option_1[0];
                string buttonText2 = matchedScene.sceneData[0].option_call_all[0].option_call_option_2[0];
                string buttonText3 = matchedScene.sceneData[0].option_call_all[0].option_call_option_3[0];


                keep_list_button1 = matchedScene.sceneData[0].option_call_all[0].option_call_option_1;
                keep_list_button2 = matchedScene.sceneData[0].option_call_all[0].option_call_option_2;
                keep_list_button3 = matchedScene.sceneData[0].option_call_all[0].option_call_option_3;
                // List<string> keep_list_button1
                //List <keep list button1>

                if (isdebug == true)
                {
                    Debug.Log("buttonText1 :" + buttonText1);
                    Debug.Log("buttonText2 :" + buttonText2);
                    Debug.Log("buttonText3 :" + buttonText3);

                }


                // ??????????????????????????????????????
                choiceButton1.GetComponentInChildren<Text>().text = buttonText1;
                choiceButton2.GetComponentInChildren<Text>().text = buttonText2;
                choiceButton3.GetComponentInChildren<Text>().text = buttonText3;

                // ??? break ?????????
                break;
            }


        }
    }

    */

    //for button_click
    public  void FOR_BUTTON_load_index_cutscene_choice()
    {

        for_do_check_currentDialogIndex();

        // for_do_check_index_order_dialog();

       // for_do_plus_currentDialogIndex_first_and_then_plus_index_order_dialog()
    }

   

    void for_do_check_currentDialogIndex()
    {
        //current_Index_on_text = 1;

        //if click have to recived button on click
        // Debug.Log("recived button :" Button.onClick.AddListener().ToSafeString());

        //if click button have to show index 1 (position 2 in real ) on debug
        // Debug.Log("")

        //for test number only no do workshop
        /*
        if (current_Index_on_text <= breakChoiceData.dialog_cs_OC[0].cutscene_choice.Count)
        {
            if (isdebug_load_index_cutscene_choic == true)
                Debug.Log(" breakChoiceData.dialog_cs_OC[0].cutscene_choice :" + breakChoiceData.dialog_cs_OC[0].cutscene_choice[index_order_dialog]);
        }
        */


        /*
        if (index_order_dialog + 1 >= breakChoiceData.dialog_cs_OC[0].cutscene_choice.Count)
        {
            if (isdebug_load_index_cutscene_choic == true)
                Debug.LogWarning("breakChoiceData.dialog_cs_OC[0].cutscene_choice[index_order_dialog] is more array have");
            return;
        }
        */
    }

    private void for_do_plus_currentDialogIndex_first_and_then_plus_index_order_dialog()
    {

    }

    void for_do_check_index_order_dialog()
    {
        //for test number only no do workshop
        if (index_order_dialog <= breakChoiceData.dialog_cs_OC[0].cutscene_choice.Count)
        {
            if (isdebug_load_index_cutscene_choic == true)
                Debug.Log(" breakChoiceData.dialog_cs_OC[0].cutscene_choice :" + breakChoiceData.dialog_cs_OC[0].cutscene_choice[index_order_dialog]);
        }


        if (index_order_dialog + 1 >= breakChoiceData.dialog_cs_OC[0].cutscene_choice.Count)
        {
            if (isdebug_load_index_cutscene_choic == true)
                Debug.LogWarning("breakChoiceData.dialog_cs_OC[0].cutscene_choice[index_order_dialog] is more array have");
            return;
        }
        else
        {

            check_count_index_more();
        }
    }

    private void check_count_index_more()
    {
     
       
        if (index_order_dialog < breakChoiceData.dialog_cs_OC[0].cutscene_choice.Count)
        {
            if (isdebug_load_index_cutscene_choic == true)
                Debug.Log("breakChoiceData.dialog_cs_OC[0].cutscene_choice[index_order_dialog] is have same array");
         
  
            index_order_dialog += 1;
        }
    }

  





    void SetupButtons()
    {
       


        UseFirstDataCurrentForFirstLoadButton();

       // ClickForLoadDataUseSecondArrayOnButtonSelect();
    }


  


    void UseFirstDataCurrentForFirstLoadButton()
    {

       
        Get_text_button();
    }

    private void load_variable_to_use()
    {
        method_check_load_variable();

        method_working_load_variable();
     
    }

    private void method_check_load_variable()
    {

        CheckDataForUseCurrent(); // ?????????????????????????????
    }

    private void method_working_load_variable()
    {
        // ???????????????????? breakChoiceData.dialog_cs_OC ???????
        if (breakChoiceData.dialog_cs_OC[0].cutscene_choice.Count > 0)
        {
            // var firstDialog = breakChoiceData.dialog_cs_OC[0]; // ????????????? dialog ??????
            var firstDialog = breakChoiceData.dialog_cs_OC[0].cutscene_choice[0]; // ????????????? dialog ??????

            if (isdebug == true)
                Debug.Log("firstDialog :" + firstDialog);
            //  Debug.Log("firstDialog :" + firstDialog);

            // ?????????????????? break_choice_scene ??? firstDialog
            LinkDataSameKey(firstDialog);
        }
    }

    /*
    void LinkDataSameKey(string firstDialog)
    {


        // ??????? break_choice_scene

        //foreach breakChoiceData.break_choice_scene
        foreach (var scene in breakChoiceData.break_choice_scene)
        {
            
          
                if (scene.key.Contains(firstDialog) == true)
                {

                if (isdebug == true)
                    Debug.Log("scene.key.Contains(dialogChoice) have :" + firstDialog);

                    int index = breakChoiceData.break_choice_scene.FindIndex(scene => scene.key.Contains(firstDialog));

                if (isdebug == true)
                    Debug.Log("breakChoiceData.break_choice_scene.FindIndex(scene => scene.key.Contains(firstDialog)); :" + index);


                // ????????????? sceneData ????????? index
                var matchedScene = breakChoiceData.break_choice_scene[index];

                if (isdebug == true)
                    Debug.Log("breakChoiceData.break_choice_scene[index] :" + matchedScene.key.ToString());

             
                // scene.key[index] = scene.sceneData[index].option_call_all
                string buttonText1 = matchedScene.sceneData[0].option_call_all[0].option_call_option_1[0];
                string buttonText2 = matchedScene.sceneData[0].option_call_all[0].option_call_option_2[0];
                string buttonText3 = matchedScene.sceneData[0].option_call_all[0].option_call_option_3[0];


                keep_list_button1 = matchedScene.sceneData[0].option_call_all[0].option_call_option_1;
                keep_list_button2 = matchedScene.sceneData[0].option_call_all[0].option_call_option_2;
                keep_list_button3 = matchedScene.sceneData[0].option_call_all[0].option_call_option_3;
                // List<string> keep_list_button1
                //List <keep list button1>

                if (isdebug == true)
                {
                    Debug.Log("buttonText1 :" + buttonText1);
                    Debug.Log("buttonText2 :" + buttonText2);
                    Debug.Log("buttonText3 :" + buttonText3);

                }


                // ??????????????????????????????????????
                choiceButton1.GetComponentInChildren<Text>().text = buttonText1;
                choiceButton2.GetComponentInChildren<Text>().text = buttonText2;
                choiceButton3.GetComponentInChildren<Text>().text = buttonText3;

                // ??? break ?????????
                break;
            }    
               

        }


    }
    */



    void LinkDataSameKey(string firstDialog)
   {



        load_variable(firstDialog);


     
   }

    private void load_variable(string firstDialog)
    {
        //foreach breakChoiceData.break_choice_scene
        foreach (var scene in breakChoiceData.break_choice_scene)
        {


            if (scene.key.Contains(firstDialog) == true)
            {

                if (isdebug == true)
                    Debug.Log("scene.key.Contains(dialogChoice) have :" + firstDialog);

                int index = breakChoiceData.break_choice_scene.FindIndex(scene => scene.key.Contains(firstDialog));

                if (isdebug == true)
                    Debug.Log("breakChoiceData.break_choice_scene.FindIndex(scene => scene.key.Contains(firstDialog)); :" + index);


                // ????????????? sceneData ????????? index
                var matchedScene = breakChoiceData.break_choice_scene[index];

                if (isdebug == true)
                    Debug.Log("breakChoiceData.break_choice_scene[index] :" + matchedScene.key.ToString());


                // scene.key[index] = scene.sceneData[index].option_call_all

                /*
                string buttonText1 = matchedScene.sceneData[0].option_call_all[0].option_call_option_1[0];
                string buttonText2 = matchedScene.sceneData[0].option_call_all[0].option_call_option_2[0];
                string buttonText3 = matchedScene.sceneData[0].option_call_all[0].option_call_option_3[0];
                */

                keep_list_button1 = matchedScene.sceneData[0].option_call_all[0].option_call_option_1;

                //add answer data dialog 
                keep_list_button1.AddRange(matchedScene.sceneData[0].option_call_all_answer[0].option_answer_option_1);


                keep_list_button2 = matchedScene.sceneData[0].option_call_all[0].option_call_option_2;
                //add answer data dialog 
                keep_list_button2.AddRange(matchedScene.sceneData[0].option_call_all_answer[0].option_answer_option_2);



                keep_list_button3 = matchedScene.sceneData[0].option_call_all[0].option_call_option_3;
                //add answer data dialog 
                keep_list_button3.AddRange(matchedScene.sceneData[0].option_call_all_answer[0].option_answer_option_3);
                // List<string> keep_list_button1
                //List <keep list button1>

                if (isdebug == true)
                {
                    /*
                   Debug.Log("buttonText1 :" + buttonText1);
                   Debug.Log("buttonText2 :" + buttonText2);
                   Debug.Log("buttonText3 :" + buttonText3);
                    */
                    Debug.Log("buttonText1 :" + keep_list_button1[0]);
                    Debug.Log("buttonText1 :" + keep_list_button2[0]);
                    Debug.Log("buttonText1 :" + keep_list_button3[0]);
                }


                //Get_text_button();
                // ??????????????????????????????????????


                // ??? break ?????????
                break;
            }


        }
    }

    private void Get_text_button()
    {
     
       // if()
      //  choiceButton1.gameObject


        choiceButton1.GetComponentInChildren<Text>().text = keep_list_button1[0];
        choiceButton2.GetComponentInChildren<Text>().text = keep_list_button2[0];
        choiceButton3.GetComponentInChildren<Text>().text = keep_list_button3[0];

        if (isdebug == true)
        {

            Debug.Log("going to keep text button :" +choiceButton1.GetComponentInChildren<Text>().text.ToString());
            Debug.Log("going to keep text button :" + choiceButton2.GetComponentInChildren<Text>().text.ToString());
            Debug.Log("going to keep text button :" + choiceButton3.GetComponentInChildren<Text>().text.ToString());

        }
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
                if (isdebug == true)
                    Debug.Log("load JSON successandhavedata break_choice_scene.");

                // ???????????????????????????????????????????????????
                // ???? ??????? label ???????, ????? event listener ???????
            }
         
            else
            {
                if (isdebug == true)
                    Debug.LogError("notfound data break_choice_scene in JSON.");
            }

        }

        else
        {
            if (isdebug == true)
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
                if (isdebug == true)
                    Debug.Log("data dialog_cs_OC .cutscene_choice ready : " + choice); // ??????????? choice
            }

           // Debug.Log("data dialog_cs_OC .cutscene_choice ready :" + breakChoiceData.dialog_cs_OC[0].cutscene_choice.na);
            // ???????????????????????? ????????????????????????????????????????
            // ???? ????????????? dialog ?????????? scene
        }

        else
        {
            if (isdebug == true)
                Debug.LogError("data dialog_cs_OC .cutscene_choice cannot use or have error in dowload");
        }


    }



    /*
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

    */
}
