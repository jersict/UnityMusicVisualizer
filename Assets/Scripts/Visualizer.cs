using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Visualizer : MonoBehaviour
{
    int[][] spectrumFrequencies;
    public GameObject capsulePrefab;
    public GameObject cubePrefab;
    public GameObject lightPrefab;
    public GameObject prefab;
    public GameObject cam;

    public static Visualizer visualizer;

    Dictionary<int, Frequencies> FrequenciesList = new Dictionary<int, Frequencies>();
    Dictionary<int, GameObject> cubes = new Dictionary<int, GameObject>();
    Dictionary<int, GameObject> capsules = new Dictionary<int, GameObject>();

    float zamik = 16f;
    float step = 1f;
    int i = 0;
    string mode = "current";
    
    // Start is called before the first frame update
    void Start()
    {
        
        spectrumFrequencies = new int[36][]
        {
            new int[2] {20, 40},
            new int[2] {41, 60},
            new int[2] {61, 80},
            new int[2] {81, 100},
            new int[2] {101, 120},
            new int[2] {121, 140},
            new int[2] {141, 160},
            new int[2] {161, 180},
            new int[2] {181, 200},
            new int[2] {201, 240},
            new int[2] {241, 280},
            new int[2] {281, 320},
            new int[2] {321, 360},
            new int[2] {361, 400},
            new int[2] {401, 440},
            new int[2] {441, 480},
            new int[2] {481, 520},
            new int[2] {521, 560},
            new int[2] {561, 600},
            new int[2] {601, 680},
            new int[2] {681, 760},
            new int[2] {761, 840},
            new int[2] {841, 920},
            new int[2] {901, 1000},
            new int[2] {1001, 1200},
            new int[2] {1201, 1400},
            new int[2] {1401, 1600},
            new int[2] {1601, 1800},
            new int[2] {1801, 2000},
            new int[2] {2001, 2500},
            new int[2] {2501, 3000},
            new int[2] {3001, 3500},
            new int[2] {3501, 4000},
            new int[2] {4001, 5000},
            new int[2] {5001, 6000},
            new int[2] {6001, 7000},

        };
        for (int i = 0; i < spectrumFrequencies.Length; i++)
        {
            var group = new Frequencies
            {
                index = i
            };
            FrequenciesList.Add(i, group);
        }
        UpdateFrequenciesList();
        definePrefabs();
        

      

    }

    void definePrefabs()
    {
        capsules.Clear();
        if (mode != "global")
        {
            i = 0;
        }
        if (prefab == null)
        {
            prefab = capsulePrefab;
        }
        foreach (var freq in FrequenciesList)
        {
            
            var group = freq.Value;
            
            var capsule = Instantiate(prefab);
            capsule.transform.SetParent(transform);
            capsule.name = group.index.ToString();
            capsule.transform.position = Vector3.zero;
            if (mode == "current")
            {
                prefab = capsulePrefab;
                var scale = Vector3.one;
                scale.x = 0.001f;
                capsule.transform.localScale = scale; 
                capsule.transform.Translate(new Vector3((int)(i / 36) * 0.2f - zamik, 0, step * i % 36 - zamik), Space.Self);
            }
            if (mode == "global")
            {
                prefab = cubePrefab;
                var scale = Vector3.one;
                scale.x = 0.1f;
                capsule.transform.localScale = scale;
                capsule.transform.Translate(new Vector3((int)(i / 36) * 0.2f - zamik, 0, step * i%36 - zamik), Space.Self); ;
            }
            capsule.SetActive(true);
            var mat = capsule.GetComponent<MeshRenderer>().material;
            var color = Color.HSVToRGB(1f / FrequenciesList.Count * (i%36), 1, 1);
            if (mode == "light")
            {

                prefab = lightPrefab;
                var scale = Vector3.one * 0.3f;
                capsule.transform.localScale = scale;
                capsule.transform.Translate(new Vector3(step * i - zamik, 700, 0), Space.Self);
            }
            if (mode == "circle")
            {
                capsule.transform.position = Vector3.zero;
                this.transform.eulerAngles = new Vector3(0, -10f * i, 0);
                capsule.transform.position = Vector3.forward * 10;
            }




            mat.color = color;

            capsules.Add(group.index, capsule);
            i++;
            
        }
    }
    public void switchMode()
    {
        if (mode == "current")
        {
            modeLight();
        }
        else if (mode == "light")
        {
            modeCircle();
        }
        else if (mode == "circle")
        {
            modeGlobal();
        }
        else if (mode == "global")
        {
            modeCurrent();
        }
    }

    public void modeGlobal()
    {
        destroyChildren();
        if (mode == "light")
        {
            foreach (var item in capsules)
            {
                item.Value.SetActive(false);
            }

        }
        if (mode != "global")
        {
            mode = "global";
            cam.transform.rotation = Quaternion.Euler(40.0f, 0.0f, 0f);
            prefab = cubePrefab;
        }
        
    }
    public void modeCurrent()
    {
        if (mode == "light")
        {
            foreach (var item in capsules)
            {
                item.Value.SetActive(false);
            }
            mode = "current";
            cam.transform.rotation = Quaternion.Euler(20.0f, -90.0f, 0f);
            prefab = capsulePrefab;
            destroyChildren();
            definePrefabs();
        }

        if (mode != "current")
        {
            
            mode = "current";
            cam.transform.rotation = Quaternion.Euler(20.0f, -90.0f, 0f);
            prefab = capsulePrefab;
            destroyChildren();
            definePrefabs();
        }
    }
    public void modeLight()
    {
        if (mode != "light")
        {
            mode = "light";
            cam.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0f);
            prefab = lightPrefab;
            
            foreach (var item in capsules)
            {
                item.Value.SetActive(false);
            }

            capsules.Clear();
            definePrefabs();
        }
        
    }
    public void modeCircle()
    {
        if (mode != "circle")
        {
            mode = "circle";
            cam.transform.rotation = Quaternion.Euler(20.0f, 90.0f, 0f);
            cam.transform.position = new Vector3(-20f, 10f, 0f);
            prefab = cubePrefab;

            foreach (var item in capsules)
            {
                item.Value.SetActive(false);
            }
            destroyChildren();
            capsules.Clear();
            definePrefabs();
        }

    }



    // Update is called once per frame
    void Update()
    {
        Debug.Log(mode);
        
        UpdateFrequenciesList();
        Frequencies prevGroup = FrequenciesList[spectrumFrequencies.Length - 1];
        foreach (var item in FrequenciesList)
        {
            var group = item.Value;
            var norm = group.dataNormalized;
            var capsule = capsules[group.index];
            var scale = Vector3.one;
            
            scale.y = norm * 10f;
            scale = Vector3.Lerp(capsule.transform.localScale, scale, 0.4f);
            
            prevGroup = group;
            if (mode == "global")
            {
                scale.x = 0.2f;
                cam.transform.position = new Vector3(capsule.transform.position.x, 11, -20);
                capsule.transform.localScale = scale;
            }
            if (mode == "current")
            {
                cam.transform.position = new Vector3(3, 11f, 2f);
                capsule.transform.localScale = scale;
            }
            if (mode == "light" )
            {   
                var pos = new Vector3(capsule.transform.position.x, 690 + 10*norm, capsule.transform.position.z);
                
                cam.transform.position = new Vector3(0f, 700.0f, -30);
                capsule.transform.position = pos;
            }
            if (mode == "circle")
            {
                var pos = new Vector3(capsule.transform.position.x, 10 * norm, capsule.transform.position.z);
                var point = new Vector3(0f, cam.transform.position.y, 0f);
                capsule.transform.position = pos;
                cam.transform.RotateAround(Vector3.zero, Vector3.up, Time.deltaTime);
            }
        }
        

        if (Input.GetKeyDown(KeyCode.V))
        {
            modeCurrent();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            modeLight();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            modeGlobal();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            modeCircle();
        }
        
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown("tab"))
        {
            switchMode();
        }
        if (mode == "global")
        {
            definePrefabs();
        }
    }
    public void UpdateFrequenciesList()
    {
        var hzPerSample = (AudioSettings.outputSampleRate / 8192f);

        var min = Mathf.Infinity;
        var max = -Mathf.Infinity;
        var Frequencies = new float[8192];

        AudioListener.GetSpectrumData(Frequencies, 0, FFTWindow.BlackmanHarris);

        for (int i = 0; i < spectrumFrequencies.Length - 1; i++)
        {
            var range = spectrumFrequencies[i];
            var group = FrequenciesList[i];
            group.data = 0;

            var minIndex = Mathf.FloorToInt(range[0] / hzPerSample);
            var maxIndex = Mathf.CeilToInt(range[1] / hzPerSample);

            for (var si = minIndex; si <= maxIndex; si++)
            {
                group.AddData(Frequencies[si]);
            }

            if (min > group.data)
            {
                min = group.data;
            }
            if (max < group.data)
            {
                max = group.data;
            }
        }
        foreach (var item in FrequenciesList)
        {
            item.Value.dataNormalized = Mathf.Clamp((item.Value.data - min) / (max - min), 0.01f, 1f);
        }
    }
    public void destroyChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
