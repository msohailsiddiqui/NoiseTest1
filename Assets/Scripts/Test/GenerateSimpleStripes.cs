using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenerateSimpleStripes : MonoBehaviour 
{
    [Range(2, 512)]
    public int resolution = 256;

    private Texture2D texture;

    public int numPoints = 500;

   
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
	}


    void ClearToWhite()
    {
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                texture.SetPixel(x, y, Color.white);
            }
        }
        texture.Apply();
    }


	void SimpleFill()
    {
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                texture.SetPixel(x, y, Color.yellow);   
            }
        }
        texture.Apply();
    }

    void SimpleStripes()
    {
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                texture.SetPixel(x, y, new Color(x & 2, x & 2, x & 2, 1));
            }
        }
        texture.Apply();
    }

    void SimpleMesh()
    {
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                float xyVal = ((x & 2) | (y & 2));//+ (25 * turbulence(x, y, 128)) / 256.0f;
                texture.SetPixel(x, y, new Color(xyVal, xyVal, xyVal));//, (x & 2) | (y & 2), (x & 2) | (y & 2), 1));
            }
        }
        texture.Apply();
    }

    void SinStripes()
    {
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                texture.SetPixel(x, y, new Color(Mathf.Sin(x+y), Mathf.Sin(x + y), Mathf.Sin(x + y), 1));
            }
        }
        texture.Apply();
    }

    void SinStripes2()
    {
        float val;
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                float xyVal = ((resolution - (x*xFactor)) + (resolution - (y*yFactor))) + (25 * turbulence(x, y, 128)) / 256.0f;
                val = Mathf.Tan(xyVal);
                texture.SetPixel(x, y, new Color(val,val,val, 1));
            }
        }
        texture.Apply();
    }

    void SinStripes3()
    {
        float val;
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                val = 1-Mathf.Sin(x+y);
                texture.SetPixel(x, y, new Color(val, val, val, 1));
            }
        }
        texture.Apply();
    }

    public float xFactor = 1;
    public float yFactor = 1;

    void Stripes4()
    {
        float val;
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                float xyValue = (x*xFactor) + (y*yFactor) + (25* turbulence(x, y, 128)) / 256.0f;
                val = Mathf.Sin(xyValue * 3.14159f);
                texture.SetPixel(x, y, new Color(val, val, val, 1));
            }
        }
        texture.Apply();
    }

    void ConcentricCircles()
    {
        float val;
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                float xyValue = Mathf.Sqrt(x * x + y * y)+(150 * turbulence(x, y, 128)) / 256.0f;
                
                val = Mathf.Sin(xyValue);
                texture.SetPixel(x, y, new Color(val, val, val, 1));
            }
        }
        texture.Apply();
    }

    void AddTest()
    {
        float val1,val2;
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                float xyValue = Mathf.Sqrt(x * x + y * y) + (150 * turbulence(x, y, 128)) / 256.0f;

                val1 = Mathf.Sin(xyValue);
                val2 = Mathf.Sin(x + y);
                texture.SetPixel(x, y, new Color(val1+val2, val1+val2, val1+val2, 1));
            }
        }
        texture.Apply();
    }

    void MulTest()
    {
        float val1, val2;
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                float xyValue = Mathf.Sqrt(x * x + y * y) + (150 * turbulence(x, y, 128)) / 256.0f;

                val1 = Mathf.Sin(xyValue);
                val2 = Mathf.Sin(x + y);
                texture.SetPixel(x, y, new Color(val1 * val2, val1 * val2, val1 * val2, 1));
            }
        }
        texture.Apply();
    }

    void AddTest2()
    {
        float val1, val2, val3;
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                float xyValue = Mathf.Sqrt(x * x + y * y) + (150 * turbulence(x, y, 128)) / 256.0f;

                val1 = Mathf.Sin(xyValue);
                val2 = Mathf.Sin(x + y);
                val3 = ((x & 2) | (y & 2));
                texture.SetPixel(x, y, new Color(val1 + val2 + val3, val1 + val2+val3, val1 + val2+val3, 1));
            }
        }
        texture.Apply();
    }

    void AddTest3()
    {
        float val1, val2, val3, val4;
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                float xyValue = Mathf.Sqrt(x * x + y * y) + (150 * turbulence(x, y, 128)) / 256.0f;

                val1 = Mathf.Sin(xyValue);
                val2 = Mathf.Sin(x + y);
                val3 = ((x & 2) | (y & 2));
                val4 = 1 - (val1 + val2 + val3);
                texture.SetPixel(x, y, new Color(val4,val4,val4, 1));
            }
        }
        texture.Apply();
    }

    void AddTest4()
    {
        float val1, val2, val3, val4;
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                float xyValue = Mathf.Sqrt(x * x + y * y) + (150 * turbulence(x, y, 128)) / 256.0f;

                val1 = Mathf.Sin(xyValue);
                xyValue  = (x * xFactor) + (y * yFactor) + (25 * turbulence(x, y, 128)) / 256.0f;
                val2 = Mathf.Sin(xyValue * 3.14159f);
                texture.SetPixel(x, y, new Color(val1+val2, val1 + val2, val1 + val2, 1));
            }
        }
        texture.Apply();
    }

    float[,] noise = new float[256,256];
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
        value += fractX * fractY * noise[y1,x1];
        value += (1 - fractX) * fractY * noise[y1,x2];
        value += fractX * (1 - fractY) * noise[y2,x1];
        value += (1 - fractX) * (1 - fractY) * noise[y2,x2];

        return value;
    }

    void generateNoise()
    {
        for (int y = 0; y < resolution; y++)
            for (int x = 0; x < resolution; x++)
            {
            noise[y,x] = Random.value;
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


    void OnGUI()
    {
        if(GUI.Button(new Rect(Screen.width * 0.05f, Screen.height * 0.05f, Screen.width * 0.1f, Screen.height * 0.04f ), "SimpleFill"))
        {
            SimpleFill();
        }

        if (GUI.Button(new Rect(Screen.width * 0.05f, Screen.height * 0.1f, Screen.width * 0.1f, Screen.height * 0.04f), "SimpleStripes"))
        {
            SimpleStripes();
        }

        if (GUI.Button(new Rect(Screen.width * 0.05f, Screen.height * 0.15f, Screen.width * 0.1f, Screen.height * 0.04f), "SimpleMesh"))
        {
            SimpleMesh();
        }

        if (GUI.Button(new Rect(Screen.width * 0.05f, Screen.height * 0.2f, Screen.width * 0.1f, Screen.height * 0.04f), "SinStripes"))
        {
            SinStripes();
        }

        if (GUI.Button(new Rect(Screen.width * 0.05f, Screen.height * 0.25f, Screen.width * 0.1f, Screen.height * 0.04f), "SinStripes2"))
        {
            SinStripes2();
        }

        if (GUI.Button(new Rect(Screen.width * 0.05f, Screen.height * 0.3f, Screen.width * 0.1f, Screen.height * 0.04f), "SinStripes3"))
        {
            SinStripes3();
        }

        if (GUI.Button(new Rect(Screen.width * 0.05f, Screen.height * 0.35f, Screen.width * 0.1f, Screen.height * 0.04f), "Stripes4"))
        {
            Stripes4();
        }

        if (GUI.Button(new Rect(Screen.width * 0.05f, Screen.height * 0.4f, Screen.width * 0.1f, Screen.height * 0.04f), "ConcentricCircles"))
        {
            ConcentricCircles();
        }

        if (GUI.Button(new Rect(Screen.width * 0.05f, Screen.height * 0.45f, Screen.width * 0.1f, Screen.height * 0.04f), "AddTest"))
        {
            AddTest();
        }

        if (GUI.Button(new Rect(Screen.width * 0.05f, Screen.height * 0.5f, Screen.width * 0.1f, Screen.height * 0.04f), "AddTest2"))
        {
            AddTest2();
        }

        if (GUI.Button(new Rect(Screen.width * 0.05f, Screen.height * 0.55f, Screen.width * 0.1f, Screen.height * 0.04f), "AddTest3"))
        {
            AddTest3();
        }

        if (GUI.Button(new Rect(Screen.width * 0.05f, Screen.height * 0.6f, Screen.width * 0.1f, Screen.height * 0.04f), "MulTest"))
        {
            MulTest();
        }
        if (GUI.Button(new Rect(Screen.width * 0.05f, Screen.height * 0.65f, Screen.width * 0.1f, Screen.height * 0.04f), "AddTest4"))
        {
            AddTest4();
        }

    }
}
