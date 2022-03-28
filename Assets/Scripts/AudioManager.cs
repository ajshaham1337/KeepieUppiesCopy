using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
//------------------------------------------------------------------------------------------------
// Variables
//------------------------------------------------------------------------------------------------
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string MusicPref = "MusicPref";
    private static readonly string SFXPref = "SFXPref";
    private int firstPlayInt;
    public Slider sFXSlider, musicSlider;
    private float musicFloat, sfxFloat;
    public AudioSource musicAudio;
    public static AudioManager me;

    // variables for playing sfx from a list

    //reward sound
    public GameObject rewardSFX;
    public List<AudioSource> rewardSFXAudioSource = new List<AudioSource>();

    //press button sound
    public GameObject buttonSFX;
    public List<AudioSource> buttonSFXAudioSource = new List<AudioSource>();

    //obstacle hit player sound
    public GameObject obstacleHitSFX;
    public List<AudioSource> obstacleHitSFXAudioSource = new List<AudioSource>();

    //obstacle spawn sound
    public GameObject obstacleSpawnSFX;
    public List<AudioSource> obstacleSpawnSFXAudioSource = new List<AudioSource>();

    //player Move sound
    public GameObject playerMoveSFX;
    public List<AudioSource> playerMoveSFXAudioSource = new List<AudioSource>();

    //dead zone sound
    public GameObject deadZoneSFX;
    public List<AudioSource> deadZoneSFXAudioSource = new List<AudioSource>();

    //count down sound
    public GameObject countDownSFX;
    public List<AudioSource> countDownSFXAudioSource = new List<AudioSource>();
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

            firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

            if(firstPlayInt == 0) //setting volumes on inital play
            {
                musicFloat = 0.5f; //music is set to 50% on initail run
                sfxFloat = 0.5f; //sfx is set to 50% on initial run
                musicSlider.value = musicFloat; //set slider value to what musicFloat is
                sFXSlider.value = sfxFloat;// set slider value to what sfxFloat is
                PlayerPrefs.SetFloat(MusicPref, musicFloat); //save musicfloat to player prefs
                PlayerPrefs.SetFloat(SFXPref, sfxFloat);//set sfx to player prefs
                PlayerPrefs.SetInt(FirstPlay, -1); //Change Value of First Play, so that it never runs again
            }
            else //setting volumes on play after first play
            {
                musicFloat = PlayerPrefs.GetFloat(MusicPref); //grab volume level from playpref
                musicAudio.volume = musicFloat; //set audio volume on initial state
                musicSlider.value = musicFloat; //set slider to new volume level
                sfxFloat = PlayerPrefs.GetFloat(SFXPref);//get sfx value from player prefs
                sFXSlider.value = sfxFloat;//set sFXSlider value from player prefs
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

    private void ContinueSettings() //grab settings from player prefs
    {
        //grab audio settings from player prefs
        musicFloat = PlayerPrefs.GetFloat(MusicPref);
        sfxFloat = PlayerPrefs.GetFloat(SFXPref);

        //set slider values to values from player prefs
        musicSlider.value = musicFloat;
        sFXSlider.value = sfxFloat;

        //set music volume from player prefs
        musicAudio.volume = musicFloat;
    }

    public void SaveSoundSettings() //Save settings when back button is pressed
    {
        PlayerPrefs.SetFloat(MusicPref, musicSlider.value); //save music values to player prefs
        PlayerPrefs.SetFloat(SFXPref, sFXSlider.value);//save music values to player prefs
    }

    void OnApplicationFocus (bool inFocus) //if user leaves game or closes game; settings will save
    {
        if(!inFocus)
        {
            SaveSoundSettings();
        }
    }

    public void UpdateSound() //function that slider calls 
    {
        //update music when slider calls this function
        musicAudio.volume = musicSlider.value;

        //update sfx when slider calls function
        updateSFXVolume(rewardSFXAudioSource);
        updateSFXVolume(buttonSFXAudioSource);
        updateSFXVolume(obstacleSpawnSFXAudioSource);
        updateSFXVolume(playerMoveSFXAudioSource);
        updateSFXVolume(countDownSFXAudioSource);
        updateSFXVolume(deadZoneSFXAudioSource);
        updateSFXVolume(obstacleHitSFXAudioSource);
    }

    public void updateSFXVolume(List<AudioSource> sfxList) //updates a list of sfx volume componenet
    {
        for (int i = 0; i < sfxList.Count; i++)
        {
            sfxList[i].volume = sFXSlider.value;
        }

    }

    //play sfx functions
    public void playRewardSFX()
    {
        playSFX(rewardSFX, rewardSFXAudioSource);
    }

    public void playButtonSFX()
    {
        playSFX(buttonSFX, buttonSFXAudioSource);
    }

    public void playObstacleHitSFX()
    {
        playSFX(obstacleHitSFX, obstacleHitSFXAudioSource);
    }

    public void playObstacleSpawnSFX()
    {
        playSFX(obstacleSpawnSFX, obstacleSpawnSFXAudioSource);
    }

    public void playPlayerMoveSFX()
    {
        playSFX(playerMoveSFX, playerMoveSFXAudioSource);
    }

    public void playDeadZoneSFX()
    {
        playSFX(deadZoneSFX, deadZoneSFXAudioSource);
    }

    public void playCountDownSFX()
    {
        playSFX(countDownSFX, countDownSFXAudioSource);
    }

    //sfx list constructor
    public void playSFX(GameObject sfxObject, List<AudioSource> sfxList) //function in order to replay a sfx
    {
        //Debug.Log("Test Win");
        foreach (AudioSource audioSource in sfxList) // will play the first nonactive sound playing inside the list
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                audioSource.volume = sFXSlider.value; //setting the volume according to slider
                return;
            }
        }
        //fill the list
        GameObject newsfxObject = Instantiate(sfxObject, transform); //Create a Game object in a parented object
        AudioSource sound = newsfxObject.GetComponent<AudioSource>(); //grab the audio component

        sfxList.Add(sound); //add sound AudioSource to the list
        sound.volume = sFXSlider.value; //setting the volume according to slider
        sound.Play();//plays the sound
    }

    //Music Pause Function
    public void pauseGameMusic()
    {
        musicAudio.Pause();
    }

    //Music Resume Function
    public void resumeGameMusic()
    {
        musicAudio.UnPause();
    }
}




