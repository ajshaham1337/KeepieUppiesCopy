using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownController : MonoBehaviour
{
    public static CountDownController instance;

    public int countDownTime;
    public Text countDownDisplay;
    public GameObject hud;

    //public TextMeshProUGUI countDownDisplay;
    
    public void Start()
    {
        Debug.Log("Start Coroutine");
        StartCoroutine(CountDownToStart());
        Debug.Log("End Coroutine");
    }

    public IEnumerator CountDownToStart()
    {
        while(countDownTime > 0)
        {
            countDownDisplay.text = countDownTime.ToString();

            yield return new WaitForSeconds(1f);

            countDownTime--;
        }

        countDownDisplay.text = "GO!";

        //GameController.instance.StartGame();

        yield return new WaitForSeconds(1f);

        countDownDisplay.gameObject.SetActive(false);
        hud.gameObject.SetActive(false);
        //Player.playerinstance.SetActive(true);
    }
}
