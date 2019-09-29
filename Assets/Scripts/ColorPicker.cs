using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public static Color pixelColor; // selected pixel color
    public static bool selected = false;
}


public class ColorPicker : MonoBehaviour
{
    Image img;
    public Button btn;
    // Start is called before the first frame update
    void Start()
    {
        Button pixel = btn.GetComponent<Button>();
        pixel.onClick.AddListener(RememberColor);
        img = btn.GetComponent<Image>();

    }

    void RememberColor()
    {
        Globals.pixelColor = img.color;
        Debug.Log("Colour Remembered: " + img.color);
    }
}
