using MongoDB.Bson;

namespace VersineUser;

public class User
{
    public String username;
    public String password;
    public String ticket;
    public Int32 ticketCount;
    public String avatar;
    public String bio;
    public String banner;
    public String color;
    public List<BsonObjectId> friends;
    public List<BsonObjectId> incomingFriendRequests;
    public List<BsonObjectId> outgoingFriendRequests;
    
    public User(string username, string password, string ticket)
    {
        this.username = username;
        this.password = password;
        this.ticket = ticket;
        ticketCount = 10;
        avatar = "https://i.imgur.com/k7eDNwW.jpg";
        bio = "Hey, I'm using Versine!";
        banner = "https://i.imgur.com/iaD9ttC.png";
        color = "#28DBB7";
        friends = new List<BsonObjectId>();
        incomingFriendRequests = new List<BsonObjectId>();
        outgoingFriendRequests = new List<BsonObjectId>();
    }

    public User(BsonDocument document)
    {
        friends = new List<BsonObjectId>();
        BsonValue[] bsonFriends = document.GetElement("friends").Value.AsBsonArray.ToArray();
        foreach (var bsonFriend in bsonFriends)
        {
            friends.Add(bsonFriend.AsObjectId);
        }
        
        incomingFriendRequests = new List<BsonObjectId>();
        BsonValue[] bsonIncomingFriendRequests = document.GetElement("incomingFriendRequests").Value.AsBsonArray.ToArray();
        foreach (var bsonIncomingFriendRequest in bsonIncomingFriendRequests)
        {
            incomingFriendRequests.Add(bsonIncomingFriendRequest.AsObjectId);
        }
        
        outgoingFriendRequests = new List<BsonObjectId>();
        BsonValue[] bsonOutgoingFriendRequests = document.GetElement("outgoingFriendRequests").Value.AsBsonArray.ToArray();
        foreach (var bsonOutgoingFriendRequest in bsonOutgoingFriendRequests)
        {
            outgoingFriendRequests.Add(bsonOutgoingFriendRequest.AsObjectId);
        }
        
        username = document.GetElement("username").Value.AsString;
        password = document.GetElement("password").Value.AsString;
        ticket = document.GetElement("ticket").Value.AsString;
        ticketCount = document.GetElement("ticketCount").Value.AsInt32;
        avatar = document.GetElement("avatar").Value.AsString;
        bio = document.GetElement("bio").Value.AsString;
        banner = document.GetElement("banner").Value.AsString;
        color = document.GetElement("color").Value.AsString;
    }

    public BsonDocument ToBson()
    {
        return new BsonDocument((IEnumerable<BsonElement>)
            new BsonElement[] 
            { 
                new ("username", username),
                new ("password", password),
                new ("ticket", ticket),
                new ("ticketCount", ticketCount),
                new ("avatar", avatar),
                new ("bio", bio),
                new ("banner", banner),
                new ("color", color),
                new ("friends", new BsonArray(friends)),
                new ("incomingFriendRequests", new BsonArray(incomingFriendRequests)),
                new ("outgoingFriendRequests", new BsonArray(outgoingFriendRequests)) 
            });
    }
}
