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

    public new Button.ButtonClickedEvent event1;


    private void Start() {
        Play();
    }


    public void Play() {

        if (cur != null) {
            // 先销毁
            Destroy(cur.transform.parent);
        }
        
        Load("Test");
        cur.Start_Conversation();
    }

    void Load(string path) {
        // Read the file

        // System.IO.StreamReader file = new System.IO.StreamReader(path);

        var asset = Resources.Load(ScenarioRoot + path, typeof(TextAsset)) as TextAsset;

        string value = asset.text;

        //分割行
        string[] strLine = value.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

        // Create a child object to hold all elements created from this file
        Transform import_parent = (new GameObject(Path.GetFileNameWithoutExtension(path))).transform;
        ConversationManager cur_conversation = null;
        string line;
        for (int i = 0; i < strLine.Length - 1; i++) {
            
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
                var branchList = split_line[1].Split(',');
                GameObject go = new GameObject("Set Branch " + split_line[1]);
                go.transform.parent = cur_conversation.transform;
                ChoiceNode node = go.AddComponent<ChoiceNode>();
                node.Name_Of_Choice = "见冬马";
                node.Number_Of_Choices = 2;
                node.Button_Text = new string[]{"看演唱会","不看"};

                node.Button_Events[0] = event1;
                node.Button_Events[1] = event1;



                // foreach (var branch in branchList) {
                //     
                //     var branchItem = split_line[1].Split('-');
                //     // 选项名称
                //     var choiseName = branchItem[0];
                //     // 选项归属
                //     var nextConver = branchItem[1];
                // }



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

    }

    public void Choice(string eventName) {
        print(eventName);
    }
}