using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.WebSockets;
using System.IO;

namespace VNN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Website = new PanWebsite("http://localhost:2778/", WebsiteLife);
            Website.onWebSocketMessage = OnWebSocketMessage;
            website_started = false;
        }

        public const string PATH_TO_DATA_FILE = @"D:\PROJECTS\VisualNeuralNetwork\VNN\VNN\data.json";

        PanWebsite Website;
        private bool website_started;
        private void Form1_Load(object sender, EventArgs e)
        {
            //
        }

        private void BtnStartWebsite_Click(object sender, EventArgs e)
        {
            if (!website_started)
            {
                Website.Start();
                website_started = true;
                console1.AppendText("website started\n");
            }
            else
            {
                MessageBox.Show("Website is already started!");
            }
        }

        public PanResponse WebsiteLife(PanRequest request)
        {
            if(File.Exists("index.html"))
            return PanResponse.ReturnHtml("index.html");
            else
                return PanResponse.ReturnHtml(@"D:\PROJECTS\VisualNeuralNetwork\localhost\index.html");
        }

        private void BtnStopWebsite_Click(object sender, EventArgs e)
        {
            if (website_started)
            {
                Website.Stop();
                website_started = false;
                console1.AppendText("website stopped\n");
            }
            else
            {
                MessageBox.Show("Website is already stopped!");
            }
        }
        public void OnWebSocketMessage(WebSocket ws, string message)
        { 
            Invoke(new Action(() =>
            {
                console1.AppendText($"->{message}\n");
            }));
        }

        private void BtnOpenHtml_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://localhost:2778");
        }

        private void BtnSendMsgUsingWebsocket_Click(object sender, EventArgs e)
        {
            string message = tbMessageToWebsocket.Text;
            Website.WebSocketSend(message);
            console1.AppendText($"<-{message}\n");
        }

        /*
static HttpListener httpListener = new HttpListener();
private static Mutex signal = new Mutex();
private void Form1_Load(object sender, EventArgs e)
{

}

public static async System.Threading.Tasks.Task ReceiveConnection()
{
   HttpListenerContext context = await
   httpListener.GetContextAsync();
   if (context.Request.IsWebSocketRequest)
   {
       HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(null);
       WebSocket webSocket = webSocketContext.WebSocket;
       while (webSocket.State == WebSocketState.Open)
       {
           await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes("Hello world")),
               WebSocketMessageType.Text, true, CancellationToken.None);
       }
   }
   signal.ReleaseMutex();
}

private void Button1_Click(object sender, EventArgs e)
{
   ws.WebSocketSend("Hello world");
}
//public WebSocket WS;
PanWebsite ws;
private void Button2_Click(object sender, EventArgs e)
{
   ws = new PanWebsite("http://localhost:2778/", (PanRequest r) => { return PanResponse.ReturnEmpty(); });
   ws.Start();
   console1.AppendText("website started\n");
   ws.onWebSocketRequest = (WebSocket webSocket, string msg) => {
       ////console1.AppendText("socket has connected\n");
       //WS = webSocket;
       //await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes("Hello world")),
       //                        WebSocketMessageType.Text, true, CancellationToken.None);
       //await Task.Factory.StartNew(async () =>
       //{
       //    ArraySegment<byte> bytes = new ArraySegment<byte>(new byte[1024]);
       //    while (webSocket.State == WebSocketState.Open)
       //    {
       //        //var a = webSocket.ReceiveAsync(bytes, CancellationToken.None);
       //        //MessageBox.Show(Encoding.UTF8.GetString(bytes.ToArray()));
       //        ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[1024]);
       //        WebSocketReceiveResult result = await webSocket.ReceiveAsync(
       //            bytesReceived, CancellationToken.None);
       //        MessageBox.Show(Encoding.UTF8.GetString(
       //            bytesReceived.Array, 0, result.Count));
       //        if (webSocket.State != WebSocketState.Open)
       //        {
       //            break;
       //            //socket.send("dddd");
       //        }
       //    }
       //});
       MessageBox.Show(msg);
   };
}
*/
    }
}
