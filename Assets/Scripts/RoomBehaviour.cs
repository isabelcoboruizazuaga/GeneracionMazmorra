using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject[] wall; //0 - top; 1 - down; 2 - left; 3 - right
    bool connected;

    public void UpdateSala(bool[] status)
    {
        connected = false;

        for (int i = 0; i < status.Length; i++)
        {
            wall[i].SetActive(!status[i]); //si la entrada es true desaparece la pared

            //TO DO: Activamos las puertas de las entradas
            if (status[i])
            {
                connected = true;
            }
        }

        if (!connected)
        {
            Destroy(gameObject);
        }
    }
}
