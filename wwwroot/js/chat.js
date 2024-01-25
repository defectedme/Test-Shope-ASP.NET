"use strict";



var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();



//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message, time) {
    var li = document.createElement("li");



    document.getElementById("messagesList").appendChild(li);

    //this makes showes new chat at first! nice 
    $('#messages').scrollTop($('#messages')[0].scrollHeight);

    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${user} says: ${message} .Time: ${time}`;
    chatHistory.li = li.scrollHeight;

});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    document.getElementById("sendButton").addEventListener("click", () => { widnow.scrollTo(0, document.body.scrollHeight); })

}).catch(function (err) {
    return console.error(err.toString());
});





document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var receiver = document.getElementById("receiverInput").value;


    if (receiver.length > 1) {

        connection.invoke("SendMessage", receiver, message).catch(function (err) {
            return console.error(err.toString());
        });
    }
    else {
        //        //send message to all user.  
        connection.invoke("SendMessageToAll", message).catch(function (err) {
            return console.error(err.toString());
        });


    }

    event.preventDefault();
    document.getElementById("sendButton").addEventListener("click", () => { widnow.scrollTo(0, document.body.scrollHeight); })

});

