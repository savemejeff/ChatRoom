## Chat Room

This is a simple chat room server over TCP protocol. 

### Start server

Run `dotnet run`. And the server will start listen on `0.0.0.0:1234`.

### Client

You can use `telnet` as a client to connect to the server.  
Run `telnet 127.0.0.1 1234`.  
Then type any message and press enter to send.  

### Command

For now only supports one command `/rename <name>`.  

### Demo

Client1
```
~/ChatRoom$ telnet 127.0.0.1 1234
Trying 127.0.0.1...
Connected to 127.0.0.1.
Escape character is '^]'.
/rename Rose
Hi.
Mike: Hi. How are you?
^]

telnet> quit
Connection closed.
```
Client2
```
~/ChatRoom$ telnet 127.0.0.1 1234
Trying 127.0.0.1...
Connected to 127.0.0.1.
Escape character is '^]'.
/rename Mike
Rose: Hi.
Hi. How are you?
```
Server
```
~/ChatRoom$ dotnet run
2024-02-27 01:10:57: Server started.
2024-02-27 01:11:12: cf33b869-08ec-4d09-bedd-319048b31434 connected.
2024-02-27 01:11:25: 884241ed-f9ff-4830-8547-94569483141d connected.
2024-02-27 01:11:35: 884241ed-f9ff-4830-8547-94569483141d: /rename Mike
2024-02-27 01:11:45: cf33b869-08ec-4d09-bedd-319048b31434: /rename Rose
2024-02-27 01:11:48: Rose: Hi.
2024-02-27 01:12:07: Mike: Hi. How are you?
2024-02-27 01:16:55: Rose disconnected.
```