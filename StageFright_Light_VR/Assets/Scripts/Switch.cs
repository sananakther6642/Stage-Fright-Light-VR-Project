using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem.Controls;

public class Switch : MonoBehaviour
{
    public Image[] backgroundCanvas1; // Array for images on the first canvas
    public Image[] backgroundCanvas2; // Array for images on the second canvas
    private int index;
    private bool canNavigate = true;

    void Start()
    {
        index = PlayerPrefs.GetInt("index", 0);
        SetActiveBackground();
    }

    void Update()
    {
        // Check for VR controller inputs
        var leftHand = InputSystem.GetDevice<UnityEngine.InputSystem.XR.XRController>(CommonUsages.LeftHand);
        var rightHand = InputSystem.GetDevice<UnityEngine.InputSystem.XR.XRController>(CommonUsages.RightHand);

        if (leftHand != null)
        {
            HandleInput(leftHand);
        }

        if (rightHand != null)
        {
            HandleInput(rightHand);
        }

        // Check for keyboard inputs
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame && canNavigate)
        {
            Next();
            canNavigate = false;
            Invoke(nameof(ResetNavigate), 0.5f); // Add a delay to prevent rapid navigation
        }
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame && canNavigate)
        {
            Previous();
            canNavigate = false;
            Invoke(nameof(ResetNavigate), 0.5f); // Add a delay to prevent rapid navigation
        }
    }

    private void HandleInput(UnityEngine.InputSystem.XR.XRController controller)
    {
        // Assuming the VR controller has a joystick or thumbstick mapped to primary2DAxis
        var primary2DAxis = controller.TryGetChildControl<Vector2Control>("primary2DAxis");

        if (primary2DAxis != null)
        {
            Vector2 axis = primary2DAxis.ReadValue();
            if (axis.x > 0.5f && canNavigate)
            {
                Next();
                canNavigate = false;
                Invoke(nameof(ResetNavigate), 0.5f); // Add a delay to prevent rapid navigation
            }
            else if (axis.x < -0.5f && canNavigate)
            {
                Previous();
                canNavigate = false;
                Invoke(nameof(ResetNavigate), 0.5f); // Add a delay to prevent rapid navigation
            }
        }
    }

    private void ResetNavigate()
    {
        canNavigate = true;
    }

    public void Next()
    {
        index++;
        if (index >= backgroundCanvas1.Length) index = 0;
        SetActiveBackground();
    }

    public void Previous()
    {
        index--;
        if (index < 0)
            index = backgroundCanvas1.Length - 1;
        SetActiveBackground();
    }

    void SetActiveBackground()
    {
        // Set active background for Canvas 1
        for (int i = 0; i < backgroundCanvas1.Length; i++)
        {
            backgroundCanvas1[i].gameObject.SetActive(i == index);
        }

        // Set active background for Canvas 2
        for (int i = 0; i < backgroundCanvas2.Length; i++)
        {
            backgroundCanvas2[i].gameObject.SetActive(i == index);
        }

        PlayerPrefs.SetInt("index", index);
        PlayerPrefs.Save();
    }
}