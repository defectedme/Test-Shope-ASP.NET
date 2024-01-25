using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Test_Shope_ASP.NET.Models;
using Microsoft.AspNetCore.Mvc;
using Test_Shope_ASP.NET.Context;
using NuGet.Protocol;
using System.Collections.Immutable;
using System.Web.WebPages;
using System.Linq;
using Test_Shope_ASP.NET.Context.Services;

namespace Test_Shope_ASP.NET.Hubs
{
    public class ChatHub : Hub
    {


        //define a dictionary to store the userid.  
        private static Dictionary<string, List<string>> NtoIdMappingTable = new Dictionary<string, List<string>>();

        public override async Task OnConnectedAsync()
        {
            var username = Context.User.Identity.Name;
            var userId = Context.UserIdentifier;
            List<string> userIds;

            //store the userid to the list.  
            if (!NtoIdMappingTable.TryGetValue(username, out userIds))
            {
                userIds = new List<string>();
                userIds.Add(userId);

                NtoIdMappingTable.Add(username, userIds);
            }
            else
            {
                userIds.Add(userId);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var username = Context.User.Identity.Name;
            var userId = Context.UserIdentifier;
            List<string> userIds;

            //remove userid from the List  
            if (NtoIdMappingTable.TryGetValue(username, out userIds))
            {
                userIds.Remove(userId);
            }
            await base.OnDisconnectedAsync(exception);
        }


        public async Task SendMessageToAll( string message)
        {

            var date = DateTime.Now.ToString();
            await Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name, message, date);

        }


        public async Task SendMessage(string receiver, string message)
        {

            var userId = NtoIdMappingTable.GetValueOrDefault(receiver);
            var date = DateTime.Now.ToString();
            await Clients.User(userId.FirstOrDefault()).SendAsync("ReceiveMessage", Context.User.Identity.Name, message, date);

        }



    }

}
