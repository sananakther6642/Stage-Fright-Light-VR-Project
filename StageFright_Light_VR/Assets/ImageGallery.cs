using UnityEngine;
using UnityEngine.UI;

public class ImageGallery : MonoBehaviour
{
    public Image displayImage; // Reference to the UI Image component
    public Sprite[] images;    // Array to hold your images
    public Button nextButton;  // Reference to the Next button
    public Button backButton;  // Reference to the Back button

    private int currentIndex = 0;

    void Start()
    {
        // Ensure there are images in the array
        if (images.Length > 0)
        {
            // Display the first image
            displayImage.sprite = images[currentIndex];
        }
        else
        {
            Debug.LogError("No images assigned to the gallery.");
        }

        // Add button click listeners
        nextButton.onClick.AddListener(NextImage);
        backButton.onClick.AddListener(PreviousImage);

        // Update button states
        UpdateButtonStates();
    }

    void NextImage()
    {
        if (currentIndex < images.Length - 1)
        {
            currentIndex++;
            displayImage.sprite = images[currentIndex];
            UpdateButtonStates();
        }
    }

    void PreviousImage()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            displayImage.sprite = images[currentIndex];
            UpdateButtonStates();
        }
    }

    void UpdateButtonStates()
    {
        nextButton.interactable = currentIndex < images.Length - 1;
        backButton.interactable = currentIndex > 0;
    }
}
