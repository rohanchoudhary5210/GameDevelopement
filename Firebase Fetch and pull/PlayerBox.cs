using UnityEngine;
using TMPro;

public class PlayerBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI age;
    public void BindUserData(string username,int userAge)
    {
        name.text = username;
        age.text = userAge.ToString();
    }
}
