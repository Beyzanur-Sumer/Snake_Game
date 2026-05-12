
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
    private Vector3Int headGridPos;
    private Vector3Int foodGridPos;
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
       bodyParts.Add(transform);
       InvokeRepeating("AllControls", 0f, moveInterval);
      
    }
    
    private void Update()
    { 
        moveTimer+= Time.deltaTime;
        if (moveTimer >= moveInterval)
        {
            if(bodyParts.Count > 1)
            {
               BodyMovement();
            }
            moveTimer = 0f;
        }
    }

    
    private void AllControls()
    {
        transform.position += direction;
        headGridPos = new Vector3Int(Mathf.RoundToInt(transform.position.x),0, Mathf.RoundToInt(transform.position.z));
        foodGridPos = new Vector3Int(Mathf.RoundToInt(foodPrefab.transform.position.x),0, Mathf.RoundToInt(foodPrefab.transform.position.z));
        
        if (headGridPos.x == foodGridPos.x && headGridPos.z == foodGridPos.z)
        {
           ScoreText();
           BodySpawn();
           FoodSpawn();
        }

        if (headGridPos.x < -7 || headGridPos.x > 7 || headGridPos.z < -7 || headGridPos.z > 7)
        {
            scoreText.gameObject.SetActive(false);
            GameOverText();
        }
    }

    private void BodySpawn()
    {
        GameObject bodyPart = Instantiate(bodyPrefab, transform.position - direction, Quaternion.identity);
        bodyParts.Add(bodyPart.transform);
    }

    private void BodyMovement()
    {
        if(bodyParts.Count > 1)
        {
            bodyParts[bodyParts.Count - 1].position += direction; //???????????????????????????????????????????????????????????????????????????????????????????????????????
        }
    }

    private void FoodSpawn()
    {
        int newFoodX = Random.Range(-7,8);
        int newFoodZ = Random.Range(-7,8); 
        foodPrefab.transform.position = new Vector3(newFoodX , 1.5f, newFoodZ);
    }
    
    private void ScoreText()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    private void GameOverText()
    {  
        gameOverText.text = "Game Over!   High Score:" + score.ToString();
        Time.timeScale = 0f; //Game is paused.
    }
}
