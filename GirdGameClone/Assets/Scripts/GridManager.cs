using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GridManager : MonoBehaviour
{
    // Start is called before the first frame update
    public const int COLS = 5;
    public const int ROWS = 7;
    public GameObject GemPrefab;
    //public Transform location;
    private int playerX = 2;
    private int playerY = 0;
    public static  Gem[,] board;
    private int blackColorCode = 7;
    private int whiteColorCode = 6;
    private bool notReadyToDestroy = false;
    private bool readyToDestroy = true;
    public GameObject Player;
    public TextMeshPro TextMeshPro;
    public int countDown = 6;
    private int score = 0;
    public GameObject Canvas;
    private GameObject a;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI endScore;


    void Start()
    {

        board = new Gem[ROWS, COLS];

       // Instantiate(GemPrefab, transform.position, transform.rotation);

        GeneratingBoardValue();
        UpdatePlayer();

      //  TestPrintBoard();
        //Detect();

        UpdateBoard();

        score = 0;
    }

    // Update is called once per frame
    void Update()
    {


        if (countDown > 0)
        {
if (Input.GetKeyDown("right") && playerX < COLS - 1)
        {
            SwapRight();
           // UpdatePlayer();
            //UpdateBoard();
        }
        else if (Input.GetKeyDown("left") && playerX > 0)
        {
            SwapLeft();
           // UpdatePlayer();
            //UpdateBoard();
        }
        else if (Input.GetKeyDown("up") && playerY < ROWS - 1)
        {
            SwapUp();
          //  UpdatePlayer();
           // UpdateBoard();
        }
        else if (Input.GetKeyDown("down") && playerY > 0)
        {
            SwapDown();
            //UpdatePlayer();
           // UpdateBoard();
        }
        else if (Input.GetKeyDown("space"))
        {

            //TestPrintBoard();
            //Detect();
            
        }
        }
        else if(countDown  <= 0)
        {
            endScore.text = "Score: " + score.ToString();
            Canvas.SetActive(true);

            if (Input.GetKeyDown("r"))
            {
                GeneratingBoardValue();
                UpdatePlayer();
                Canvas.SetActive(false);
                countDown = 6;
                UpdateBoard();
                score = 0; 
            }
        }
        
        Replace();
        DropDown();
        PlayerDropDown();
        UpdatePlayer();
        Refill();
        if (NoBlackBlock())
        {
            Detect();
            Debug.Log("no black block");
        }
        
        
        UpdateBoard();
        PlayerController();
        scoreText.text = "Score: " + score.ToString();

        //Debug.Log(playerX);
       // Destroy(a, 0.1f);
    }

    public class Gem
    {
        public int color;
        public int row;
        public int column;
        public bool readyToDestroy;

        public Gem(int clr, int r, int c, bool rtd)
        {
            color = clr;
            row = r;
            column = c;
            rtd = readyToDestroy;

        }
    }

    void GeneratingBoardValue()
    {
        for (int i = 0; i < ROWS; i++)
        {
            for (int u = 0; u < COLS; u++)
            {

                 //board[i, u] = new Gem(Random.Range(0, 6), i, u);
                board[i, u] = new Gem(blackColorCode, i, u, notReadyToDestroy);
                //location.position = new Vector3(i,u);

                // a.transform.position = new Vector2(i ,u);
                //Debug.Log(i);
            }

           


        }

       // board[0, 3].color = 1;
       // board[0, 4].color = 1;
       // board[1, 2].color = 1;
        //board[2, 2].color = 1;
        //board[0, 1].color = 1;
    }

    void UpdatePlayer()
    {
        board[playerY, playerX] = new Gem(whiteColorCode, playerY, playerX,notReadyToDestroy);
    } 

    void TestPrintBoard()
    {
        for (int i = 0; i < ROWS; i++)
        {
            for (int u = 0; u < COLS; u++)
            {
                Debug.Log(board[i,u].row.ToString() + board[i, u].column.ToString()
                    + i.ToString()+ u.ToString()+ board[i,u].color.ToString());



            }
        }

    }

    void UpdateBoard()
    {

        
        for (int i = 0; i < ROWS; i++)
        {
            for (int u = 0; u < COLS; u++)
            {

                Vector3 position = new Vector3(1.5f * u -3f , 1.5f * i - 4.5f);
                a = Instantiate(GemPrefab, position, transform.rotation) as GameObject;
                a.GetComponent<TileManager>().colorcode = board[i,u].color;
                a.GetComponent<TileManager>().y = board[i, u].row;
                a.GetComponent<TileManager>().x = board[i, u].column;

            }

        }
    }

    void SwapRight()
    {
        board[playerY, playerX].color = board[playerY, playerX + 1].color;

       // board[x, y + 1].color = 5;

        playerX++;
        countDown--;
    }

    void SwapLeft()
    {
        board[playerY, playerX].color = board[playerY, playerX - 1].color;

        // board[x, y + 1].color = 5;

        playerX--;
        countDown--;
    }

    void SwapUp()
    {
        board[playerY, playerX].color = board[playerY + 1, playerX].color;

        // board[x, y + 1].color = 5;

        playerY++;
        countDown--;
    }

    void SwapDown()
    {
        board[playerY, playerX].color = board[playerY - 1, playerX].color;

        // board[x, y + 1].color = 5;

        playerY--;
        countDown--;
    }

    void Detect()
    {
       
        
        for (int i = 0; i < ROWS-2; i++)
        {
            for (int u = 0; u < COLS; u++)
            {
               if(board[i,u].color == board[i+1,u].color && board[i,u].color == board[i + 2, u].color && board[i, u].color != blackColorCode)
                {
                    Debug.Log("shu 3");
                    board[i, u].readyToDestroy = readyToDestroy;
                    board[i + 1, u].readyToDestroy = readyToDestroy;
                    board[i + 2, u].readyToDestroy = readyToDestroy;
                    countDown = 6;
                    score = score + 3;
                }
               

            }

        }

        

        for (int i = 0; i < ROWS ; i++)
        {
            for (int u = 0; u < COLS-2; u++)
            {
                if (board[i, u].color == board[i, u + 1].color && board[i, u].color == board[i, u + 2].color && board[i, u].color != blackColorCode)
                {
                    Debug.Log("hen 3");
                    board[i, u].readyToDestroy = readyToDestroy;
                    board[i, u + 1].readyToDestroy = readyToDestroy;
                    board[i, u + 2].readyToDestroy = readyToDestroy;
                    countDown = 6;
                    score = score + 3;
                }
            }

        }

            }

    void Replace()
    {
        for (int i = 0; i < ROWS; i++)
        {
            for (int u = 0; u < COLS; u++)
            {
                if (board[i, u].readyToDestroy == readyToDestroy)
                {
                    board[i, u].readyToDestroy = notReadyToDestroy;
                    board[i, u].color = blackColorCode;


                }
                
            }
            
        }

            }

    void DropDown()
    {
        for (int i = 1; i < ROWS; i++)
        {
            for (int u = 0; u < COLS; u++)
            {
                if (board[i-1,u].color == blackColorCode && board[i,u].color != blackColorCode && board[i,u].color != whiteColorCode)
                {
                    board[i - 1, u].color = board[i, u].color;

                    board[i, u].color = blackColorCode;
                }


            }

        }
            }

    void PlayerDropDown()
    {
        if (playerY > 0 && board[playerY-1,playerX].color == blackColorCode)
        {
            board[playerY, playerX].color = board[playerY - 1, playerX].color;

           

            playerY--;
            // Debug.Log("asfasfasdas");
        }
    }

    void Refill()
    {
        for (int i = 0; i < COLS; i++)
        {
            if (board[ROWS - 1,i].color == blackColorCode )
            {
                board[ROWS - 1, i].color = Random.Range(0,6);
            }
        }
    }

    bool NoBlackBlock()
    {
        int a = 0;

        for (int i = 0; i < ROWS; i++)
        {
            for (int u = 0; u < COLS; u++)
            {
                if (board[i,u].color == blackColorCode)
                {
                    a++;
                }


            }


        }

        if (a== 0)
        {
            return true;
        }
        else
        {
            return false;
        }
                }

    void PlayerController()
    {
        Vector3 position = new Vector3(1.5f * playerX - 3f, 1.5f * playerY - 4.5f);
        TextMeshPro.text = countDown.ToString();
        Player.transform.position = position;
    }
}

