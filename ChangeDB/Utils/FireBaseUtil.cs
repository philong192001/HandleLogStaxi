using ChangeDB.Models;
using FirebaseAdmin.Messaging;
using MongoDB.Bson;
using System.Text.Json;

namespace ChangeDB.Utils;

public class FireBaseUtil
{
    public static string SendNotification(string title, string message,string token,FirebaseArticleDatas firebaseArticleDatas)
    {
        try
        {

            var notification = new Message
            {

                Notification = new Notification
                {
                    Title = title,
                    Body = message,

                },
                Data = new Dictionary<string, string>()
                {
                    { "type", "7" },
                    { "datas", firebaseArticleDatas.ToJson() }
                },
                Android = new AndroidConfig()
                {
                    Notification = new AndroidNotification()
                    {
                        Sound = "default"
                    }
                },
                Apns = new ApnsConfig()
                {
                    Headers = new Dictionary<string, string>()
                    {
                        {"apns-priority", "10"}
                    },
                    Aps = new Aps()
                    {
                        ContentAvailable = true,
                        Sound = "default"
                    }
                },
                Token = token
            };
            var response = FirebaseMessaging.DefaultInstance.SendAsync(notification).Result;
            return response;
        }
        catch (Exception ex)
        {
            return $"Failed to send notification: {ex.Message}";
        }
    }
}

public class FirebaseArticleDatas
{
    public string LinkArticle { get; set; }
}