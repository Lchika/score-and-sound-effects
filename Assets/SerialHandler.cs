using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;
using UnityEngine.SceneManagement;

public class SerialHandler : MonoBehaviour
{
    public delegate void SerialDataReceivedEventHandler(string message);
    public event SerialDataReceivedEventHandler OnDataReceived;

	//public string portName = "/dev/tty.usbserial-AI045S5Q"; // ポート名(Macだと/dev/tty.usbmodem1421など)
	public string portName = "/dev/tty.usbserial-A7043RK5"; // pro mini
    public int baudRate = 115200;  // ボーレート(Arduinoに記述したものに合わせる)

    private SerialPort serialPort_;
    private Thread thread_;
    private bool isRunning_ = false;

    private string message_;
    private byte[] buff = new byte[1];
    private bool isNewMessageReceived_ = false;

    void Awake()
    {
        Open();
    }

    void Update()
    {
        if (isNewMessageReceived_)
        {
            OnDataReceived(message_);
        }
        isNewMessageReceived_ = false;
    }

    void OnDestroy()
    {
        Close();
    }

    private void Open()
    {
        serialPort_ = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
        //または
        //serialPort_ = new SerialPort(portName, baudRate);
        serialPort_.Open();
        Debug.Log("Serial Port Open!");

        isRunning_ = true;

        thread_ = new Thread(new ThreadStart(Read));
        thread_.Start();
    }

    private void Close()
    {
        isNewMessageReceived_ = false;
        isRunning_ = false;

        if (thread_ != null && thread_.IsAlive)
        {
            thread_.Join();
            thread_ = null;
        }

        if (serialPort_ != null && serialPort_.IsOpen)
        {
            serialPort_.Close();
            serialPort_.Dispose();
            Debug.Log("Serial Port Close!");
        }
    }

    private void Read()
    {
        while (isRunning_ && serialPort_ != null && serialPort_.IsOpen)
        {
            //Debug.Log("Read Serial Port!");
			if (serialPort_.BytesToRead > 0) {
				try {
					//message_ = serialPort_.ReadLine();
					serialPort_.Read (buff, 0, 1);
					//ASCII エンコード
					message_ = System.Text.Encoding.ASCII.GetString (buff);
					Debug.Log ("Read Serial Massage, " + message_);
					isNewMessageReceived_ = true;
				} catch (System.Exception e) {
					Debug.LogWarning (e.Message);
				}
			}
        }
    }

    public void Write(string message)
    {
        try
        {
            serialPort_.Write(message);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}