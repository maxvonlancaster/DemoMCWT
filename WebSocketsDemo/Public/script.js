let connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7160/chat").build();

connection.on("ReceiveMessage", function (user, message) {
    let messages = document.getElementById("messages");
    let li = document.createElement("li");
    li.textContent = `${user}: ${message}`;
    messages.appendChild(li);
});

async function start() {
    await connection.start();
    console.log("SignalR Connected.");
}

document.getElementById("send").addEventListener("click", async function (event) {
    let user = document.getElementById("user").value;
    let message = document.getElementById("message").value;

    await connection.invoke("SendMessage", user, message);
});

start();