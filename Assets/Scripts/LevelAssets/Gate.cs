using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public Camera camera;
    public GameObject boss;
    public GameObject bgMusic;
    public AudioClip bossMusic;
    public AudioClip lvlMusic;
    public GameManager gameManager;

    private bool playerCollided = false;

    // Update is called once per frame
    void Update()
    {
        if (playerCollided)
        {
            // Gradually move camera to position (194, 18.8, -10)
            camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(194f, 20f, -10f), Time.deltaTime);


            // Gradually increase camera projection size to 45
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, 40, Time.deltaTime);
        }
    }

    public void Back()
    {
        bgMusic.GetComponent<AudioSource>().clip = lvlMusic;
        bgMusic.GetComponent <AudioSource>().Play();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCollided = true;
            camera.GetComponent<FollowCam>().enabled = false;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            gameManager.lvlMusic = bossMusic;
            bgMusic.GetComponent<AudioSource>().clip = bossMusic;
            bgMusic.GetComponent<AudioSource>().Play();
            boss.SetActive(true);
        }
    }
}