using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalR
{
    [HubName("HubMessage")]
    public class MyHub : Hub
    {
        public override Task OnConnected()
        {
            int i = 0;

            //while (true)
            //{
            i = new Random().Next(0, 3);

            switch (i)
            {
                case 0:
                    Groups.Add(Context.ConnectionId, "Grupo01");
                    Clients.Group("Grupo01").groupInfo("Grupo01", Context.ConnectionId);
                    for (int a = 0; a < 500; a++)
                        Clients.Group("Grupo02").messageAdded("Grupo01", "Grupo01", "Mensagem do grupo 01");

                    break;
                case 1:
                    Groups.Add(Context.ConnectionId, "Grupo02");
                    Clients.Group("Grupo02").groupInfo("Grupo02", Context.ConnectionId);
                    for (int b = 0; b < 500; b++)
                        Clients.Group("Grupo03").messageAdded("Grupo02", "Grupo03", "Mensagem do grupo 02");
                    break;
                case 2:
                    Groups.Add(Context.ConnectionId, "Grupo03");
                    Clients.Group("Grupo03").groupInfo("Grupo03", Context.ConnectionId);
                    for (int c = 0; c < 500; c++)
                        Clients.Group("Grupo01").messageAdded("Grupo03", "Grupo01", "Mensagem do grupo 01");
                    break;
            }

            Task.Delay(5000);
            //}

            return base.OnConnected();
        }

        public void SendMessage(string remetente, string destinatario, string message)
        {
            Clients.Others.messageAdded(remetente, destinatario, message);
        }
    }
}