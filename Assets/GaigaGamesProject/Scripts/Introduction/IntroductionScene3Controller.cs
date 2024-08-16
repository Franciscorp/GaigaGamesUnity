using UnityEngine;

public class IntroductionScene3Controller : MonoBehaviour
{
    public GameObject Silhouette;
    public GameObject Male;
    public GameObject Female;

    public void SetSceneAccordingToGender(Utils.Gender gender)
    {
        Silhouette.SetActive(false);

        if (gender == Utils.Gender.Male)
        {
            Male.SetActive(true);
            Female.SetActive(false);
        }
        else
        {
            Male.SetActive(false);
            Female.SetActive(true);
        }
    }
}
