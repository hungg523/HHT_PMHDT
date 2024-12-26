// Chat Box
const apiBase = "https://localhost:1006";
const customerId = localStorage.getItem("customerId");
let conversationId = null;

const connection = new signalR.HubConnectionBuilder()
  .withUrl(`${apiBase}/chat`)
  .build();

connection.start()
  .then(() => console.log("Connected to SignalR Hub"))
  .catch(err => console.error("Error connecting to SignalR Hub:", err));

connection.on("ReceiveMessage", (from, message) => {
  const messagesContainer = document.getElementById("messages");

  const messageDiv = document.createElement("div");
  messageDiv.style.marginBottom = "10px";
  messageDiv.innerHTML = `
	<strong >${from}:</strong> ${message} <br>
	<small>${new Date().toLocaleString()}</small>
  `;

  messagesContainer.appendChild(messageDiv);
  messagesContainer.scrollTop = messagesContainer.scrollHeight;
});

document.getElementById("chatButton").addEventListener("click", async () => {
  const chatWindow = document.getElementById("chatWindow");

  if (chatWindow.style.display === "block") {
	  chatWindow.style.display = "none";

	  if (conversationId) {
		  connection.invoke("LeaveConversation", conversationId)
			  .then(() => console.log(`Left conversation ${conversationId}`))
			  .catch(err => console.error("Error leaving conversation:", err));
	  }

	  return;
  }

  const existingConversation = await fetch(`${apiBase}/get-detail-message-by-customer-id?customerId=${customerId}`)
	  .then(response => (response.ok ? response.json() : null))
	  .catch(() => null);

  if (existingConversation && existingConversation.conversationId) {
	  conversationId = existingConversation.conversationId;
  } else {
	  const response = await fetch(`${apiBase}/create-conversation?customerId=${customerId}`, { method: "POST" });
	  const conversation = await response.json();
	  conversationId = conversation.conversationId;
  }

  if (conversationId) {
	  connection.invoke("JoinConversation", conversationId)
		  .then(() => console.log(`Joined conversation ${conversationId}`))
		  .catch(err => console.error("Error joining conversation:", err));
  }

  chatWindow.style.display = "block";
  loadMessages(); 
});


async function loadMessages() {
  if (!conversationId) return;
  try {
	const response = await fetch(`${apiBase}/get-detail-message-by-customer-id?customerId=${customerId}`);
	const chatData = await response.json();
	const messages = chatData.messages;

	const messagesContainer = document.getElementById("messages");
	messagesContainer.innerHTML = ""; // Xóa nội dung cũ

	if (messages && messages.length > 0) {
	  // Duyệt qua danh sách tin nhắn và hiển thị
	  messages.forEach(msg => {
		const messageDiv = document.createElement("div");
		messageDiv.style.marginBottom = "10px";
		messageDiv.innerHTML = `
		  <strong>${msg.from}:</strong> ${msg.message} <br>
		  <small>${new Date(msg.timeSend).toLocaleString()}</small>
		`;
		messagesContainer.appendChild(messageDiv);
	  });
	  messagesContainer.scrollTop = messagesContainer.scrollHeight; // Cuộn xuống cuối
	} else {
	  messagesContainer.innerHTML = "<p>Chưa có tin nhắn nào.</p>";
	}
  } catch (error) {
	console.error("Lỗi khi tải tin nhắn:", error);
	document.getElementById("messages").innerHTML = "<p>Lỗi khi tải tin nhắn.</p>";
  }
}

document.getElementById("sendMessageButton").addEventListener("click", async () => {
  const message = document.getElementById("messageInput").value;
  if (message && conversationId) {
	// Gửi tin nhắn qua API
	await fetch(`${apiBase}/user-send-message?conversionId=${conversationId}`, {
	  method: "POST",
	  headers: { "Content-Type": "application/json" },
	  body: JSON.stringify({ message })
	});

	// Gửi tin nhắn qua SignalR để các client khác nhận được ngay
	connection.invoke("SendMessage", "Customer", message, conversationId)
	  .catch(err => console.error("Error sending message via SignalR:", err));

	document.getElementById("messageInput").value = ""; // Xóa nội dung input
  }
});

