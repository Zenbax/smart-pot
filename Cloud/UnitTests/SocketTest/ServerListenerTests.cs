using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Domain.Model;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using YourApiNamespace.Controllers;

public class ServerListenerTests
{
    private Mock<IMongoCollection<Pot>> _mockPotCollection;
    private Mock<IMongoCollection<SensorData>> _mockSensorDataCollection;

    [SetUp]
    public void Setup()
    {
        _mockPotCollection = new Mock<IMongoCollection<Pot>>();
        _mockSensorDataCollection = new Mock<IMongoCollection<SensorData>>();

        // Set the private static fields using reflection
        var potField = typeof(ServerListener).GetField("potCollection",
            System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
        potField.SetValue(null, _mockPotCollection.Object);

        var sensorDataField = typeof(ServerListener).GetField("sensorDataCollection",
            System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
        sensorDataField.SetValue(null, _mockSensorDataCollection.Object);
    }
}