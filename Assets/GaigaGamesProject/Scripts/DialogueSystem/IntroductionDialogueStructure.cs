using static Utils;
using static BaseIntroductionDialogueGenerator;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IntroductionDialogueStructure
{
    public List<Dialogue> introduction;
    public List<Dialogue> askName;
    public List<Dialogue> nameEntered;
    public List<Dialogue> askGender;
    public List<Dialogue> maleGenderEntered;
    public List<Dialogue> femaleGenderEntered;

}