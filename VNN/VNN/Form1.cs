using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VNN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //
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
