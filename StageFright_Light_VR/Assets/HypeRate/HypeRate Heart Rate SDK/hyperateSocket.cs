using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using NativeWebSocket;

public class hyperateSocket : MonoBehaviour
{
    public string websocketToken = "HfNvht2sfZvmrFAtTXg6j4nRx4A4FLPp8mKS2iGea9GQSU331OBvAGVdcjfQcuc5";
    public string hyperateID = "2fe4ab";

    public Text heartRateText; // Assign this in the Inspector

    private WebSocket websocket;

    void Start()
    {
        websocket = new WebSocket("wss://app.hyperate.io/socket/websocket?token=" + websocketToken);
        Debug.Log("Connect!");

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
            SendWebSocketMessage();
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        websocket.OnMessage += WebSocket_OnMessage;

        InvokeRepeating("SendHeartbeat", 1.0f, 25.0f);

        websocket.Connect();
    }

    private void WebSocket_OnMessage(byte[] bytes)
    {
        var message = System.Text.Encoding.UTF8.GetString(bytes);
        Debug.Log("Received message: " + message);
        var msg = JObject.Parse(message);

        Debug.Log("Event type: " + msg["event"].ToString());

        if (msg["event"].ToString() == "hr_update")
        {
            var heartRate = float.Parse(msg["payload"]["hr"].ToString()); // Parse heart rate as float
            Debug.Log("Heart rate update received: " + heartRate);

            // Update graph visualization
          

            // Update the heart rate text
            if (heartRateText != null)
            {
                heartRateText.text = "♥️ " + heartRate.ToString();
            }
            else
            {
                Debug.LogError("HeartRateText is not assigned. Make sure it's properly set in the Inspector.");
            }
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
            await websocket.SendText("{\"topic\": \"hr:" + hyperateID + "\", \"event\": \"phx_join\", \"payload\": {}, \"ref\": 0}");
        }
    }

    async void SendHeartbeat()
    {
        if (websocket.State == WebSocketState.Open)
        {
            await websocket.SendText("{\"topic\": \"phoenix\",\"event\": \"heartbeat\",\"payload\": {},\"ref\": 0}");
        }
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }
}
