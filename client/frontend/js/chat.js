let currentRoomId = '';
const accessToken = localStorage.getItem('accessToken')

const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:3000/chatHub", {accessTokenFactory: () => {
        console.log(localStorage.getItem('accessToken'))
        return localStorage.getItem('accessToken')}})
    .configureLogging(signalR.LogLevel.Debug)
    .build();


const start = async () => {
    try{
        await connection.start();
    } catch (error){
        console.log(error);
    }
}

const formatDateTime = (dateTimeToFormat) => {
    const dateTime = new Date(dateTimeToFormat);

    const date = dateTime.toLocaleDateString();
    const time = dateTime.toLocaleTimeString();

    return `${date}-${time}`
}

const receivedMessageHandler = (receivedMessage) => {
    console.log(receivedMessage);
    const chatBodyDiv = document.getElementById('chat_chat-body');
    const messageDiv = document.createElement('div');
    messageDiv.classList.add('chat_message')
    const headerElement = document.createElement('header');
    headerElement.textContent = receivedMessage.messengerUser.username;
    const textElement = document.createElement('p');
    textElement.textContent = receivedMessage.text;
    const footerElement = document.createElement('footer');

    footerElement.textContent = formatDateTime(receivedMessage.createdAt);
    messageDiv.append(headerElement, textElement, footerElement);
    chatBodyDiv.appendChild(messageDiv);
}

const renderRoom = (roomToRender) => {
    console.log(roomToRender);
    const chatBodyDiv = document.getElementById('chat_chat-body');
    chatBodyDiv.innerHTML = '';
    const chatDiv = document.getElementById('chat_chat');
    chatDiv.removeAttribute('hidden');

    roomToRender.messages.reverse().forEach(message => {
        receivedMessageHandler(message);
    })
}

const joinRoom = (chatRoomId) => {
    console.log(chatRoomId)
    connection.invoke("JoinRoom",chatRoomId,{})
    .then((result) => {
        renderRoom(result);
        connection.on("SendMessage", receivedMessageHandler);
    })
}

const send = (chatRoomId, text) => {
    const messageToSend = {text: text}
    connection.invoke("SendMessage", chatRoomId, messageToSend)
    .then((result) => {
        console.log(result);
    })
}

const leaveRoom = (chatRoomId) => {
    connection.send("LeaveRoom", chatRoomId)
    .then(() =>{
        console.log("Leaved");
        connection.off("SendMessage", receivedMessageHandler)
    });
}

const newMessageHandler = () => {
    const messageInput = document.getElementById('chat_message-input');
    const text = messageInput.value;
    if (!text == ''){
        send(currentRoomId, text)
    }
}

const fetchUsersRooms = () => {
    const uri = `http://localhost:3000/api/1/chat-rooms`;
    fetch(uri, {
        method : 'GET',
        headers: {Authorization: `Bearer ${localStorage.getItem('accessToken')}`}
    })
        .then(response => response.json())
        .then(data => displayRooms(data))
        .catch(error => console.error("Unable to get items", error))
}

const handleRoomClick = (roomId) => {
    console.log(roomId);
    const uri = `http://localhost:3000/api/1/chat-rooms/${roomId}`;
    fetch(uri, {
        method : 'GET',
        headers: {Authorization: `Bearer ${localStorage.getItem('accessToken')}`}
    })
        .then(response => response.json())
        .then(data => displayRoom(data))
        .catch(error => console.error("Unable to get items", error))
}

const displayRooms = (rooms) => {
    const sideMenu = document.getElementById('chat_side-menu');
    console.log(sideMenu)
    rooms.forEach(element => {
        console.log(element)
        const roomButton = document.createElement('a');
        roomButton.classList.add('chat_room-button');
        roomButton.setAttribute('data-link', null)
        roomButton.href = element.id;
        roomButton.textContent = element.title;

        roomButton.addEventListener('click', () => {
            handleRoomClick(element.id);
        })

        sideMenu.appendChild(roomButton);
    });
}

const displayRoom = (room) => {
    if(currentRoomId != ''){
        leaveRoom(currentRoomId);
        currentRoomId = '';
    }

    joinRoom(room.id);
    currentRoomId = room.id;
}

const addDefaultHandlers = () => {
    document.getElementById('chat_send-button').addEventListener('click', newMessageHandler);
}

export const disposeChat = () => {
    document.getElementById('chat_send-button').removeEventListener('click', newMessageHandler);
}

export const startChat = async () => {
    await start();
    fetchUsersRooms();
    addDefaultHandlers();
}