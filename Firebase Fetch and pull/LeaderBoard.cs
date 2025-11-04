using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using System;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private GameObject boardRow;
    [SerializeField] private Transform initialpos;
    private DatabaseReference reference;
    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
     public void GenerateData()
    {
        reference.Child("users").OrderByChild("age").LimitToLast(10).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("❌ Failed to fetch data: " + task.Exception);
                return;
            }
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (!snapshot.Exists)
                {
                    Debug.Log("⚠️ No data found in database.");
                    return;
                }

                int count = 0;
                foreach (var child in snapshot.Children)
                {
                    string id = child.Key;
                    string json = child.GetRawJsonValue();
                    User user = JsonUtility.FromJson<User>(json);
                    if (user == null) continue;

                    // Instantiate and bind data
                    var rowGO = Instantiate(boardRow, initialpos);
                    var box = rowGO.GetComponent<PlayerBox>();
                    box.BindUserData(user.username, user.age);
                    count++;
                }
                Debug.Log($"✅ Returned {count} entries (might be less than 10).");
            }

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
