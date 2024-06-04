using System.Collections.Generic;
using static UtilsSpeechMachine;

[System.Serializable]
public class SpeechMachineDialogueStructure
{

    public List<Dialogue> introduction;
    public List<Dialogue> askSuggestion;
    public List<Dialogue> rightAnswers;
    public List<Dialogue> conclusion;
    public List<SpeechElementDialogues> speechElementsDialogues;

    public SpeechMachineDialogueStructure(List<Dialogue> introduction, List<Dialogue> askSuggestion,
        List<Dialogue> rightAnswers, List<Dialogue> conclusion, List<SpeechElementDialogues> speechElementsDialogues)
    {
        this.introduction = introduction;
        this.askSuggestion = askSuggestion;
        this.rightAnswers = rightAnswers;
        this.conclusion = conclusion;
        this.speechElementsDialogues = speechElementsDialogues;
    }
}

[System.Serializable]
public class SpeechElementDialogues
{
    public SpeechElements speechElementName;
    public List<Dialogue> suggestion;
    public List<Dialogue> wrongAnswer;
    public List<Dialogue> rightAnswer;

    public SpeechElementDialogues(SpeechElements speechElementName, List<Dialogue> suggestion, List<Dialogue> wrongAnswer, List<Dialogue> rightAnswer)
    {
        this.speechElementName = speechElementName;
        this.suggestion = suggestion;
        this.wrongAnswer = wrongAnswer;
        this.rightAnswer = rightAnswer;
    }
}