// Logging.cs
// created by Yaroslav Nevmerzhytskiy
// e-mail: y.nevmerzhytskiy@twinwingames.com
// Copyright 2017 Luck Genome

using System;
using UnityEngine;

namespace LGPlatform.Log
{
    public static class Logging
    {
        private const String LogFormatDefault = "<color={0}>[{1}]</color> {2}";
        private const String LogFormatWithTimestamp = "<color={0}>[{1}][{2}]</color> {3}";

        public enum Level
        {
            Message,
            Warning,
            Error
        }

        [Flags]
        public enum Channel
        {
            None = 0x00,
            Core = 0x01,
            Network = 0x02,
            Game = 0x04,
            UI = 0x08,
            Sounds = 0x16,
            Facebook = 0x32
        }

        public static Channel LogMask;

        public static void Log(Level level, Channel channel, String message, Boolean addTimeStamp = false)
        {
            if((LogMask & channel) == 0)
                return;

            String fMessage = String.Format(LogFormatDefault, GetChannelColor(channel), channel, message);
            if(addTimeStamp)
            {
                fMessage = String.Format(LogFormatWithTimestamp, GetChannelColor(channel), channel,
                                         DateTime.Now.ToString("HH:mm:ss:fff"),
                                         message);
            }

            switch(level)
            {
                case Level.Error:
                    Debug.LogError(fMessage);
                    break;
                case Level.Warning:
                    Debug.LogWarning(fMessage);
                    break;
                case Level.Message:
                    Debug.Log(fMessage);
                    break;
            }
        }

        private static String GetChannelColor(Channel channel)
        {
            switch(channel)
            {
                case Channel.Core:
                    return "yellow"; //Color.yellow;
                case Channel.Network:
                    return "cyan"; //Color.cyan;
                case Channel.Game:
                    return "blue"; //Color.blue;
                case Channel.UI:
                    return "grey"; //Color.grey;
                case Channel.Sounds:
                    return "green"; //Color.green; 
                default:
                    return "white"; //Color.white;
            }
        }
    }
}
