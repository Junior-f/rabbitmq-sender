using System;
public interface IRabbitMqConnection
{

    IRabbitMqConnection NewConnection(string host, string userName, string password);
    IDisposable NewConnection();

}


