using System;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{

    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4]; //0 - top; 1 - down; 2 - left; 3 - right
    }

    [SerializeField] private Vector2Int size;
    [SerializeField] private int initPosition = 0;
    [SerializeField] GameObject room;
    [SerializeField] private Vector2 roomSize;
    [SerializeField] private int nRooms;

    List<Cell> board;

    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();
    }

    private void MazeGenerator()
    {
        board = new List<Cell>();

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = initPosition;
        Stack<int> path= new Stack<int>(); //Pila 

        int num = 0;
        while(num< nRooms)
        {
            num++;
            board[num].visited = true;

            //Comprobar si celdas vecinas
            List<int> neighbours = CheckNeighbours(currentCell);
        }
    }

    private List<int> CheckNeighbours(int cell)
    {
       List<int> neighbours= new List<int>();

        if(cell - size.x > 0 && !board[cell - size.x].visited) //comprobamos TOP
        {
            neighbours.Add(cell - size.x); //si no está visitada añadimos la celda en top
        }

        if (cell + size.x < 0 && !board[cell + size.x].visited) //comprobamos DOWN
        {
            neighbours.Add(cell + size.x); //si no está visitada añadimos la celda en bot
        }

        if (cell % size.x != 0 && !board[cell - 1].visited) //comprobamos LEFT
        {
            neighbours.Add(cell - 1); //si no está visitada añadimos la celda en left
        }

        if ((cell + 1) % size.x != 0 && !board[cell + 1].visited) //comprobamos LEFT
        {
            neighbours.Add(cell +1); //si no está visitada añadimos la celda en left
        }


        return neighbours;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
