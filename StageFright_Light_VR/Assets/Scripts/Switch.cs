using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject[] background;
    private int index;

    void Start()
    {
        index = PlayerPrefs.GetInt("index", 0);
        SetActiveBackground();
        Debug.Log("Starting index: " + index);
    }

    public void Next()
    {
        Debug.Log("Next button pressed");
        index++;
        if (index >= background.Length) index = 0;
        Debug.Log("Next index: " + index);
        SetActiveBackground();
    }

    public void Previous()
    {
        Debug.Log("Previous button pressed");
        index--;
        if (index < 0)
            index = background.Length - 1;
        Debug.Log("Previous index: " + index);
        SetActiveBackground();
    }

    void SetActiveBackground()
    {
        for (int i = 0; i < background.Length; i++)
        {
            background[i].SetActive(i == index);
        }
        PlayerPrefs.SetInt("index", index);
        PlayerPrefs.Save();
        Debug.Log("SetActiveBackground called. Current index: " + index);
    }
}
