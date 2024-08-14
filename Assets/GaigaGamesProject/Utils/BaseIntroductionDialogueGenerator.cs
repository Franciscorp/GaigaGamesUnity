using System.Collections.Generic;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;
using static UnityEngine.EventSystems.EventTrigger;

class BaseIntroductionDialogueGenerator
{
    public const string Key = "Key";


    public static IntroductionDialogueStructure GenerateIntroductionDialogueStructure()
    {
        IntroductionDialogueStructure introduction = new IntroductionDialogueStructure();
        introduction.introduction = GenerateIntroduction();
        introduction.askName = GenerateAskName();
        introduction.nameEntered = GenerateNameEntered();
        introduction.askGender = GenerateAskGender();
        introduction.maleGenderEntered = GenerateMaleGenderEntered();
        introduction.femaleGenderEntered = GenerateFemaleGenderEntered();

        return introduction;
    }


    // Introduction 
    public static List<Dialogue> GenerateIntroduction()
    {

        List<string> englishText = new List<string>
        {
            "Our story begins with your trip to Grandma's house at the start of summer vacation.",
            "I am the narrator, the all-powerful guide who will accompany you throughout this journey.",
            "But before we start, I need to know..."
        };

        List<string> portugueseText = new List<string>
        {
            "A nossa história começa com a tua ida para a casa da avó no ínicio das férias de verão.",
            "Eu sou o narrador, deus todo poderoso que te acompanha ao longo desta história",
            "Mas antes de começarmos, preciso de saber..."
        };

        Dialogue dialogue = new Dialogue("Introduction" + Key + "1", englishText, portugueseText);

        List<Dialogue> introductionList = new List<Dialogue>();
        introductionList.Add(dialogue);

        return introductionList;
    }

    // Ask Name 
    public static List<Dialogue> GenerateAskName()
    {

        List<string> englishText = new List<string>
        {
            "What should I call you?"
        };

        List<string> portugueseText = new List<string>
        {
            "Como te devo chamar?"
        };

        Dialogue dialogue = new Dialogue("AskName" + Key + "1", englishText, portugueseText);

        List<Dialogue> askNameList = new List<Dialogue>();
        askNameList.Add(dialogue);

        return askNameList;
    }

    // Name Entered
    public static List<Dialogue> GenerateNameEntered()
    {

        List<string> englishText = new List<string>
        {
            "Welcome $username$! Welcome to Gaiga Games!"
        };

        List<string> portugueseText = new List<string>
        {
            "Bem vindo $username$! Bem vindo aos jogos Gaiga!"
        };

        Dialogue dialogue = new Dialogue("NameEntered" + Key + "1", englishText, portugueseText);

        List<Dialogue> nameEnteredList = new List<Dialogue>();
        nameEnteredList.Add(dialogue);

        return nameEnteredList;
    }

    // Ask Name 
    public static List<Dialogue> GenerateAskGender()
    {

        List<string> englishText = new List<string>
        {
            "Are you a boy or a girl?"
        };

        List<string> portugueseText = new List<string>
        {
            "Diz-me, és um rapaz ou uma rapariga?"
        };

        Dialogue dialogue = new Dialogue("AskName" + Key + "1", englishText, portugueseText);

        List<Dialogue> askNameList = new List<Dialogue>();
        askNameList.Add(dialogue);

        return askNameList;
    }

    public static List<Dialogue> GenerateMaleGenderEntered()
    {

        List<string> englishText = new List<string>
        {
            "Ah, so you're a girl! Wonderful!",
            "Are you ready for an adventure full of mysteries and surprises?"
        };
        List<string> portugueseText = new List<string>
        {
            "Ah, então és um rapaz! Excelente! ",
            "Vamos começar essa aventura cheia de descobertas e diversão!"
        };

        Dialogue dialogue = new Dialogue("MaleGenderEntered" + Key + "1", englishText, portugueseText);

        List<Dialogue> genderEnteredList = new List<Dialogue>();
        genderEnteredList.Add(dialogue);

        return genderEnteredList;
    }

    public static List<Dialogue> GenerateFemaleGenderEntered()
    {

        List<string> englishText = new List<string>
        {
            "Ah, so you're a boy! Excellent!",
            "Let's start this adventure full of discoveries and fun!"
        };

        List<string> portugueseText = new List<string>
        {
            "Ah, então és uma rapariga! Maravilha!",
            "Preparada para uma aventura cheia de mistérios e surpresas?"
        };

        Dialogue dialogue = new Dialogue("FemaleGenderEntered" + Key + "1", englishText, portugueseText);

        List<Dialogue> genderEnteredList = new List<Dialogue>();
        genderEnteredList.Add(dialogue);

        return genderEnteredList;
    }

}