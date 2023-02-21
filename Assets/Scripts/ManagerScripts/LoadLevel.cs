using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{

    public string levelToLoad;

    public bool cleared;

    // Start is called before the first frame update
    void Start()
    {
        //katsotaan aina map scene avattaessa ett‰ onko game managerissa merkattu ett‰ kyseinen taso on l‰p‰isty
        //jos on l‰p‰isty ajetaan cleared funktio joka tekee tarpeelliset muutokset t‰h‰n objektiin. eli 
        //n‰ytt‰‰ stage clear kyltin ja poistaa colliderin.

        if (GameManager.manager.GetType().GetField(levelToLoad).GetValue(GameManager.manager).ToString() == "True")
        {
            Cleared(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cleared(bool isClear)
    {
        if(isClear == true)
        {
            cleared = true;
            //asetetaan gamemanagerissa oikea boolean arvo trueksi
            GameManager.manager.GetType().GetField(levelToLoad).SetValue(GameManager.manager, true);
            //laitetaan stage clear kyltti n‰kyviin
            transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = true;
            //koska taso on l‰p‰isty, poistetaan level trigger objectilta circle collider ett‰ tasoon ei p‰‰st‰ takaisin
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
