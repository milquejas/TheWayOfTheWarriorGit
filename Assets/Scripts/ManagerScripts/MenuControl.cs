using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public void LoadMap()
    {
        //playe paniketta painettu valikossa
        SceneManager.LoadScene("Map");
    }
    public void Save()
    {
        GameManager.manager.Save();
    }
    public void Load()
    {
        GameManager.manager.Load();
    }
}
