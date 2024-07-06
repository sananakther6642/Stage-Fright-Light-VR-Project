using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MainMenu : MonoBehaviour
{
    private List<InputDevice> devices = new List<InputDevice>();
    private InputDevice leftController;
    private InputDevice rightController;

    private int selectedIndex = 0;
    private string[] menuItems = { "Play", "Quit" };

    void Start()
    {
        GetDevices();
    }

    void GetDevices()
    {
        InputDevices.GetDevices(devices);
        foreach (var device in devices)
        {
            if (device.characteristics.HasFlag(InputDeviceCharacteristics.Left))
            {
                leftController = device;
            }
            else if (device.characteristics.HasFlag(InputDeviceCharacteristics.Right))
            {
                rightController = device;
            }
        }
    }

    void Update()
    {
        HandleThumbstickInput();
        HandleTriggerInput();
        HandleButtonInput();
    }

    void HandleThumbstickInput()
    {
        Vector2 thumbstickValue;
        if (leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out thumbstickValue))
        {
            if (thumbstickValue.y > 0.5f)
            {
                selectedIndex = Mathf.Max(0, selectedIndex - 1);
                Debug.Log("Selected: " + menuItems[selectedIndex]);
            }
            else if (thumbstickValue.y < -0.5f)
            {
                selectedIndex = Mathf.Min(menuItems.Length - 1, selectedIndex + 1);
                Debug.Log("Selected: " + menuItems[selectedIndex]);
            }
        }
    }

    void HandleTriggerInput()
    {
        bool triggerValue;
        if (rightController.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
        {
            switch (selectedIndex)
            {
                case 0:
                    Play();
                    break;
                case 1:
                    Quit();
                    break;
            }
        }
    }

    void HandleButtonInput()
    {
        bool aButtonValue;
        if (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out aButtonValue) && aButtonValue)
        {
            SceneManager.LoadScene("MainMenu");
            Time.timeScale = 0;
        }

        bool bButtonValue;
        if (rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bButtonValue) && bButtonValue)
        {
            Time.timeScale = 1;
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("User Has Quit");
    }
}
