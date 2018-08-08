using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoisePoint
{
    public Vector2 pos;
    public Vector2 vel;
    public float maxX;
    public float maxY;
    public bool isFinished;
}

public class VisualizeNoiseTest1 : MonoBehaviour 
{
    [Range(2, 512)]
    public int resolution = 256;


    public int numPoints = 1;

    public bool invert;

    private float[,] noise = new float[256, 256];
    private Color[] pixels;

    private Texture2D texture;

    private List<NoisePoint> pointsList;

    private bool visualizing;


    private float alpha = 0.1f;


    private void Start()
    {
        Random.InitState(Time.renderedFrameCount);
    }
    private void OnEnable()
    {
        if (texture == null)
        {
            texture = new Texture2D(resolution, resolution, TextureFormat.RGB24, true);
            texture.name = "Procedural Texture";
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Trilinear;
            texture.anisoLevel = 9;
            GetComponent<MeshRenderer>().material.mainTexture = texture;
        }
        generateNoise();

        ClearToRed();

        if (pointsList == null)
        {
            pointsList = new List<NoisePoint>();
        }

        ResetNoisePoints();

        if(pixels == null)
        {
            pixels = new Color[resolution * resolution];
        }

        visualizing = false;
    }

    void ResetNoisePoints()
    {
        if (pointsList != null)
        {
            pointsList.Clear();
            for (int i = 0; i < numPoints; i++)
            {
                NoisePoint np = new NoisePoint();
                np.pos = new Vector2(Random.Range(0, resolution), Random.Range(0, resolution));
                //np.pos = new Vector2(0,i);
                np.maxX = resolution;
                np.maxY = resolution;
                np.isFinished = false;
                pointsList.Add(np);
            }
        }
    }

