using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

    private Slider pointsToWinSlider;
    private Slider vegiCountdownSlider;
    private Text valueDisplayPointsToWin;
    private Text valueDisplayVegiCountdown;


    void Start()
    {
        pointsToWinSlider = GameObject.Find("PointsToWinSlider").GetComponent<Slider>();
        pointsToWinSlider.value = GlobalData.PointsToWin;

        vegiCountdownSlider = GameObject.Find("VegiCountdownSlider").GetComponent<Slider>();
        vegiCountdownSlider.value = GlobalData.VegiCountdown;
    }

    public void OnPointsToWinChanged()
    {
        GlobalData.PointsToWin = (int) pointsToWinSlider.value;
        valueDisplayPointsToWin = GameObject.Find("ValueDisplayPointsToWin").GetComponent<Text>();
        valueDisplayPointsToWin.text = pointsToWinSlider.value.ToString();
    }

    public void OnVegiCountdownChanged()
    {
        GlobalData.VegiCountdown = (int)vegiCountdownSlider.value;
        valueDisplayVegiCountdown = GameObject.Find("ValueDisplayVegiCountdown").GetComponent<Text>();
        valueDisplayVegiCountdown.text = vegiCountdownSlider.value.ToString();

    }
}
