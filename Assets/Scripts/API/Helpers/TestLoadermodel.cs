using System.Collections;
using System.Collections.Generic;
using API.Helpers;
using Siccity.GLTFUtility;
using UnityEngine;

public class TestLoadermodel : MonoBehaviour
{
    public string url;
    
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ModelLoader.DownloadModel(url, (model) => ModelLoader.GetModel(url));
        }
    }
}
