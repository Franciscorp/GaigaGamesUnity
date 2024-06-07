using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using static Utils;


/// <summary>
/// Dialogue Save State belongs to [DialoguePanel.DialogueManager]
/// Saves all data from dialogues
/// </summary>
public class DialogueSafeStateController : BaseState
{
    private bool _isSavable = true;

    public static DialogueSafeStateController Instance;


    public DialogueDataStructure dialogueStructure { get; set; }
    //public Language language;
    public string importedJson;

    public DialogueSafeStateController()
    {
        this.dialogueStructure = new DialogueDataStructure();
        // TODO BUG being called two times
        //Debug.Log("[DialogueSafeStateController] New()");
    }

    private void Awake()
    {
        saveOnQuit = true;

        EnsureInit();

        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Directory in which player information will be saved
    public override string GetSaveDir()
    {
        var _filepath = Application.persistentDataPath + "/" + Utils.DialoguesSaveFilePath;
        try
        {
            if (!Directory.Exists(_filepath))
                Directory.CreateDirectory(_filepath);
        }
        catch (IOException ex)
        {
            Debug.LogError(ex.Message);
        }

        return _filepath;
    }

    // convert the state to json
    public override string SaveState()
    {
        EnsureInit();
        if (!_isSavable)
        {
            Debug.LogWarning("[Save.Settings] It was not possible to save file.");
            return string.Empty;
        }

        return JsonUtility.ToJson(dialogueStructure, true);
    }


    public override void LoadState(string json)
    {
        EnsureInit();
        // TODO load disable to save data correctly
        //dialogueStructure = JsonUtility.FromJson<DialogueDataStructure>(json);
        // TODO DELETE
        this.dialogueStructure.GenerateData();
    }

    private void EnsureInit()
    {
        if (dialogueStructure == null)
            dialogueStructure = new DialogueDataStructure();
    }

}
