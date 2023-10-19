using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Diagnostics;

namespace SignalRConsoleApp
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            
            HubConnection hubConnection=new HubConnectionBuilder()
                .WithUrl("https://localhost:7132/signalr")
                //自动重连频率，也可以通过实现IRetryPolicy进行配置
                .WithAutomaticReconnect(new[] { TimeSpan.Zero,
                    TimeSpan.Zero,
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                    TimeSpan.FromSeconds(20),
                    TimeSpan.FromSeconds(10),
                    TimeSpan.FromMinutes(1),
                    TimeSpan.FromMinutes(3),
                    TimeSpan.FromMinutes(5) })
                .Build();

            hubConnection.Reconnected += connectionId =>
            {
                Debug.Assert(hubConnection.State == HubConnectionState.Connected);

                // Notify users the connection was reestablished.
                // Start dequeuing messages queued while reconnecting if any.

                Console.WriteLine("WS重新链接成功");

                return Task.CompletedTask;
            };

            

            hubConnection.On<string>("ReciveMsg", (msg) =>
            {
                Debug.WriteLine(msg);
                hubConnection.SendAsync("ReciveMsgByClient","Hi!:from client");

            });


            Task task = Task.Run(async () => { await hubConnection.StartAsync(); });

            task.Wait();
            if (task.IsCompleted)
            {
                Debug.WriteLine("已建立连接");
            }
            Console.ReadLine();
        }
    }
}