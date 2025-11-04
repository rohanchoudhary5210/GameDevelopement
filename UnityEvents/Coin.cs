using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public UnityEvent coinCollect;
   void Start()
    {
        coinCollect.AddListener(GameObject.FindGameObjectWithTag("UiManager").GetComponent<UiManager>().UpdateScore);
        coinCollect.AddListener(test);
    }


    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            coinCollect?.Invoke();
            Destroy(gameObject);
        }       
    }

    public void test()
    {
        Debug.Log("Coin Received");
    }
}
