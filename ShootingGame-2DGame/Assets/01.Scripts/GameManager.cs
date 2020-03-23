using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    
    public bool isGameOver;

    public int score;

    public int playerLife = 3;

    [SerializeField] GameObject playerPrefab;

    [SerializeField] Texture2D[] numTextures;
    [SerializeField] Image lifeNumImage;

    private void Awake()
    {
        if (GameManager.instance == null)
            GameManager.instance = this;

        playerLife = 3;


    }
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDie() // 플레이어가 죽었을 때 호출될 함수
    {
        playerLife--;

        if(playerLife < 0)
        {
            GameOver();
        }
        else
        {
            //플레이어 부활
            Instantiate(playerPrefab);

        }
    }

    void GameOver() // 몫이 다달아서 게임오버가 됐을때 호출될 함수
    {

    }
}
