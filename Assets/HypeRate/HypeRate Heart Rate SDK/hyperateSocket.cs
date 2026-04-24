using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using NativeWebSocket;
using System;
using System.IO;

public class hyperateSocket : MonoBehaviour
{
    public string websocketToken = "HfNvht2sfZvmrFAtTXg6j4nRx4A4FLPp8mKS2iGea9GQSU331OBvAGVdcjfQcuc5";
    public string hyperateID = "internal-testing";

    public Text heartRateText; // Assign this in the Inspector

    private WebSocket websocket;
    private string csvFilePath;

    void Start()
    {
        string websocketUrl = "wss://app.hyperate.io/socket/websocket?token=" + websocketToken;
        Debug.Log("Connecting to: " + websocketUrl);
        websocket = new WebSocket(websocketUrl);

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
            SendWebSocketMessage();
        };

        websocket.OnError += (e) =>
        {
            Debug.LogError("WebSocket Error: " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("WebSocket Connection closed: " + e);
        };

        websocket.OnMessage += WebSocket_OnMessage;

        InvokeRepeating("SendHeartbeat", 1.0f, 25.0f);

        ConnectWebSocket();

        // Initialize CSV file path with timestamp
        string date = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        csvFilePath = Path.Combine(Application.dataPath, $"{date}_HeartRateData.csv");
        CreateCSVFile();
    }

    private async void ConnectWebSocket()
    {
        try
        {
            await websocket.Connect();
        }
        catch (System.Exception ex)
        {
            Debug.LogError("WebSocket connection exception: " + ex.Message);
        }
    }

    private void WebSocket_OnMessage(byte[] bytes)
    {
        var message = System.Text.Encoding.UTF8.GetString(bytes);
        Debug.Log("Received message: " + message);
        try
        {
            var msg = JObject.Parse(message);
            Debug.Log("Event type: " + msg["event"].ToString());

            if (msg["event"].ToString() == "hr_update")
            {
                var heartRate = float.Parse(msg["payload"]["hr"].ToString());
                Debug.Log("Heart rate update received: " + heartRate);

                if (heartRateText != null)
                {
                    heartRateText.text = "Heart Rate: " + heartRate.ToString();
                }
                else
                {
                    Debug.LogError("HeartRateText is not assigned. Make sure it's properly set in the Inspector.");
                }

                // Record heart rate to CSV file
                RecordHeartRateToCSV(heartRate);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error parsing WebSocket message: " + ex.Message);
        }
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
#endif
    }

    async void SendWebSocketMessage()
    {
        if (websocket.State == WebSocketState.Open)
        {
            string message = "{\"topic\": \"hr:" + hyperateID + "\", \"event\": \"phx_join\", \"payload\": {}, \"ref\": 0}";
            Debug.Log("Sending message: " + message);
            await websocket.SendText(message);
        }
        else
        {
            Debug.LogWarning("WebSocket is not open. State: " + websocket.State);
        }
    }

    async void SendHeartbeat()
    {
        if (websocket.State == WebSocketState.Open)
        {
            string heartbeat = "{\"topic\": \"phoenix\",\"event\": \"heartbeat\",\"payload\": {},\"ref\": 0}";
            Debug.Log("Sending heartbeat: " + heartbeat);
            await websocket.SendText(heartbeat);
        }
        else
        {
            Debug.LogWarning("WebSocket is not open. State: " + websocket.State);
        }
    }

    private async void OnApplicationQuit()
    {
        if (websocket != null && websocket.State == WebSocketState.Open)
        {
            await websocket.Close();
        }
    }

    private void CreateCSVFile()
    {
        if (!File.Exists(csvFilePath))
        {
            using (StreamWriter writer = new StreamWriter(csvFilePath))
            {
                writer.WriteLine("Time,Heart Rate");
            }
        }
    }

    private void RecordHeartRateToCSV(float heartRate)
    {
        using (StreamWriter writer = new StreamWriter(csvFilePath, true))
        {
            string time = DateTime.Now.ToString("HH:mm:ss");
            writer.WriteLine($"{time},{heartRate}");
        }
    }
}
