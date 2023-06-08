using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using System;

public class Socket : MonoBehaviour
{
    Thread mThread;
    public string connectionIP = "127.0.0.1";
    public int connectionPort = 25001;
    IPAddress localAdd;
    TcpListener listener;
    TcpClient client;
    string receivedEmotion = "";
    string receivedPos = "";
    public RawImage rawImage;
    private byte[] receivedData;
    public float frameUpdateRate = 0.1f; // Adjust this value as needed (e.g., 0.1f corresponds to 10 frames per second)
    private float lastFrameUpdateTime;

    bool running;
    int score = 0;

    [SerializeField]
    Scrollbar scrollbar;
    private void Update()
    {

        //Debug.Log("Emotion detected: " + receivedEmotion);
        if (receivedData != null && Time.time - lastFrameUpdateTime >= frameUpdateRate)
        {
            Texture2D receivedTexture = new Texture2D(640, 480);  // Use the same resized resolution
            receivedTexture.LoadImage(receivedData);
            rawImage.texture = receivedTexture;

            // Release memory
            receivedData = null;

            lastFrameUpdateTime = Time.time;
        }
        ComputerScript computerScript = gameObject.GetComponent<ComputerScript>();
        if (computerScript.isRunning)
        {
            if (receivedEmotion.Equals(computerScript.emotion))
            {
                score++;
            }
            else if (score > 0)
            {
                score--;
            }
            if (score >= 400)
            {
                computerScript.ValidateEmotion();
                score = 0;
            }
        }
        scrollbar.size = score/400f;
    }

    private void Start()
    {
        StartCoroutine(Wait());
        ThreadStart ts = new ThreadStart(GetInfo);
        mThread = new Thread(ts);
        mThread.Start();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
    }

    void GetInfo()
    {
        localAdd = IPAddress.Parse(connectionIP);
        listener = new TcpListener(IPAddress.Any, connectionPort);
        listener.Start();

        client = listener.AcceptTcpClient();

        running = true;
        while (running)
        {
            SendAndReceiveData();
        }
        listener.Stop();
    }

    void SendAndReceiveData()
    {
        NetworkStream nwStream = client.GetStream();

        //---receiving Data from the Host----
        receivedData = ReadMessage(nwStream);
        receivedPos = Convert.ToBase64String(receivedData);

        //---receiving Emotion from the Host---
        byte[] emotionData = ReadMessage(nwStream);
        receivedEmotion = Encoding.UTF8.GetString(emotionData);

        //---Sending Data to Host----
        byte[] myWriteBuffer = Encoding.ASCII.GetBytes("Frame received by Unity");
        nwStream.Write(myWriteBuffer, 0, myWriteBuffer.Length);
    }

    byte[] ReadMessage(NetworkStream nwStream)
    {
        byte[] sizeBuffer = new byte[4];
        nwStream.Read(sizeBuffer, 0, 4);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(sizeBuffer);
        }
        int size = BitConverter.ToInt32(sizeBuffer, 0);

        byte[] buffer = new byte[size];
        int totalBytesRead = 0;
        while (totalBytesRead < size)
        {
            totalBytesRead += nwStream.Read(buffer, totalBytesRead, size - totalBytesRead);
        }

        return buffer;
    }




    public static Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }
}