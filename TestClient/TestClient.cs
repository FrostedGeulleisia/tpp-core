﻿using System;
using System.Threading;
using TPPCommon.Logging;
using TPPCommon.PubSub;
using TPPCommon.PubSub.Events;

namespace TestClient
{
    /// <summary>
    /// Test service that subscribes to some pub-sub topics and prints out the published events, while keeping
    /// track of the total number of events it receives across all topics.
    /// </summary>
    class TestClient
    {
        private ISubscriber Subscriber;
        private IPublisher Publisher;
        private ITPPLoggerFactory LoggerFactory;
        private TPPLoggerBase Logger;
        private int TotalEventsReceived;

        public TestClient(ISubscriber subscriber, IPublisher publisher, ITPPLoggerFactory loggerFactory)
        {
            this.Subscriber = subscriber;
            this.Publisher = publisher;
            this.LoggerFactory = loggerFactory;
            this.Logger = this.LoggerFactory.Create("test_client");
            this.TotalEventsReceived = 0;
        }

        public void Run()
        {
            this.Logger.LogInfo("Running Subscriber client...");

            // Subscribe to the pub-sub topics, and assign event handler functions for each topic.
            this.Subscriber.Subscribe<SongInfoEvent>(OnSongInfoChanged);
            this.Subscriber.Subscribe<SongPausedEvent>(OnSongPaused);

            // Run forever.
            while (true)
            {
                Thread.Sleep(100);
            }
        }

        void OnSongInfoChanged(SongInfoEvent @event)
        {
            this.TotalEventsReceived += 1;
            this.Logger.LogInfo($"Song Info:  Id = {@event.Id}, Title = '{@event.Title}', Artist = '{@event.Artist}'");
            this.Logger.LogWarning($"Total Events Received: {this.TotalEventsReceived}");
        }

        void OnSongPaused(SongPausedEvent @event)
        {
            this.TotalEventsReceived += 1;
            this.Logger.LogInfo($"Song was paused!");
            this.Logger.LogWarning($"Total Events Received: {this.TotalEventsReceived}");
        }
    }
}