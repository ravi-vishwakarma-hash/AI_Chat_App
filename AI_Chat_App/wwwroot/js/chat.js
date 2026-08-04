const chats = [];

let currentChatId = null;

const history = document.querySelector(".history");
const messages = document.querySelector(".messages");
const textarea = document.querySelector("textarea");

const sendButton = document.querySelector(".input-area button");
const newChatButton = document.querySelector(".new-chat");


// New Chat
newChatButton.addEventListener("click", createChat);

// Send Message
sendButton.addEventListener("click", sendMessage);

// Press Enter
textarea.addEventListener("keydown", function (e) {

    if (e.key === "Enter" && !e.shiftKey) {

        e.preventDefault();

        sendMessage();
    }

});

// Auto Resize
textarea.addEventListener("input", function () {

    this.style.height = "auto";

    this.style.height = this.scrollHeight + "px";

});

createChat();

function createChat() {

    const id = Date.now();

    const chat = {

        id,

        title: "New Chat",

        messages: []

    };

    chats.unshift(chat);

    currentChatId = id;

    renderHistory();

    renderMessages();

}

function sendMessage() {

    const text = textarea.value.trim();

    if (text === "")
        return;

    const chat = getCurrentChat();

    if (!chat)
        return;

    if (chat.messages.length === 0)
        chat.title = text.substring(0, 30);

    chat.messages.push({

        role: "user",

        content: text

    });

    renderHistory();

    renderMessages();

    textarea.value = "";

    textarea.style.height = "auto";

    // Fake AI response

    setTimeout(() => {

        chat.messages.push({

            role: "ai",

            content: "This is an AI response for: " + text

        });

        renderMessages();

    }, 700);

}

function renderHistory() {

    history.innerHTML = "";

    chats.forEach(chat => {

        const item = document.createElement("div");

        item.className = "history-item";

        if (chat.id === currentChatId)
            item.classList.add("active");

        item.innerHTML = `

            <span>${chat.title}</span>

            <button class="delete">🗑</button>

        `;

        item.querySelector("span").onclick = () => {

            currentChatId = chat.id;

            renderHistory();

            renderMessages();

        };

        item.querySelector(".delete").onclick = (e) => {

            e.stopPropagation();

            deleteChat(chat.id);

        };

        history.appendChild(item);

    });

}

function deleteChat(id) {

    const index = chats.findIndex(x => x.id === id);

    if (index === -1)
        return;

    chats.splice(index, 1);

    if (currentChatId === id) {

        if (chats.length > 0)
            currentChatId = chats[0].id;
        else
            createChat();

    }

    renderHistory();

    renderMessages();

}

function renderMessages() {

    messages.innerHTML = "";

    const chat = getCurrentChat();

    if (!chat)
        return;

    chat.messages.forEach(m => {

        const div = document.createElement("div");

        div.className = "message " + m.role;

        // div.innerText = m.content;

        const html = marked.parse(m.content);

        div.innerHTML = DOMPurify.sanitize(html);

        messages.appendChild(div);

    });

    messages.scrollTop = messages.scrollHeight;

}

function getCurrentChat() {

    return chats.find(x => x.id === currentChatId);

}


const sidebar = document.querySelector(".sidebar");

const overlay = document.querySelector(".overlay");

const menuBtn = document.getElementById("menuBtn");

menuBtn.addEventListener("click", () => {

    sidebar.classList.toggle("open");

    overlay.classList.toggle("show");

});

overlay.addEventListener("click", closeSidebar);

function closeSidebar() {

    sidebar.classList.remove("open");

    overlay.classList.remove("show");

}