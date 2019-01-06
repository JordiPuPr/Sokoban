using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public GameObject buttonLevel;
    public Transform parentOfLevel;
    public Text gameSelected;

    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists("Assets/Resources/levelSelected.txt"))
        {
            File.Delete("Assets/Resources/levelSelected.txt");
        }

        string[] levels = Directory.GetFiles("Assets/Resources/");

        foreach (string level in levels)
        {
            if (level.EndsWith(".json"))
            {
                string texto = level;
                GameObject buttonOfLevel = Instantiate(buttonLevel, Vector3.zero, transform.rotation) as GameObject;
                buttonOfLevel.transform.SetParent(parentOfLevel);

                Button butt = buttonOfLevel.GetComponent<Button>();
                butt.onClick.AddListener(() => { this.LoadLevel(texto); });

                Text text = buttonOfLevel.GetComponentInChildren<Text>();
                text.text = texto.Split('.')[0].Replace("Assets/Resources/", "");
            }
        }

    }

    public void LoadLevel(string name)
    {
        if (File.Exists("Assets/Resources/levelSelected.txt"))
        {
            File.Delete("Assets/Resources/levelSelected.txt");
        }

        if (name != "")
        {
            FileStream stream = new FileStream("Assets/Resources/levelSelected.txt", FileMode.Create);
            stream.Close();

            StreamWriter sw = File.CreateText("Assets/Resources/levelSelected.txt");
            sw.Close();

            File.WriteAllText("Assets/Resources/levelSelected.txt", name);
            gameSelected.text = name.Split('.')[0].Replace("Assets/Resources/", "");
        }

        //SceneManager.LoadScene("EditLevels");
    }

    public void PlayLevel()
    {
        SceneManager.LoadScene("PlayGame");
    }

    public void EditLevel()
    {
        SceneManager.LoadScene("EditLevels");
    }

    public void CreateLevel()
    {
        if (File.Exists("Assets/Resources/levelSelected.txt"))
        {
            File.Delete("Assets/Resources/levelSelected.txt");
        }

        SceneManager.LoadScene("EditLevels");
    }

    public void OnCollisionExit(Collision collision)
    {
        Application.Quit();
    }
}
