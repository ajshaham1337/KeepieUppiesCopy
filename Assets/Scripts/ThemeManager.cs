using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ThemeManager : MonoBehaviour
{
//------------------------------------------------------------------------------------------------
// Variables
//------------------------------------------------------------------------------------------------
    private static readonly string firstPlay = "firstPlay";
    private static readonly string BGPref = "BGPref";
    private static readonly string ObjPref = "ObjPref";
    private int firstPlayInt,BGInt, ObjInt;
    public static ThemeManager me;

    public GameObject[] backGroundTheme;
    private GameObject oldBGTheme, newBGTheme;

    public GameObject[] objectTheme;
    
    

//------------------------------------------------------------------------------------------------
// Functions
//------------------------------------------------------------------------------------------------
    void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            ContinueSettings();
        }
    }

    // Start is called before the first frame update
    void Start()
    {

       if(SceneManager.GetActiveScene().buildIndex == 0) //if on Main Menu Scene
        {
            if (me == null)
            {
                me = this;
            }
            else
            {
                Destroy(gameObject);
            }

            firstPlayInt = PlayerPrefs.GetInt(firstPlay);

            if(firstPlayInt == 0) //setting volumes on inital play
            {
                BGInt = 1; //BG is set to the first selection
                ObjInt = 1; //Objects are set to first selection
                PlayerPrefs.SetInt(BGPref, BGInt); //save BGint to player prefs
                PlayerPrefs.SetInt(ObjPref, ObjInt);//set Objects to player prefs
                PlayerPrefs.SetInt(firstPlay, -1); //Change Value of first Play, so that it never runs again
            }
            else //setting volumes on play after first play
            {
                BGInt = PlayerPrefs.GetInt(BGPref); //grab volume level from playpref
                ObjInt = PlayerPrefs.GetInt(ObjPref);//get sfx value from player prefs
            }
        }//end of if Scene is on Main Menu Scene
        else if(SceneManager.GetActiveScene().buildIndex == 1) //if on Game Scene
        {
            if(me == null)
            {
                me  = this;
            }
            else
            {
                Destroy(gameObject);
            }
        } //end of if Scene is on Game Scene
    } // end of Start() Function

    // void Update(){
    //     Debug.Log("oldBGTheme is " + oldBGTheme.name);
    // }

    private void ContinueSettings() //grab settings from player prefs
    {
        //grab theme settings from player prefs
        BGInt = PlayerPrefs.GetInt(BGPref);
        ObjInt = PlayerPrefs.GetInt(ObjPref);

    }

    public void SaveThemePreferences() //Save settings when back button is pressed
    {
        PlayerPrefs.SetInt(BGPref, BGInt); //save BGint to player prefs
        PlayerPrefs.SetInt(ObjPref, ObjInt);//set Objects to player prefs
    }

    void OnApplicationFocus (bool inFocus) //if user leaves game or closes game; settings will save
    {
        if(!inFocus)
        {
            SaveThemePreferences();
        }
    }

    
    
    void SetTheme(GameObject newTheme, GameObject[] list)
    {
        //check for current active theme 
        for (int i = 0; i < list.Length; i++)
        {
            if(list[i].activeSelf == true)
            {
                oldBGTheme = list[i];
            }
        }

        newBGTheme = newTheme;

        oldBGTheme.SetActive(false);
        newBGTheme.SetActive(true);
    }

    public void SetTheme1()
    {
        SetTheme(backGroundTheme[0], backGroundTheme);
    }

    public void SetTheme2()
    {
        SetTheme(backGroundTheme[1], backGroundTheme);
    }



    

    
    // We need a way to get Backgrounds, UI, and Sounds Connected to one object (Art Theme)
    // We need to assign an int value to Art Theme; best way i could think is to make an array of art themes and assigning the theme the int value of its index

    //we need a way to disable an Old Art Theme and Enable a new Art Theme
        // probably using int values

    // We alson need a way to Assign Objects to an object (Objects Theme)
    // We need to assign an int Value to 

}
