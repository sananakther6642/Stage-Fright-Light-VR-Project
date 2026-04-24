using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class LoadLookTimes : MonoBehaviour
{
    public Text lookTimesText; // Assign this in the inspector

    void Start()
    {
        // Load the look times when the scene starts
        LoadTimes();
    }

    void LoadTimes()
    {
        // Get the look times from PlayerPrefs
        float audienceLookTime = PlayerPrefs.GetFloat("AudienceLookTime", 0f);
        float slidesLookLaptopTime = PlayerPrefs.GetFloat("slidesLookLaptopTime", 0f);
        float slidesLookBeamerTime = PlayerPrefs.GetFloat("slidesLookBeamerTime", 0f);
        float elsewhereLookTime = PlayerPrefs.GetFloat("ElsewhereLookTime", 0f);

        // Create a file name with the current time
        string fileName = "LookTimes_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // Prepare the content to display
        string content = "Looking at audience for: \n" + audienceLookTime + " seconds\n" +
                         "Looking at slides Laptop for: \n" + slidesLookLaptopTime + " seconds\n" +
                         "Looking at slides Beamer for: \n" + slidesLookBeamerTime + " seconds\n" +
                         "Looking elsewhere for: \n" + elsewhereLookTime + " seconds\n\n" +
                     "The look times file is saved at: " + filePath;

        // Display the content on the Text component
        lookTimesText.text = content;
    }
}
