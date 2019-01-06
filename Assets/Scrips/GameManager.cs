using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform firtsPosition;
    public GameObject tilePrefab;

    public Text levelName;
    public GameObject winner;

    public Sprite clear;
    public Sprite wall;
    public Sprite player;
    public Sprite boxRed;
    public Sprite pointRed;
    public Sprite boxYellow;
    public Sprite pointYellow;
    public Sprite boxBlue;
    public Sprite pointBlue;
    public Sprite boxPurple;
    public Sprite pointPurple;

    private int horizontal = 14;
    private int vertical = 10;

    private bool move;
    private int[] positionPlayer;
    private List<Boton> listaBotones = new List<Boton>();
    private GameObject[,] tiles = new GameObject[14, 10];
    private int[] saveTiles = new int[14 * 10];
    private int[,] saveTiles2 = new int[14, 10];

    void Start()
    {
        int x, y, i;
        Level lvl = null;
        Boton b = null;
        move = true;

        if (!File.Exists("Assets/Resources/levelSelected.txt"))
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            string file = File.ReadAllText("Assets/Resources/levelSelected.txt");
            lvl = LoadLevel(file);

            levelName.text = lvl.fileName.Split('.')[0].Replace("Assets/Resources/", "");
            saveTiles = lvl.tiles;
            positionPlayer = lvl.lastPositionPlayer;

            i = 0;
            for (x = 0; x < horizontal; x++)
            {
                for (y = 0; y < vertical; y++)
                {
                    saveTiles2[x, y] = saveTiles[i];
                    i++;
                }
            }
        }

        i = 0;
        for (x = 0; x < horizontal; x++)
        {
            for (y = 0; y < vertical; y++)
            {
                tiles[x, y] = Instantiate(tilePrefab, new Vector3(firtsPosition.position.x + (x * 100), firtsPosition.position.y - (y * 100), firtsPosition.position.z), transform.rotation) as GameObject;
                tiles[x, y].transform.SetParent(firtsPosition);

                if (x == 0 || y == 0 || x == horizontal - 1 || y == vertical - 1)
                {
                    Image img = tiles[x, y].GetComponent<Image>();
                    img.sprite = wall;
                }
                else
                {
                    Button butt = tiles[x, y].GetComponent<Button>();
                    string num = x.ToString() + ";" + y.ToString();

                    Image img = tiles[x, y].GetComponent<Image>();
                    if (saveTiles2[x, y] != -1)
                    {
                        switch (saveTiles2[x, y])
                        {
                            case 1:
                                img.sprite = boxRed;
                                break;
                            case 2:
                                img.sprite = boxYellow;
                                break;
                            case 3:
                                img.sprite = boxBlue;
                                break;
                            case 4:
                                img.sprite = boxPurple;
                                break;
                            case 5:
                                b = new Boton();
                                b.x = x;
                                b.y = y;
                                b.solucion = boxRed;
                                b.color = pointRed;
                                listaBotones.Add(b);

                                img.sprite = pointRed;
                                break;
                            case 6:
                                b = new Boton();
                                b.x = x;
                                b.y = y;
                                b.solucion = boxYellow;
                                b.color = pointYellow;
                                listaBotones.Add(b);

                                img.sprite = pointYellow;
                                break;
                            case 7:
                                b = new Boton();
                                b.x = x;
                                b.y = y;
                                b.solucion = boxBlue;
                                b.color = pointBlue;
                                listaBotones.Add(b);

                                img.sprite = pointBlue;
                                break;
                            case 8:
                                b = new Boton();
                                b.x = x;
                                b.y = y;
                                b.solucion = boxPurple;
                                b.color = pointPurple;
                                listaBotones.Add(b);

                                img.sprite = pointPurple;
                                break;
                            case 9:
                                img.sprite = wall;
                                break;
                            case 10:
                                img.sprite = player;
                                break;
                        }
                    }
                }
            }
            i++;
        }
    }

    public Level LoadLevel(string path)
    {
        string json = File.ReadAllText(path);

        return JsonUtility.FromJson<Level>(json);
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (CanMove(positionPlayer[0], positionPlayer[1] - 1))
                {
                    if (MoveBox(positionPlayer[0], positionPlayer[1] - 1, positionPlayer[0], positionPlayer[1] - 2))
                    {
                        MovePlayer(positionPlayer[0], positionPlayer[1], positionPlayer[0], positionPlayer[1] - 1);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (CanMove(positionPlayer[0], positionPlayer[1] + 1))
                {
                    if (MoveBox(positionPlayer[0], positionPlayer[1] + 1, positionPlayer[0], positionPlayer[1] + 2))
                    {
                        MovePlayer(positionPlayer[0], positionPlayer[1], positionPlayer[0], positionPlayer[1] + 1);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (CanMove(positionPlayer[0] - 1, positionPlayer[1]))
                {
                    if (MoveBox(positionPlayer[0] - 1, positionPlayer[1], positionPlayer[0] - 2, positionPlayer[1]))
                    {
                        MovePlayer(positionPlayer[0], positionPlayer[1], positionPlayer[0] - 1, positionPlayer[1]);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (CanMove(positionPlayer[0] + 1, positionPlayer[1]))
                {
                    if (MoveBox(positionPlayer[0] + 1, positionPlayer[1], positionPlayer[0] + 2, positionPlayer[1]))
                    {
                        MovePlayer(positionPlayer[0], positionPlayer[1], positionPlayer[0] + 1, positionPlayer[1]);
                    }
                }
            }
        }
    }

    private void MovePlayer(int x, int y, int x2, int y2)
    {
        tiles[x, y].GetComponent<Image>().sprite = clear;
        tiles[x2, y2].GetComponent<Image>().sprite = player;
        positionPlayer[0] = x2;
        positionPlayer[1] = y2;
        EndGame();
    }

    private bool MoveBox(int x, int y, int x2, int y2)
    {
        if (tiles[x, y].GetComponent<Image>().sprite == boxBlue ||
                tiles[x, y].GetComponent<Image>().sprite == boxRed ||
                tiles[x, y].GetComponent<Image>().sprite == boxPurple ||
                tiles[x, y].GetComponent<Image>().sprite == boxYellow)
        {
            if (tiles[x2, y2].GetComponent<Image>().sprite != wall &&
                !(tiles[x2, y2].GetComponent<Image>().sprite == boxBlue ||
                tiles[x2, y2].GetComponent<Image>().sprite == boxRed ||
                tiles[x2, y2].GetComponent<Image>().sprite == boxPurple ||
                tiles[x2, y2].GetComponent<Image>().sprite == boxYellow))
            {
                tiles[x2, y2].GetComponent<Image>().sprite = tiles[x, y].GetComponent<Image>().sprite;
                
                return true;
            }
            return false;
        }
        return true;
    }

    private void EndGame()
    {
        bool fin = true;

        foreach (Boton btn in listaBotones)
        {
            if (tiles[btn.x, btn.y].GetComponent<Image>().sprite != btn.solucion)
            {
                fin = false;
            }

            if (tiles[btn.x, btn.y].GetComponent<Image>().sprite == clear)
            {
                tiles[btn.x, btn.y].GetComponent<Image>().sprite = btn.color;
            }
        }

        if (fin)
        {
            move = false;
            winner.SetActive(true);
            Invoke("Quit", 2);
        }
    }

    private bool CanMove(int x, int y)
    {
        return tiles[x, y].GetComponent<Image>().sprite != wall;
    }

    public void Restart()
    {
        SceneManager.LoadScene("PlayGame");
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public class Boton
    {
        public int x;
        public int y;
        public Sprite solucion;
        public Sprite color;
    }
}
