using System.Net;
using System.Net.Sockets;
using System.Text;

int PORT = 1234;
int BUFFER_SIZE = 1024;
string HELP_INFO = "This is a simple chat room app.\nType \"/rename <name>\" to change your name.\n";

List<Socket> socketList = new();
Socket host = new(SocketType.Stream, ProtocolType.Tcp);
host.Bind(new IPEndPoint(IPAddress.Parse("0.0.0.0"), PORT));
host.Listen();
Log("Server started.");

for (;;)
{
    Socket socket = host.Accept();
    socketList.Add(socket);
    _ = HandleClient(socket);
}

async Task HandleClient(Socket socket)
{
    byte[] buffer = new byte[BUFFER_SIZE];
    int msgSize;
    string name = Guid.NewGuid().ToString();
    Log($"{name} connected.");
    while ((msgSize = await socket.ReceiveAsync(buffer)) > 0)
    {
        string message = Encoding.UTF8.GetString(buffer, 0, msgSize).Trim();
        Log($"{name}: {message}");
        if (message.StartsWith('/'))
        {
            HandleCommand(socket, message, ref name);
        }
        else
        {
            await HandleMessage(socket, $"{name}: {message}\n");
        }
    }
    socketList.Remove(socket);
    socket.Disconnect(true);
    Log($"{name} disconnected.");
}

void HandleCommand(Socket socket, string msg, ref string name)
{
    string[] commands = msg.Split(' ');
    switch (commands[0])
    {
        case "/help": socket.Send(Encoding.UTF8.GetBytes(HELP_INFO)); break;
        case "/rename": name = commands[1]; break;
        default: break;
    }
}

async Task HandleMessage(Socket socket, string msg)
{
    byte[] bytes = Encoding.UTF8.GetBytes(msg);
    foreach (Socket s in socketList)
    {
        if (s == socket) continue;

        await s.SendAsync(bytes);
    }
}

void Log(string msg)
{
    Console.WriteLine($"{DateTime.UtcNow:yyyy-MM-dd hh:mm:ss}: {msg}");
}