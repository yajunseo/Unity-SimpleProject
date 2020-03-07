using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public PlayerMove player;

    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject Restart;

    // Start is called before the first frame update
    public void NextStage()
    {
        stageIndex++;

        totalPoint += stagePoint;
        stagePoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HealthDown();
            collision.attachedRigidbody.velocity = Vector2.zero;
            collision.transform.position = new Vector3(0, 0, -1);

        }
    }
    public void HealthDown()
    {
        if (health > 1)
        {
            health--;
            UIhealth[health].color = new Color(0, 0, 0, 0.4f);

        }
        else
        {
            player.OnDie();
            Restart.SetActive(true);
            Text btnText = Restart.GetComponentInChildren<Text>();
        }

    }

    public void ReStart()
    {
        SceneManager.LoadScene(0);
    }

   
}
