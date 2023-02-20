using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // GameManager on Singleton tyyppinen

    public static GameManager manager;

    public string currentLevel;

    private void Awake()
    {
        if (manager == null)
        {
            // jos ei ole manageria kerrotaan ett� t�m� luokka on se manageri
            //kerrotaan my�s ett� t�m� manageri ei saa tuhoutua jos scene vaihtuu
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else
        {
            //t�m� ajetaan silloin jos on jo olemassa manageri
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
        
    }
}
