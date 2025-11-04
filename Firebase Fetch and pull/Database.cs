using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using System;

public class Database : MonoBehaviour
{
    private DatabaseReference reference;
    [SerializeField]private TMP_InputField Name;
    [SerializeField] private TMP_InputField Age;
    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void writeNewUser(string userId, string name, int age)
    {
        User user = new User(name, age);
        string json = JsonUtility.ToJson(user);
        reference.Child("users").Child(userId).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("✅ User data written successfully!");
                }
                else
                {
                    Debug.LogError("❌ Failed to write user data: " + task.Exception);
                }
            });
    }

    public void sendData()
    {
        reference.Child("users").OrderByKey().LimitToLast(1).GetValueAsync().ContinueWithOnMainThread(task =>

        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                int newId = 1;
                if (snapshot.Exists)
                {
                    foreach (var child in snapshot.Children)
                    {
                        int lastId = int.Parse(child.Key);
                        newId = lastId + 1;
                    }
                }
                string id = newId.ToString();
                string name = Name.text;
                int age = int.Parse(Age.text);
                writeNewUser(id, name, age);
            }
            else
            {
                Debug.LogError("❌ Failed to get last ID: " + task.Exception);
            }
        });
    }
    
}


public class User
{
    public string username;
    public int age;
    public User(string username, int age)
    {
        this.username = username;
        this.age = age;
    }
}