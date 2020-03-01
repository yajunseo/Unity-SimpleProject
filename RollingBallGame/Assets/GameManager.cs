using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public int TotalItemCount;
    public int stage;
    public Text StageItemText;
    public Text PlayerItemText;

    void Awake()
    {
        StageItemText.text = "/" + TotalItemCount;
    }

    public void GetItem(int count)
    {
        PlayerItemText.text = count.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            SceneManager.LoadScene(stage);

    }
}
