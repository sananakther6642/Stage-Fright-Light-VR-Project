using UnityEngine;
using System.Collections.Generic;

public class OverallHeartRateGraph : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private List<float> dataPoints = new List<float>();
    private float time = 0f;

    void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
    }

    public void AddDataPoint(float value)
    {
        dataPoints.Add(value);
        UpdateGraph();
    }

    private void UpdateGraph()
    {
        lineRenderer.positionCount = dataPoints.Count;
        for (int i = 0; i < dataPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(i * 0.1f, dataPoints[i], 0));
        }
    }
}
