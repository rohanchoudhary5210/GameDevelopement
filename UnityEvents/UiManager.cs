using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI score;
    public int scores = 0;
    void Start()
    {
      
    }

    public void UpdateScore()
    {scores++;
        score.text = scores.ToString();
        
    } 
}
