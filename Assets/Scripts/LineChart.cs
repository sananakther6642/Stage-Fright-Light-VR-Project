using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LineChart : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Canvas canvas;  // Assign this in the Inspector
    private string csvFilePath;
    private List<Vector2> points = new List<Vector2>();

    void Start()
    {
        // Get the newest CSV file
        csvFilePath = GetNewestCsvFile(Application.dataPath);

        if (csvFilePath != null)
        {
            // Load CSV data
            LoadCSVData();

            // Check if there are any data points
            if (points.Count > 0)
            {
                // Normalize points to fit canvas
                points = NormalizePoints(points);

                // Draw line chart
                DrawLineChart();
            }
            else
            {
                Debug.Log("No data points found in CSV file: " + csvFilePath);
            }
        }
        else
        {
            Debug.Log("No CSV files found in directory: " + Application.dataPath);
        }
    }

    private string GetNewestCsvFile(string directoryPath)
    {
        var directoryInfo = new DirectoryInfo(directoryPath);
        var csvFile = directoryInfo.GetFiles("*.csv")
                                   .OrderByDescending(f => f.LastWriteTime)
                                   .FirstOrDefault();
        return csvFile?.FullName;
    }

    private void LoadCSVData()
    {
        if (File.Exists(csvFilePath))
        {
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                reader.ReadLine(); // Skip header line

                float startTime = 0;
                bool isFirstLine = true;

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');

                    // Parse time and heart rate
                    float time = float.Parse(values[0]);
                    float heartRate = float.Parse(values[1]);

                    // Subtract start time from all time values
                    if (isFirstLine)
                    {
                        startTime = time;
                        isFirstLine = false;
                    }
                    time -= startTime;

                    // Add point to list
                    points.Add(new Vector2(time, heartRate));
                }
            }
        }
        else
        {
            Debug.LogError("CSV file not found: " + csvFilePath);
        }
    }

    private List<Vector2> NormalizePoints(List<Vector2> points)
    {
        if (points.Count == 0)
        {
            Debug.LogError("No data points to normalize.");
            return points;
        }

        float minX = points.Min(point => point.x);
        float maxX = points.Max(point => point.x);
        float minY = points.Min(point => point.y);
        float maxY = points.Max(point => point.y);

        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        for (int i = 0; i < points.Count; i++)
        {
            float normalizedX = (points[i].x - minX) / (maxX - minX) * canvasRect.rect.width;
            float normalizedY = (points[i].y - minY) / (maxY - minY) * canvasRect.rect.height;
            points[i] = new Vector2(normalizedX, normalizedY);
        }

        return points;
    }

    private void DrawLineChart()
    {
        // Set line renderer properties
        lineRenderer.positionCount = points.Count;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        // Set line renderer positions
        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, points[i]);
        }
    }
}
