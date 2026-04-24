using System.IO;
using UnityEngine;

public class TrackingDirection : MonoBehaviour
{
    public Transform audience;
    public Transform slidesLaptop;
    public Transform slidesBeamer;

    private float audienceLookTime = 0f;
    private float slidesLookBeamerTime = 0f;
    private float slidesLookLaptopTime = 0f;
    private float elsewhereLookTime = 0f;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.transform == audience)
            {
                audienceLookTime += Time.deltaTime;
                Debug.Log("Looking at audience for: " + audienceLookTime + " seconds");
            }
            else if (hit.transform == slidesLaptop)
            {
                slidesLookLaptopTime += Time.deltaTime;
                Debug.Log("Looking at slides Laptop for: " + slidesLookLaptopTime + " seconds");
            }
            else if (hit.transform == slidesBeamer)
            {
                slidesLookBeamerTime += Time.deltaTime;
                Debug.Log("Looking at slides Beamer for: " + slidesLookBeamerTime + " seconds");
            }
            else
            {
                elsewhereLookTime += Time.deltaTime;
                Debug.Log("Looking elsewhere for: " + elsewhereLookTime + " seconds");
            }
        }
        else
        {
            elsewhereLookTime += Time.deltaTime;
            Debug.Log("Looking elsewhere for: " + elsewhereLookTime + " seconds");
        }
    }

    // This method is called when the script is disabled
    void OnDisable()
    {
        // Save the look times when the script is disabled
        SaveTimes();
    }

    // This method is called when the application is quitting
    void OnApplicationQuit()
    {
        // Save the look times when the application is quitting
        SaveTimes();
    }

    // Method to save the look times to PlayerPrefs and to a file
    void SaveTimes()
    {
        // Create a file name with the current time
        string fileName = "LookTimes_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // Prepare the content to write
        string content = "Looking at audience for: " + audienceLookTime + " seconds\n" +
                         "Looking at slides Laptop for: " + slidesLookLaptopTime + " seconds\n" +
                         "Looking at slides Beamer for: " + slidesLookBeamerTime + " seconds\n" +
                         "Looking elsewhere for: " + elsewhereLookTime + " seconds";

        // Write the content to the file
        File.WriteAllText(filePath, content);

        // Debug logs
        Debug.Log("Saved look times to: " + filePath);
        Debug.Log("Looking at audience for: " + audienceLookTime + " seconds");
        Debug.Log("Looking at slides Laptop for: " + slidesLookLaptopTime + " seconds");
        Debug.Log("Looking at slides Beamer for: " + slidesLookBeamerTime + " seconds");
        Debug.Log("Looking elsewhere for: " + elsewhereLookTime + " seconds");

        // Save the audience look time
        PlayerPrefs.SetFloat("AudienceLookTime", audienceLookTime);
        PlayerPrefs.SetFloat("slidesLookLaptopTime", slidesLookLaptopTime);
        PlayerPrefs.SetFloat("slidesLookBeamerTime", slidesLookBeamerTime);
        PlayerPrefs.SetFloat("ElsewhereLookTime", elsewhereLookTime);

        // Write the changes to disk
        PlayerPrefs.Save();

        // Provide user with information on where to find the file
        Debug.Log("You can find the look times file at: " + filePath);
    }
}