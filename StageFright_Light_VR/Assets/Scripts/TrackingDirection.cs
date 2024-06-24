using System.Collections;
using System.Collections.Generic;
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

    // Method to save the look times to PlayerPrefs
    void SaveTimes()
    {
        Debug.Log("Last");
        // Save the audience look time
        PlayerPrefs.SetFloat("AudienceLookTime", audienceLookTime);
        Debug.Log("Looking at audience for: " + audienceLookTime + " seconds");
        // Save the slides look time
        PlayerPrefs.SetFloat("slidesLookLaptopTime", slidesLookLaptopTime);
        Debug.Log("Looking at slides Laptop for: " + slidesLookLaptopTime + " seconds");
        PlayerPrefs.SetFloat("slidesLookBeamerTime", slidesLookBeamerTime);
        Debug.Log("Looking at slides Beamer for: " + slidesLookBeamerTime + " seconds");
        // Save the elsewhere look time
        PlayerPrefs.SetFloat("ElsewhereLookTime", elsewhereLookTime);
        Debug.Log("Looking elsewhere for: " + elsewhereLookTime + " seconds");
        // Write the changes to disk
        PlayerPrefs.Save();
    }
}
