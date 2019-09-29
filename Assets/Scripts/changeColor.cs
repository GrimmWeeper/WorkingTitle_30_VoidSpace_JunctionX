using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;

[System.Serializable]
class ColorPixel
{
    public float r;
    public float g;
    public float b;
    public float a;
}

public class changeColor : MonoBehaviour
{
    public GameObject canvasPixel;
    MeshRenderer renderer;    public bool refreshStarted;    public bool unsubStarted;
    // Start is called before the first frame update
    void Start()
    {
        renderer = canvasPixel.GetComponent<MeshRenderer>();
        //refreshStarted = false;
        //unsubStarted = false;
    }

    //IEnumerator Refresh()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(10f);
    //        if (!refreshStarted)
    //        {
    //            firebaseRefresh();
    //            refreshStarted = true;
    //            unsubStarted = false;
    //        }
    //        yield return new WaitForSeconds(3f);
    //        if (!unsubStarted)
    //        {
    //            unsubscribe();
    //            unsubStarted = true;
    //            refreshStarted = false;
    //        }
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://canvas-f8087.firebaseio.com");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        ////InvokeRepeating("firebaseRefresh", 3.0f, 8.0f);
        //StartCoroutine("Refresh");

        
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit Hit;
            if (Physics.Raycast(ray, out Hit))
            {
                if (Hit.transform.gameObject.transform.name == canvasPixel.transform.name)
                {
                    Debug.Log("Hit " + canvasPixel.transform.name);
                    Globals.pixelColor.a = 100; // reset alpha of pixelColor to be 100
                    renderer.material.color = Globals.pixelColor;
                    string targetMatName = renderer.material.name;
                    Debug.Log(targetMatName + " get updated!");

                    ColorPixel pixelIns = new ColorPixel();

                    pixelIns.r = Globals.pixelColor.r;
                    pixelIns.g = Globals.pixelColor.g;
                    pixelIns.b = Globals.pixelColor.b;
                    pixelIns.a = Globals.pixelColor.a;


                    string jsonString = JsonUtility.ToJson(pixelIns);

                    reference.Child(targetMatName).SetRawJsonValueAsync(jsonString);

                    // update canvas pixel color using renderer material color attributes
                    Globals.selected = true;
                }
               
            }
        }
    }

    //private void firebaseRefresh()
    //{
    //    FirebaseDatabase.DefaultInstance
    //    .GetReference(canvasPixel.name)
    //    .ValueChanged += HandleValueChanged;
    //    Debug.Log("Subscribed!");

        
    //}

    //private void unsubscribe()
    //{
    //    FirebaseDatabase.DefaultInstance
    //    .GetReference(canvasPixel.name)
    //    .ValueChanged -= HandleValueChanged;
    //    Debug.Log("Unsubscribed!");
    //}

    //void HandleValueChanged(object sender, ValueChangedEventArgs args)
    //{
    //    if (args.DatabaseError != null)
    //    {
    //        Debug.LogError("Database Error: " + args.DatabaseError.Message);
    //        return;
    //    }
    //    Dictionary<string, string> collection = new Dictionary<string, string>();
    //    string dataString = args.Snapshot.GetRawJsonValue();
    //    if (dataString == null)
    //    {
    //      }
    //    else
    //    {
    //        //Debug.Log("************" + dataString);
    //        ColorPixel colorTemplate = new ColorPixel();
    //        colorTemplate = JsonUtility.FromJson<ColorPixel>(dataString);
    //        Color newColor = new Color();
    //        newColor.r = colorTemplate.r;
    //        newColor.b = colorTemplate.b;
    //        newColor.g = colorTemplate.g;
    //        newColor.a = colorTemplate.a;
    //        renderer.material.color = newColor;
    //    }
    //}
}
