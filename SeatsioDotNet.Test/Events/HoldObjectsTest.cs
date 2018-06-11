﻿using System.Collections.Generic;
using FluentAssertions;
using SeatsioDotNet.Events;
using SeatsioDotNet.HoldTokens;
using Xunit;

namespace SeatsioDotNet.Test.Events
{
    public class HoldObjectsTest : SeatsioClientTest
    {
        [Fact]
        public void Test()
        {
            var chartKey = CreateTestChart();
            var evnt = Client.Events.Create(chartKey);
            HoldToken holdToken = Client.HoldTokens.Create();

            var result = Client.Events.Hold(evnt.Key, new[] {"A-1", "A-2"}, holdToken.Token);

            var status1 = Client.Events.RetrieveObjectStatus(evnt.Key, "A-1");
            Assert.Equal(ObjectStatus.Held, status1.Status);
            Assert.Equal(holdToken.Token, status1.HoldToken);

            var status2 = Client.Events.RetrieveObjectStatus(evnt.Key, "A-2");
            Assert.Equal(ObjectStatus.Held, status2.Status);
            Assert.Equal(holdToken.Token, status2.HoldToken);

            result.Labels.Should().BeEquivalentTo(new Dictionary<string, Labels>
            {
                {"A-1", new Labels("1", "seat", "A", "row")},
                {"A-2", new Labels("2", "seat", "A", "row")}
            });
        }

        [Fact]
        public void OrderId()
        {
            var chartKey = CreateTestChart();
            var evnt = Client.Events.Create(chartKey);
            HoldToken holdToken = Client.HoldTokens.Create();

            Client.Events.Hold(evnt.Key, new[] {"A-1", "A-2"}, holdToken.Token, "order1");

            Assert.Equal("order1", Client.Events.RetrieveObjectStatus(evnt.Key, "A-1").OrderId);
            Assert.Equal("order1", Client.Events.RetrieveObjectStatus(evnt.Key, "A-2").OrderId);
        }
    }
}