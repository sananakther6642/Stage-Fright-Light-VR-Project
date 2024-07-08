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

    private bool canNavigate = true;

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
        bool inputReceived = false;

        // Handle thumbstick input for both controllers
        inputReceived |= HandleThumbstickInput(leftController);
        inputReceived |= HandleThumbstickInput(rightController);

        // Handle trigger input
        if (rightController != null)
        {
            HandleTriggerInput(rightController);
        }

        if (inputReceived && canNavigate)
        {
            canNavigate = false;
            StartCoroutine(ResetNavigateAfterDelay(0.5f)); // Add a delay to prevent rapid navigation
        }
    }

    private bool HandleThumbstickInput(InputDevice controller)
    {
        if (controller == null) return false;

        Vector2 thumbstickValue;
        if (controller.TryGetFeatureValue(CommonUsages.primary2DAxis, out thumbstickValue))
        {
            if (thumbstickValue.y > 0.5f && canNavigate)
            {
                selectedIndex = Mathf.Max(0, selectedIndex - 1);
                Debug.Log("Selected: " + menuItems[selectedIndex]);
                return true;
            }
            else if (thumbstickValue.y < -0.5f && canNavigate)
            {
                selectedIndex = Mathf.Min(menuItems.Length - 1, selectedIndex + 1);
                Debug.Log("Selected: " + menuItems[selectedIndex]);
                return true;
            }
        }

        return false;
    }

    private void HandleTriggerInput(InputDevice controller)
    {
        bool triggerValue;
        if (controller.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
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

    private IEnumerator ResetNavigateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canNavigate = true;
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
