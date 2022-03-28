using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.
using UnityEngine.SceneManagement;

public class MusicSliderPlayback : MonoBehaviour, IPointerUpHandler, IPointerDownHandler// These are the interfaces the OnPointerUp method requires.
{
    //OnPointerDown is also required to receive OnPointerUp callbacks
    public void OnPointerDown(PointerEventData eventData)
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            AudioManager.me.resumeGameMusic();
        }  
    }

    //Do this when the mouse click on this selectable UI object is released.
    public void OnPointerUp(PointerEventData eventData)
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            AudioManager.me.pauseGameMusic();
        }        
    }
}
