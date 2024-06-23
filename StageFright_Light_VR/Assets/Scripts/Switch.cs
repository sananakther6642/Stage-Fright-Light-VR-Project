using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    public Image[] backgroundCanvas1; // Array for images on the first canvas
    public Image[] backgroundCanvas2; // Array for images on the second canvas
    private int index;

    void Start()
    {
        index = PlayerPrefs.GetInt("index", 0);
        SetActiveBackground();
    }

    void Update()
    {
        // Check for keyboard inputs
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Next();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Previous();
        }
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
