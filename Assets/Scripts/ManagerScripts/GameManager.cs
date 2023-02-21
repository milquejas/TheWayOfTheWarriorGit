using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{

    // GameManager on Singleton tyyppinen

    public static GameManager manager;

    public string previousLevel;
    public string currentLevel;

    public float health;
    public float previousHealth;
    public float maxHealth;

    public float historyHealth;
    public float historyPreviousHealth;
    public float historyMaxHealth;

    //jokaista tasoa varten on muuttuja. muuttujan nimen pit‰‰ olla sama kuin loadlevele scriptiss‰ olevan muuttujan arvo.
    public bool LevelFirst;
    public bool Level2;
    public bool Level3;
    public bool Level4;

    private void Awake()
    {
        if (manager == null)
        {
            // jos ei ole manageria kerrotaan ett‰ t‰m‰ luokka on se manageri
            //kerrotaan myˆs ett‰ t‰m‰ manageri ei saa tuhoutua jos scene vaihtuu
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else
        {
            //t‰m‰ ajetaan silloin jos on jo olemassa manageri
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    // kaksi toimintoa, save ja load
    public void Save()
    {
        Debug.Log("GameSaved");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();
        data.health = health;
        data.previousHealth = previousHealth;
        data.maxHealth = maxHealth;
        data.currentLevel = currentLevel;
        data.LevelFirst = LevelFirst;
        data.Level2 = Level2;
        data.Level3 = Level3;
        data.Level4 = Level4;
        bf.Serialize(file, data);
        file.Close();
    }
    public void Load()
    {
        //tsekataan onko tallennetua tiedostoa edes olemassa. jos on niin load tapahtuu.
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            Debug.Log("Game Loaded");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            //siirret‰‰n ladattu info Game manageriin
            health = data.health;
            previousHealth = data.previousHealth;
            maxHealth = data.maxHealth;
            currentLevel = data.currentLevel;
            LevelFirst = data.LevelFirst;
            Level2 = data.Level2;
            Level3 = data.Level3;
            Level4 = data.Level4;
        }
    }

}

//toinen luokka mik‰ voidaan serialisoida. pit‰‰ sis‰ll‰‰n vain sen datan mit‰ voidaan serialisoida. 
[Serializable]
class PlayerData
{
   
    public float health;
    public float previousHealth;
    public float maxHealth;

    public string currentLevel;
    public bool LevelFirst;
    public bool Level2;
    public bool Level3;
    public bool Level4;
}
