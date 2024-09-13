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
        NewDungeonMaze();
    }

    public void NewDungeonMaze()
    {
        if (board != null)
        {
            board.Clear();

            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
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

        Debug.Log(board.Count);

        int num = 0;
        while(num< nRooms)
        {
            num++;
            board[num].visited = true;

            //Comprobar si celdas vecinas
            List<int> neighbours = CheckNeighbours(currentCell);
            Debug.Log(neighbours.Count);
            //Comprobamos si no hay vecinos 
            if(neighbours.Count==0 ) {
                //Si no hay más caminos que probar 
                if(path.Count==0)
                {
                    break;
                }
                else
                {
                    currentCell=path.Pop(); //Pop saca un elemento de la fila
                }
            }
            else
            {
                //Si hay vecinos
                path.Push(currentCell);
                int newCell = neighbours[Random.Range(0, neighbours.Count)]; //Le damos una celda aleatoriamente
                //la lista de vecinos va a tener un maximo de 4 vecinos que estén disponibles, no cuenta las diagonales

                //si la celda nueva es mayor es porque es un vecino derecha o abajo
                if(newCell>currentCell) {
                    if(newCell-1 ==currentCell)
                    {
                        //Está a la derecha de nuestra celda (se abre la actual a la derecha y la nueva a la izquierda)
                        board[currentCell].status[3] = true;
                        board[newCell].status[2] = true;
                    }
                    else
                    {
                        //está abajo (se abre la actual abajo y la nueva arriba) //gráficamente está al revés porque al cambiar el eje se dibuja de abajo a arriba
                        board[currentCell].status[0] = true;
                        board[newCell].status[1] = true;
                    }
                }
                else
                {
                    //Vecino izquierda o arriba
                    if(newCell+1 ==currentCell)
                    {
                        //está a la izquierda
                        board[currentCell].status[2] = true;
                        board[newCell].status[3] = true;
                    }
                    else
                    {
                        //está arriba
                        board[currentCell].status[1] = true;
                        board[newCell].status[0] = true;
                    }
                }
                currentCell = newCell;
            }
        }
        DungeonGenerator();
    }

    private void DungeonGenerator()
    {
        for(int i = 0;i<size.x;i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                var newRoom = Instantiate(room, new Vector3(i * roomSize.x, 0, j * roomSize.y),Quaternion.identity,transform).GetComponent<RoomBehaviour>();
                newRoom.UpdateSala(board[i+j*size.x].status);
            }
        }
    }

    private List<int> CheckNeighbours(int cell)
    {
       List<int> neighbours= new List<int>();

        if(cell - size.x > 0 && !board[cell - size.x].visited) //comprobamos TOP
        {
            neighbours.Add(cell - size.x); //si no está visitada añadimos la celda en top
        }

        if (cell + size.x < board.Count && !board[cell + size.x].visited) //comprobamos DOWN
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
