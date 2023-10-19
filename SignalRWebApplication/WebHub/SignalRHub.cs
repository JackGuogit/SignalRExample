using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace SignalRWebApplication.WebHub
{
    [Authorize(Roles ="Admin")]
    public class SignalRHub:Hub
    {
        public SignalRHub()
        {
        }

        public void ReciveMsgByClient(string msg)
        {
            Debug.WriteLine(msg);
        }



        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }


    }
}
