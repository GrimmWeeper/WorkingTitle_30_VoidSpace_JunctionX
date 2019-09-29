using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class ColorWheel
{
    public float r = 1;
    public float g = 1;
    public float b = 1;
    public float a = 1;
}


public class InstantiateCanvas : MonoBehaviour
{
    public GameObject prefabCanvasPixel;
    public GameObject parentImageTarget;
    MeshRenderer meshRenderer;
    // Start is called before the first frame update
    public float r, g, b, a;
    Dictionary<string, string> collection = new Dictionary<string, string>();

    private void Awake()
    {
        Debug.Log("AWAKE!!!");
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://canvas-f8087.firebaseio.com");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        reference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot data in snapshot.Children)
                {
                    string dataString = data.GetRawJsonValue();
                    //Debug.Log(dataString);
                    string dataKey = data.Key;
                    //Debug.Log(dataKey);

                    collection.Add(dataKey, dataString);
                    //Debug.Log(collection.Count);

                }
            }
        });
    }

    private void Start()
    {
        Debug.Log("START!!!");
        Invoke("PrefabInstantiation", 3.0f);
     
    }

    private void PrefabInstantiation()
    {
        Debug.Log(collection.Count);
        for (float x = -1f; x <= 1f; x += 0.1f)
        {
            for (float z = -1f; z <= 1f; z += 0.1f)
            {
                GameObject prefabInstance = Instantiate(prefabCanvasPixel, new Vector3(x, 0.02f, z), Quaternion.identity);
                meshRenderer = prefabInstance.GetComponent<MeshRenderer>();

                //Debug.Log("INSTANTIATION!!!");

                string prefabId = string.Format("m-{0}-{1}", Mathf.Round(x * 100), Mathf.Round(z * 100));

                ColorWheel colorObj = new ColorWheel();
                colorObj = JsonUtility.FromJson<ColorWheel>(collection[prefabId]);
                r = colorObj.r;
                g = colorObj.g;
                b = colorObj.b;
                a = colorObj.a;

                Globals.pixelColor.r = r;
                Globals.pixelColor.g = g;
                Globals.pixelColor.b = b;
                Globals.pixelColor.a = a;

                prefabInstance.GetComponent<Renderer>().material.name = prefabId;
                prefabInstance.transform.name = prefabId;
                meshRenderer.material.color = Globals.pixelColor;
                prefabInstance.transform.parent = parentImageTarget.transform;
                GameObject.Find(prefabId).GetComponent<changeColor>();
            }
        }
    }
}