    void ClearToWhite()
    {
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                pixels[(y * resolution) + (x)] = Color.white;
            }
        }
        texture.SetPixels(pixels);
        texture.Apply();
    }

    void ClearToBlack()
    {
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                pixels[(y * resolution) + (x)] = Color.black;
            }
        }
        texture.SetPixels(pixels);
        texture.Apply();
    }


    void ClearToRed()
    {
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                texture.SetPixel(x, y, Color.red);
            }
        }
        texture.Apply();
    }

    float smoothNoise(float x, float y)
    {
        //get fractional part of x and y
        float fractX = x - (int)(x);
        float fractY = y - (int)(y);

        //wrap around
        int x1 = ((int)(x) + resolution) % resolution;
        int y1 = ((int)(y) + resolution) % resolution;

        //neighbor values
        int x2 = (x1 + resolution - 1) % resolution;
        int y2 = (y1 + resolution - 1) % resolution;

        //smooth the noise with bilinear interpolation
        float value = 0.0f;
        value += fractX * fractY * noise[y1, x1];
        value += (1 - fractX) * fractY * noise[y1, x2];
        value += fractX * (1 - fractY) * noise[y2, x1];
        value += (1 - fractX) * (1 - fractY) * noise[y2, x2];

        return value;
    }

    void generateNoise()
    {
        for (int y = 0; y < resolution; y++)
            for (int x = 0; x < resolution; x++)
            {
                noise[y, x] = Random.value;
            }
    }

    float turbulence(float x, float y, float size)
    {
        float value = 0.0f;
        float initialSize = size;

        while (size >= 1)
        {
            value += smoothNoise(x / size, y / size) * size;
            size /= 2.0f;
        }

        return (128.0f * value / initialSize);
    }

	
	// Update is called once per frame
	void Update () 
    {
        if (pixels == null || pointsList == null)
            return;

        if (!visualizing)
            return;
        
        for (int i = 0; i < pointsList.Count; i++)
        {
            if(pointsList[i].isFinished)
            {continue;}

            if (pointsList[i].pos.x > resolution-1)
            {
                Debug.Log("Finishing point at:" + pointsList[i].pos);
                pointsList[i].isFinished = true;
            }

            if (pointsList[i].pos.x < 0)
            {
                Debug.Log("Finishing point at:" + pointsList[i].pos);
                pointsList[i].isFinished = true;
            }

            if (pointsList[i].pos.y > resolution-1)
            {
                Debug.Log("Finishing point at:" + pointsList[i].pos);
                pointsList[i].isFinished = true;
               
            }
            if (pointsList[i].pos.y < 0)
            {
                Debug.Log("Finishing point at:" + pointsList[i].pos);
                pointsList[i].isFinished = true;
            }

            //Debug.Log("Setting pixel Value for:" + (((int)pointsList[i].pos.y * (resolution - 1)) + (int)pointsList[i].pos.x));
            if(invert)
            {
                pixels[((int)pointsList[i].pos.y * (resolution)) + (int)pointsList[i].pos.x] = new Color(alpha, alpha, alpha, alpha);//Color.white;
            }
            else
            {
                pixels[((int)pointsList[i].pos.y * (resolution)) + (int)pointsList[i].pos.x] = new Color(1 - alpha, 1 - alpha, 1 - alpha, alpha);//Color.black;//new Color(1-alpha,1 - alpha,1 - alpha,alpha);
            }

            if(pointsList[i].isFinished)
            {
                pixels[((int)pointsList[i].pos.y * (resolution)) + (int)pointsList[i].pos.x] = Color.red;
            }
            //vel.x = Mathf.Cos( noise(pos.x * .01, pos.y * .01) * (2*Mathf.PI));
            //vel.y = -Mathf.Sin(noise(pos.x * .01, pos.y * .01) * (2 * Mathf.PI));
            //pointsList[i].vel.x = Mathf.Cos(smoothNoise(pointsList[i].pos.x *0.01f, pointsList[i].pos.y* 0.01f) * (2 * Mathf.PI));
            //pointsList[i].vel.y = -Mathf.Sin(smoothNoise(pointsList[i].pos.x * 0.01f, pointsList[i].pos.y * 0.01f) * (2 * Mathf.PI));
            pointsList[i].vel.x = 1;//Mathf.Cos(pointsList[i].pos.x * Mathf.PI);;
            //pointsList[i].vel.y = Mathf.Sin(smoothNoise(pointsList[i].pos.x * 0.01f, pointsList[i].pos.y * 0.01f) * (2 * Mathf.PI));
            //pointsList[i].vel.y = Mathf.Sin((pointsList[i].pos.y * 0.01f) * (2 * Mathf.PI));
            //pointsList[i].vel.y = Mathf.Sin(smoothNoise(pointsList[i].pos.y * 0.01f, pointsList[i].pos.y * 0.01f) * (2 * Mathf.PI));
            pointsList[i].vel.y = -Mathf.Sin((pointsList[i].pos.x * 0.01f) * (Mathf.PI));
            //pointsList[i].vel.x = Mathf.Cos(Random.value * (2 * Mathf.PI));
            //pointsList[i].vel.y = -Mathf.Sin(Random.value * (2 * Mathf.PI));


            pointsList[i].pos.x = pointsList[i].pos.x + pointsList[i].vel.x;
            pointsList[i].pos.y = pointsList[i].pos.y + pointsList[i].vel.y;
        }

        texture.SetPixels(pixels);
        texture.Apply();

        alpha += Time.deltaTime/8;
        //Debug.Log("pointsList count: "+pointsList.Count);
		
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width * 0.05f, Screen.height * 0.05f, Screen.width * 0.1f, Screen.height * 0.04f), "ClearToWhite"))
        {
            ClearToWhite();
        }

        if (GUI.Button(new Rect(Screen.width * 0.05f, Screen.height * 0.1f, Screen.width * 0.1f, Screen.height * 0.04f), "Update"))
        {
            alpha += 0.1f;
            texture.SetPixels(pixels);
            texture.Apply();
        }

        if (GUI.Button(new Rect(Screen.width * 0.05f, Screen.height * 0.15f, Screen.width * 0.1f, Screen.height * 0.04f), "Start"))
        {
            visualizing = true;
        }

        if (GUI.Button(new Rect(Screen.width * 0.05f, Screen.height * 0.2f, Screen.width * 0.1f, Screen.height * 0.04f), "Stop"))
        {
            visualizing = false;
        }

        if (GUI.Button(new Rect(Screen.width * 0.05f, Screen.height * 0.25f, Screen.width * 0.1f, Screen.height * 0.04f), "Reset"))
        {
            if (invert)
            {
                ClearToBlack();
            }
            else
            {
                ClearToWhite();
            }
            ResetNoisePoints();
            alpha = 0.1f;

            //Random.InitState(Time.frameCount);
        }

        if (GUI.Button(new Rect(Screen.width * 0.05f, Screen.height * 0.3f, Screen.width * 0.1f, Screen.height * 0.04f), "Show Points"))
        {
            for (int i = 0; i < pointsList.Count; i++)
            {
                if (invert)
                {
                    pixels[((int)pointsList[i].pos.y * (resolution - 1)) + (int)pointsList[i].pos.x] = Color.white;
                }
                else
                {
                    pixels[((int)pointsList[i].pos.y * (resolution - 1)) + (int)pointsList[i].pos.x] = Color.black;
                }

            }

            //for (int i = 0; i < pointsList.Count; i++)
            //{
            //    int index = ((resolution) * i) + i;
            //    Debug.Log("index: "+index);
            //    if(invert)
            //    {
            //        pixels[index] = Color.white;   
            //    }
            //    else
            //    {
            //        pixels[index] = Color.black;    
            //    }

            //}
            texture.SetPixels(pixels);
            texture.Apply();
        }

    }
}
