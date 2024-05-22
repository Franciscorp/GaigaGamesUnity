using System.Collections.Generic;
using static UtilsSpeechMachine;

[System.Serializable]
public class SpeechMachineDialogueStructure
{

    private List<Dialogue> introduction;
    private List<Dialogue> rightAnswers;
    private List<Dialogue> closure;
    private List<SpeechElementDialogues> speechElements;
}

[System.Serializable]
public class SpeechElementDialogues
{
    private SpeechElements speechElementName;
    private List<Dialogue> wrongAnswer;
    private List<Dialogue> rightAnswer;
}