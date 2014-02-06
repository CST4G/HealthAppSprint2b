using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using healthApp.Controllers;
using healthApp.Models;
using System.Web.SessionState;



namespace healthApp.Hubs
{
    
    public class ChatHub : Hub
    {
        
        public void Send(string message)
        {
            


            Chats chat = new Chats();
            chat.Chat = message;
            chat.UserName = Context.User.Identity.Name.ToString();
            chat.DateAdded = DateTime.Now;
            

            ChatDBContext db = new ChatDBContext();
            chat.addToDB(db);

          

            Clients.All.addNewMessageToPage(chat.UserName, message);
        }
    }
}