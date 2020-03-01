using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyBall : MonoBehaviour
{
    public float jumpPower = 30;
    public int itemCount;
    bool isJump;
    Rigidbody rigid;
    AudioSource audio;
    public GameManager manager;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        
        isJump = false;
    }

    // Update is called once per frame

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJump)
        { 
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
            isJump = true;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
            isJump = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            itemCount++;
            audio.Play();
            manager.GetItem(itemCount);
            other.gameObject.SetActive(false);
        }

        else if (other.tag == "Finish")
        {
            if (manager.TotalItemCount == itemCount)
            {
                if (manager.stage == 2)
                    SceneManager.LoadScene(0);
                else
                    SceneManager.LoadScene(manager.stage + 1);
            }

            else
            {
                SceneManager.LoadScene(manager.stage);
            }
        }

    }
}
