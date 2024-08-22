using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VIDE_Data;

public class UIManager : MonoBehaviour
{
    //This script will handle everything related to dialogue interface
    //It will use the VD class to load dialogues and retrieve node data

    #region VARS

    public GameObject containerDialogue;
    public GameObject containerNpc;

    public Image npcIcon;
    public TextMeshProUGUI npcName;
    public TextMeshProUGUI dialogueText;
    public GameObject continueIcon;

    bool dialoguePaused = false; //Custom variable to prevent the manager from calling VD.Next
    bool animatingText = false; //Will help us know when text is currently being animated

    //With this we can start a coroutine and stop it. Used to animate text
    IEnumerator npcTextAnimator;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //containerNpc.SetActive(true);
        //containerPlayer.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InteractWithDialogue()
    {
        if (!VD.isActive)
        {
            Begin(GetComponent<VIDE_Assign>());
        }
        else
        {
            CallNext();
        }
    }

    //This begins the conversation
    void Begin(VIDE_Assign dialogue)
    {
        dialogue.assignedDialogue = "Demo_tutorial";
        //Let's reset the NPC text variables
        dialogueText.text = "";
        npcName.text = "";

        //First step is to call BeginDialogue, passing the required VIDE_Assign component 
        //This will store the first Node data in VD.nodeData
        //But before we do so, let's subscribe to certain events that will allow us to easily
        //Handle the node-changes
        VD.OnActionNode += ActionHandler;
        VD.OnNodeChange += UpdateUI;
        VD.OnEnd += End;

        VD.BeginDialogue(dialogue); //Begins dialogue, will call the first OnNodeChange

        containerDialogue.SetActive(true); //Let's make our dialogue container visible
        containerNpc.SetActive(true); //Let's make our dialogue container visible
    }

    //Calls next node in the dialogue
    public void CallNext()
    {
        //Let's not go forward if text is currently being animated, but let's speed it up.
        if (animatingText) { CutTextAnim(); return; }

        if (!dialoguePaused) //Only if
        {
            VD.Next(); //We call the next node and populate nodeData with new data. Will fire OnNodeChange.
        }
    }

    private void End(VD.NodeData data)
    {
        //containerDialogue.SetActive(false);
        containerNpc.SetActive(false);

        VD.OnActionNode -= ActionHandler;
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= End;
        VD.EndDialogue();
    }

    private void UpdateUI(VD.NodeData data)
    {
        //containerDialogue.SetActive(false);
        //containerNpc.SetActive(false);
        //containerPlayer.SetActive(false);
        npcIcon.sprite = null;
        dialogueText.text = "";
        continueIcon.SetActive(false);

        if (!data.isPlayer)
        {
            //containerNpc.SetActive(true);
            //dialogueText.text = data.comments[data.commentIndex];


            //Set node sprite if there's any, otherwise try to use default sprite
            if (data.sprite != null)
            {
                //For NPC sprite, we'll first check if there's any "sprite" key
                //Such key is being used to apply the sprite only when at a certain comment index
                //Check CrazyCap dialogue for reference
                if (data.extraVars.ContainsKey("sprite"))
                {
                    if (data.commentIndex == (int)data.extraVars["sprite"])
                        npcIcon.sprite = data.sprite;
                    else
                        npcIcon.sprite = VD.assigned.defaultNPCSprite; //If not there yet, set default dialogue sprite
                }
                else //Otherwise use the node sprites
                {
                    npcIcon.sprite = data.sprite;
                }
            } //or use the default sprite if there isnt a node sprite at all
            else if (VD.assigned.defaultNPCSprite != null)
                npcIcon.sprite = VD.assigned.defaultNPCSprite;

            //This coroutine animates the NPC text instead of displaying it all at once
            npcTextAnimator = DrawText(data.comments[data.commentIndex], 0.06f);
            StartCoroutine(npcTextAnimator);

            //If it has a tag, show it, otherwise let's use the alias we set in the VIDE Assign
            if (data.tag.Length > 0)
                npcName.text = data.tag;
            else
                npcName.text = VD.assigned.alias;

            //Sets the NPC container on
            containerDialogue.SetActive(true);
            containerNpc.SetActive(true);

        }

    }

    private void OnDisable()
    {
        if (containerNpc != null)
            End(null);
    }

    public void SetPlayerChoice(int choice)
    {
        VD.nodeData.commentIndex = choice;
        //VD.Next();
    }

    #region Events and Handlers

    //Another way to handle Action Nodes is to listen to the OnActionNode event, which sends the ID of the action node
    void ActionHandler(int actionNodeID)
    {
        //Debug.Log("ACTION TRIGGERED: " + actionNodeID.ToString());
    }

    IEnumerator DrawText(string text, float time)
    {
        animatingText = true;

        string[] words = text.Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i];
            if (i != words.Length - 1) word += " ";

            string previousText = dialogueText.text;

            float lastHeight = dialogueText.preferredHeight;
            dialogueText.text += word;
            if (dialogueText.preferredHeight > lastHeight)
            {
                previousText += System.Environment.NewLine;
            }

            for (int j = 0; j < word.Length; j++)
            {
                dialogueText.text = previousText + word.Substring(0, j + 1);
                yield return new WaitForSeconds(time);
            }
        }
        dialogueText.text = text;
        animatingText = false;
        continueIcon.SetActive(true);
    }

    void CutTextAnim()
    {
        StopCoroutine(npcTextAnimator);
        dialogueText.text = VD.nodeData.comments[VD.nodeData.commentIndex]; //Now just copy full text		
        animatingText = false;
        continueIcon.SetActive(true);
    }

    #endregion
}
