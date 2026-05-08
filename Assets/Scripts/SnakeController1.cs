
using System.Collections;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class SnakeController : MonoBehaviour
{
    private Vector2Int foodPosition;
    public GameObject foodPrefab;
    public GameObject bodyPrefab;
    private int score = 0;
    public float moveInterval = 0.2f;
    public float moveTimer = 0f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    //private bool canInput = true; //Input Lock
    private Vector3 lastStepDirection = Vector3.forward;
    private Vector3 direction = Vector3.forward;
    private List<Transform> bodyParts = new List<Transform>();
   
    private void OnMoveSnake(InputValue value)
    {
        //if(!canInput) return;

        Vector2 input = value.Get<Vector2>();
        if (input.x != 0 || input.y != 0)
        {
            Vector3 newDirection = new Vector3(Mathf.Round(input.x), 0, Mathf.Round(input.y));
            if (newDirection != -lastStepDirection)
            {
                direction = newDirection;
                //canInput = false;
            }
        }
        lastStepDirection = direction;
    }

    private void Start()
    {
       InvokeRepeating("MoveSnake", 0f, moveInterval);
    }

    private void MoveSnake()
    {
        transform.position += direction;
        Debug.Log( transform.position.x + " " + transform.position.z);
    }
    
    private void ScoreText()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    private void GameOverText()
    {
        gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0f; //Game is paused.
    }
}
