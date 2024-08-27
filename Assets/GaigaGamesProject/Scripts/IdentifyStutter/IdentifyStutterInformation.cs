using static Utils;
using UnityEngine;

public class IdentifyStutterInformation
{
    private int NumberOfWrongAnswers { get; set; }
    private ConclusionState isConcluded { get; set; }

    public IdentifyStutterInformation(int numberOfWrongAnswers, ConclusionState isConcluded)
    {
        NumberOfWrongAnswers = numberOfWrongAnswers;
        this.isConcluded = isConcluded;
    }

    // Loads or Resets Data
    public IdentifyStutterInformation()
    {
        LoadIdentifyStutterInfo();
    }


    public void ResetIdentifyStutter()
    {
        NumberOfWrongAnswers = 0;
        this.isConcluded = ConclusionState.NotConcluded;
    }

    public void EndIdentifyStutterGame(int wrongAnswersDone)
    {
        NumberOfWrongAnswers = wrongAnswersDone;
        this.isConcluded = ConclusionState.Concluded;
        SaveIdentifyStutterInfo();
    }

    public int GetNumberOfWrongAnswer()
    {
        return PlayerPrefs.GetInt(Utils.IdentifyStutterWrongAnswersDone);
    }


    #region Save and Load
    public void LoadIdentifyStutterInfo()
    {
        NumberOfWrongAnswers = PlayerPrefs.GetInt(Utils.IdentifyStutterWrongAnswersDone, 0);
        Debug.Log(NumberOfWrongAnswers);
        isConcluded = (ConclusionState)PlayerPrefs.GetInt(Utils.IdentifyStutterConclusionState, 0);
        Debug.Log(isConcluded);
    }

    public void SaveIdentifyStutterInfo()
    {
        PlayerPrefs.SetInt(Utils.IdentifyStutterWrongAnswersDone, NumberOfWrongAnswers);
        PlayerPrefs.SetInt(Utils.IdentifyStutterConclusionState, (int)isConcluded);
        PlayerPrefs.Save();
        Debug.Log("ItentifyStutter information saved");
    }

    public void SetGameConclusion(ConclusionState isConcluded)
    {
        PlayerPrefs.SetInt(Utils.IdentifyStutterConclusionState, (int)isConcluded);
        PlayerPrefs.Save();
        Debug.Log("ItentifyStutter Conclusion State saved");
    }


    public void SetIdentifyStutterWrongAnswersDone(int numberOfWrongAnswers)
    {
        PlayerPrefs.SetInt(Utils.IdentifyStutterWrongAnswersDone, numberOfWrongAnswers);
        PlayerPrefs.Save();
        Debug.Log("ItentifyStutter Number of Wrong answers saved");
    }
    #endregion

}