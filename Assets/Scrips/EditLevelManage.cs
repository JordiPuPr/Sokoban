using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EditLevelManage : MonoBehaviour
{
    public Transform firtsPosition;
    public GameObject tilePrefab;

    public InputField inputField;
    public Text fileName;

    public Image btnClear;
    public Image btnWall;
    public Image btnPlayer;
    public Image btnBoxRed;
    public Image btnPointRed;
    public Image btnBoxYellow;
    public Image btnPointYellow;
    public Image btnBoxBlue;
    public Image btnPointBlue;
    public Image btnBoxPurple;
    public Image btnPointPurple;

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

    private int[] lastPositionPlayer = new int[] { -1, -1 };
    private int selectedSprite = 9;
    private GameObject[,] tiles = new GameObject[14, 10];
    private int[] saveTiles = new int[14 * 10];
    private int[,] saveTiles2 = new int[14, 10];

    private Color transparent = new Color(24f, 72f, 148f, 0f);
    private Color green = new Color(0f, 255f, 0f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        int x, y, i;
        bool cargarNivel = false;
        Level lvl = null;

        if (File.Exists("Assets/Resources/levelSelected.txt"))
        {
            string file = File.ReadAllText("Assets/Resources/levelSelected.txt");
            lvl = LoadLevel(file);

            cargarNivel = true;

            saveTiles = lvl.tiles;
            lastPositionPlayer = lvl.lastPositionPlayer;
            fileName.text = lvl.fileName;

            inputField.interactable = false;
            inputField.text = lvl.fileName;

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
                    butt.onClick.AddListener(() => { this.PaintTile(num); });

                    if (cargarNivel)
                    {
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
                                    img.sprite = pointRed;
                                    break;
                                case 6:
                                    img.sprite = pointYellow;
                                    break;
                                case 7:
                                    img.sprite = pointBlue;
                                    break;
                                case 8:
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
                    else
                    {
                        saveTiles2[x, y] = -1;
                    }
                }

                i++;
            }
        }
    }

    public void SelectSprite(int num)
    {
        switch (selectedSprite)
        {
            case -1:
                btnClear.color = transparent;
                break;
            case 1:
                btnBoxRed.color = transparent;
                break;
            case 2:
                btnBoxYellow.color = transparent;
                break;
            case 3:
                btnBoxBlue.color = transparent;
                break;
            case 4:
                btnBoxPurple.color = transparent;
                break;
            case 5:
                btnPointRed.color = transparent;
                break;
            case 6:
                btnPointYellow.color = transparent;
                break;
            case 7:
                btnPointBlue.color = transparent;
                break;
            case 8:
                btnPointPurple.color = transparent;
                break;
            case 9:
                btnWall.color = transparent;
                break;
            case 10:
                btnPlayer.color = transparent;
                break;
        }

        switch (num)
        {
            case -1:
                btnClear.color = green;
                break;
            case 1:
                btnBoxRed.color = green;
                break;
            case 2:
                btnBoxYellow.color = green;
                break;
            case 3:
                btnBoxBlue.color = green;
                break;
            case 4:
                btnBoxPurple.color = green;
                break;
            case 5:
                btnPointRed.color = green;
                break;
            case 6:
                btnPointYellow.color = green;
                break;
            case 7:
                btnPointBlue.color = green;
                break;
            case 8:
                btnPointPurple.color = green;
                break;
            case 9:
                btnWall.color = green;
                break;
            case 10:
                btnPlayer.color = green;
                break;
        }
        selectedSprite = num;
    }

    public void PaintTile(string xy)
    {
        int x = int.Parse(xy.Split(';')[0]);
        int y = int.Parse(xy.Split(';')[1]);

        Image img = tiles[x, y].GetComponent<Image>();
        
        saveTiles2[x, y] = selectedSprite;
        switch (selectedSprite)
        {
            case -1:
                img.sprite = clear;
                break;
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
                img.sprite = pointRed;
                break;
            case 6:
                img.sprite = pointYellow;
                break;
            case 7:
                img.sprite = pointBlue;
                break;
            case 8:
                img.sprite = pointPurple;
                break;
            case 9:
                img.sprite = wall;
                break;
            case 10:
                if (lastPositionPlayer[0] != -1)
                {
                    Image lastImgPlayer = tiles[lastPositionPlayer[0], lastPositionPlayer[1]].GetComponent<Image>();
                    lastImgPlayer.sprite = clear;
                    saveTiles2[lastPositionPlayer[0], lastPositionPlayer[1]] = selectedSprite;
                }

                lastPositionPlayer[0] = x;
                lastPositionPlayer[1] = y;
                img.sprite = player;
                break;
        }
    }

    public void SaveLevel()
    {
        if (fileName.text == "")
        {
            StartCoroutine(ErrorSaving(6));
        }
        else
        {
            int i = 0;
            for (int x = 0; x < horizontal; x++)
            {
                for (int y = 0; y < vertical; y++)
                {
                    saveTiles[i] = saveTiles2[x, y];
                    i++;
                }
            }

            Level lvl = new Level();
            lvl.tiles = saveTiles;
            lvl.lastPositionPlayer = lastPositionPlayer;
            lvl.fileName = fileName.text;

            string json = JsonUtility.ToJson(lvl);

            string path = "";
            path = "Assets/Resources/" + fileName.text + ".json";

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            FileStream stream = new FileStream(path, FileMode.Create);
            stream.Close();

            StreamWriter sw = File.CreateText(path);
            sw.Close();

            File.WriteAllText(path, json);
        }
    }

    public Level LoadLevel(string path)
    {
        string json = File.ReadAllText(path);

        return JsonUtility.FromJson<Level>(json);
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator ErrorSaving(int veces)
    {
        while (veces != 0)
        {
            inputField.interactable = !inputField.interactable;
            yield return new WaitForSeconds(.1f);
            veces--;
        }
    }

}
