/****************************************************
* Unity版本：2019.4.11f1
* 文件：ScenarioManager.cs
* 作者：tottimctj
* 邮箱：tottimctj@163.com
* 日期：2021/07/02 14:52:17
* 功能：Nothing
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VNEngine;

public class ScenarioManager : Singleton<ScenarioManager> {
    private const string ScenarioRoot = "Scenario/";

    private ConversationManager cur;

    [SerializeField] private List<Button.ButtonClickedEvent> eventsHub;
    private string[] eventsValueList;

    [SerializeField] private Canvas dialogueCanvas;
    
    private List<ScenarioData> ScenarioList;
    
    
    private void Start() {
        LoadComplete();
        // ActiveDialogue(true);
        // PlayScenario("Test");
    }


    /**
     * 读取已结束的事件列表
     */
    public void LoadComplete() {
        ScenarioList = new List<ScenarioData>();
        // TODO 从游戏记录中获取记录

        #if true
        ScenarioList.Add(ScenarioData.Test(1));
        #endif
    }


    public void PlayScenario(string path) {
        if (cur != null) {
            // 先销毁
            cur.Finish_Conversation();
        }

        Load(path);

        ActiveDialogue(true);
        // cur.Start_Conversation();
    }

    void Load(string path) {
        // Read the file

        // System.IO.StreamReader file = new System.IO.StreamReader(path);

        var asset = Resources.Load(ScenarioRoot + path, typeof(TextAsset)) as TextAsset;

        string value = asset.text;

        //分割行
        string[] strLine = value.Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);

        // Create a child object to hold all elements created from this file
        Transform import_parent = (new GameObject(Path.GetFileNameWithoutExtension(path))).transform;
        ConversationManager cur_conversation = null;
        string line;
        for (int i = 0; i < strLine.Length; i++) {
            // Read it line by line
            line = strLine[i];
            // Continue if it's an empty line
            if (String.IsNullOrEmpty(line))
                continue;
            string[] split_line = line.Split(new char[] {':'}, 2);

            // Create a new conversation
            if (line.StartsWith("Conversation", true, System.Globalization.CultureInfo.InvariantCulture)) {
                GameObject go = new GameObject(split_line[1] + " Conversation");
                go.transform.parent = import_parent;
                ConversationManager new_conv = go.AddComponent<ConversationManager>();
                if (cur_conversation != null) {
                    cur_conversation.start_conversation_when_done = new_conv;
                }

                cur_conversation = new_conv;
            }
            else if (line.StartsWith("EnterActor", true, System.Globalization.CultureInfo.InvariantCulture)) {
                GameObject go = new GameObject("Enter " + split_line[1]);
                go.transform.parent = cur_conversation.transform;
                EnterActorNode node = go.AddComponent<EnterActorNode>();
                node.actor_name = split_line[1];
            }
            else if (line.StartsWith("ExitActor", true, System.Globalization.CultureInfo.InvariantCulture)) {
                GameObject go = new GameObject("Exit " + split_line[1]);
                go.transform.parent = cur_conversation.transform;
                ExitActorNode node = go.AddComponent<ExitActorNode>();
                node.actor_name = split_line[1];
            }
            else if (line.StartsWith("SetBackground", true, System.Globalization.CultureInfo.InvariantCulture)) {
                GameObject go = new GameObject("Set Background " + split_line[1]);
                go.transform.parent = cur_conversation.transform;
                SetBackground node = go.AddComponent<SetBackground>();
                try {
                    node.sprite = Resources.Load<Sprite>(split_line[1]);
                }
                catch (Exception e) {
                    Debug.Log("Error loading audio clip " + split_line[1] +
                              ". Make sure your named clip matches the resource. Ex: some_folder/cool_music");
                }
            }
            else if (line.StartsWith("SetMusic", true, System.Globalization.CultureInfo.InvariantCulture)) {
                GameObject go = new GameObject("Set Music " + split_line[1]);
                go.transform.parent = cur_conversation.transform;
                SetMusicNode node = go.AddComponent<SetMusicNode>();
                // If possible, load the necessary resource
                try {
                    node.new_music = Resources.Load<AudioClip>(split_line[1]);
                }
                catch (Exception e) {
                    Debug.Log("Error loading audio clip " + split_line[1] +
                              ". Make sure your named clip matches the resource. Ex: some_folder/cool_music");
                }
            }
            // ADD MORE HERE IF YOU WISH TO EXTEND THE IMPORTING FUNCTIONALITY
            else if (line.StartsWith("SetBranch", true, System.Globalization.CultureInfo.InvariantCulture)) {
                var attribute = split_line[1].Split('-');
                var branchList = attribute[1].Split('|');

                GameObject go = new GameObject("Set Branch " + split_line[1]);
                go.transform.parent = cur_conversation.transform;
                ChoiceNode node = go.AddComponent<ChoiceNode>();
                node.Name_Of_Choice = attribute[0];
                node.Number_Of_Choices = branchList.Length;
                node.Button_Text = new string[branchList.Length];
                eventsValueList = new string[branchList.Length];

                for (int ii = 0; ii < branchList.Length; ii++) {
                    var branchItem = branchList[ii].Split('=');
                    node.Button_Text[ii] = branchItem[0];
                    eventsValueList[ii] = branchItem[1];
                    node.Button_Events[ii] = eventsHub[ii];
                }
            }
            else if (line.StartsWith("Close", true, System.Globalization.CultureInfo.InvariantCulture)) {
                GameObject exitAllActorsNodego = new GameObject("ExitAllActorsNode");
                exitAllActorsNodego.transform.parent = cur_conversation.transform;
                exitAllActorsNodego.AddComponent<ExitAllActorsNode>();
                
                GameObject disableBackgroundNodeGo = new GameObject("DisableBackgroundNode");
                disableBackgroundNodeGo.transform.parent = cur_conversation.transform;
                disableBackgroundNodeGo.AddComponent<DisableBackgroundNode>();
                
                GameObject disableForegroundNodeGo = new GameObject("DisableForegroundNode");
                disableForegroundNodeGo.transform.parent = cur_conversation.transform;
                disableForegroundNodeGo.AddComponent<DisableForegroundNode>();
                
                GameObject hideShowUINodeGo = new GameObject("HideShowUINode");
                hideShowUINodeGo.transform.parent = cur_conversation.transform;
                hideShowUINodeGo.AddComponent<HideShowUINode>();

            }

            // Must be a line of dialogue
            else if (split_line.Length == 2) {
                GameObject go = new GameObject(split_line[0]);
                go.transform.parent = cur_conversation.transform;
                DialogueNode node = go.AddComponent<DialogueNode>();
                node.actor = split_line[0];
                node.textbox_title = split_line[0];
                node.text = split_line[1];
            }
        }


        VNSceneManager.current_conversation = cur_conversation;
        cur = cur_conversation;
    }


    void ActiveDialogue(bool flag) {
        dialogueCanvas.gameObject.SetActive(flag);
    }

    public void ChoiceEvent(int index) {
        print(eventsValueList[index]);

        cur.Start_Next_Node();
        // cur.Finish_Conversation();
        // VNSceneManager.current_conversation.Reset_Conversation();

        // PlayScenario("Test");
    }
}