using System.Collections.Generic;
using System.Linq;
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

    public SpeechElementDialogues GetSpeechElementDialogues(SpeechElements speechElement)
    {
        foreach(SpeechElementDialogues x in speechElementsDialogues)
        {
            if(x.speechElementName == speechElement) 
                return x;
        }
        return null;
    }

    public List<Dialogue> GetSpeechElementDialogues(SpeechElements speechElement, DialogueEventType type)
    {
        SpeechElementDialogues speechElementDialgues = null;
        foreach (SpeechElementDialogues x in speechElementsDialogues)
        {
            if (x.speechElementName == speechElement)
                speechElementDialgues = x;
        }

        if (speechElementDialgues == null)
            return null;

        switch (type)
        {
            case DialogueEventType.Suggestion:
                return speechElementDialgues.suggestion;
            case DialogueEventType.RightAnswer:
                return speechElementDialgues.rightAnswer;
            case DialogueEventType.WrongAnswer:
                return speechElementDialgues.wrongAnswer;
        }

        return null;
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