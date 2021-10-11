using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject lightsButton;
    private bool lightsOn;

    [SerializeField]
    private Button parkButton;
    public int parkedPlanes;

    [SerializeField]
    private GameObject[] airplanes;
    [SerializeField]
    private List<int> airplaneNumbers;
    [SerializeField]
    private GameObject[] airplaneLights;

    [SerializeField]
    private GameObject[] hangars;
    [SerializeField]
    private List<int> hangarNumbers;

    void Awake()
    {
        airplanes = GameObject.FindGameObjectsWithTag("Airplane");
        hangars = GameObject.FindGameObjectsWithTag("Hangar");

        lightsOn = false;

        AssignNumbers(airplanes, airplaneNumbers);
        AssignNumbers(hangars, hangarNumbers);
    }

    void Update()
    {
        CheckIfAllParked();
    }

    void AssignNumbers(GameObject[] array, List<int> numbers)
    {
        for (int i = array.Length - 1; i >= 0; i--)
        {
            int index = Random.Range(0, numbers.Count);
            array[i].gameObject.name = "" + numbers[index];            
            array[i].GetComponentInChildren<TextMeshPro>().SetText("" + numbers[index]);
            numbers.RemoveAt(index);
        }
    }

    public void Park()
    {
        ChangeButtonColor(1, 0, 0, .5f, parkButton);

        foreach (var a in airplanes)
        {
            a.GetComponent<AirplaneController>().park = true;            
        }
    }

    void CheckIfAllParked()
    {
        if (parkedPlanes == airplanes.Length)
        {
            ChangeButtonColor(0, 1, 0, .5f, parkButton);
        }
    }

    void ChangeButtonColor(int r, int g, int b, float alpha, Button button)
    {
        button.interactable = false;
        var newColor = button.colors;
        newColor.disabledColor = new Color(r, g, b, alpha);
        button.colors = newColor;
    }

    public void LightsOnOff()
    {
        if (!lightsOn)
        {
            lightsOn = true;
            lightsButton.GetComponentInChildren<Text>().text = "Lights Off";
            foreach (var a in airplanes)
            {
                a.GetComponent<AirplaneController>().lightsOn = true;
            }
        }
        else if (lightsOn)
        {
            lightsOn = false;
            lightsButton.GetComponentInChildren<Text>().text = "Lights On";
            foreach (var a in airplanes)
            {
                a.GetComponent<AirplaneController>().lightsOn = false;
            }
        }
    }
}
