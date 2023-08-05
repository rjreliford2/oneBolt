using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPortal : MonoBehaviour
{

    public float amplitude = 0.5f; // Set the amplitude of the oscillation
    public float frequency = 2f;
    public bool floating = true;
    public bool cutscene = false;

    private GameManager gameManager;
    private float startY;


    private void Start()
    {
        startY = transform.position.y; // Record the starting Y position of the object
        gameManager = GameObject.Find("MainCanvas").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (floating)
        {
            // Calculate the new Y position based on a sine wave
            float newY = startY + amplitude * Mathf.Sin(Time.time * frequency);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (cutscene)
            {
                NextLevel();
            }
            else
            {
                gameManager.Handler(5);
            }
            
        }
    }


    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}