

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
    private float moveTimer = 0f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    private bool canInput = true; //Input Lock
    private Vector3 lastStepDirection = Vector3.forward;
    private Vector3 direction = Vector3.forward;
    private List<Transform> bodyParts = new List<Transform>();
   

    private void Start()
    {
       SpawnFood();
    }

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
    }

   

    private void Update()
    {
        moveTimer+= Time.deltaTime;
        if (moveTimer >= moveInterval)
        {
            // ? BodyMove();
            
            moveTimer = 0f;
        }
    }
    
    private void SpawnFood()
    {  
        int gridSizeX = 1;
        int gridSizeZ = 1;
        float headX = transform.position.x / gridSizeX + 1;
        float headZ = transform.position.z / gridSizeZ + 1;
        int snakeX = (int)headX;
        int snakeZ = (int)headZ;
        Debug.Log("CUBE: " + snakeX + ", " + snakeZ);
       
        float appleX = foodPrefab.transform.position.x / gridSizeX + 1;
        float appleZ = foodPrefab.transform.position.z / gridSizeZ+ 1;
        int foodX = (int)appleX;
        int foodZ = (int)appleZ;
        if(snakeX == foodX && snakeZ == foodZ)
        {

            Debug.Log("YEM YENDI!!");
            int newFoodX = Random.Range(-7,8);
            int newFoodZ = Random.Range(-7,8); 
            foodPrefab.transform.position = new Vector3(newFoodX , 1.5f, newFoodZ);
            Debug.Log("YERI DEGISTI: " + newFoodX  + ", " + newFoodZ);
           
        }
        else{
            Debug.Log("NOT SAME GRID");
        }
    }
    
    private void Grow()
    {
        
    }
   
    
    private void BodyMove()
    {   
        
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
