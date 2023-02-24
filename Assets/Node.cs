using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;
using MongoDB.Bson; 
using MongoDB.Driver;
using UnityEngine.Serialization;

public class Node: MonoBehaviour{
    // Start is called before the first frame update
    private MongoClient client = new MongoClient("mongodb://localhost:27017");
    private IMongoDatabase database;
    private IMongoCollection<BsonDocument> collection;
    public GameObject nodeObject;
    public BsonValue nodeStatus;

    void Start(){
        database = client.GetDatabase("unityDB");
        collection = database.GetCollection<BsonDocument>("test");
        GetNodeInfo();

        // foreach (var index in document){
        //     Debug.Log(index["NodeA"]);
        // }
        // Debug.Log(document[0]["NodeA"]);
        //
        // Debug.Log("123");
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void GetNodeInfo(){
        var filter = Builders<BsonDocument>.Filter.Eq("type", "flowControl");
        var document = collection.Find(filter).ToList(); 
        // print(document[0]["NodeA"]);
        nodeStatus = document[0][nodeObject.name];
        if (nodeStatus == "True"){
            nodeObject.SetActive(true);
        }
        else{
            nodeObject.SetActive(false);
        }
    }

    public void ChangeNodeStatus(){
        string status = "";
        if (nodeStatus == "True"){
            status = "False";
        }
        else{
            status = "True";
        }
        var filter = Builders<BsonDocument>.Filter.Eq("type", "flowControl");
        var update = Builders<BsonDocument>.Update.Set(nodeObject.name, status);
        var result = collection.UpdateOne(filter, update);

    }
    
}