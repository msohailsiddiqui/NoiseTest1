using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTextureGenerator : MonoBehaviour 
{
    [Range(2, 512)]
    public int resolution = 256;

    private Texture2D texture;

    public delegate void GenerationMethod();

    public GenerationMethod generationMethod;

    public float frequency = 10f;


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
        if (generationMethod != null)
        {
            generationMethod();
        }
    }

    public void VisualizeUV()
    {
        if (texture.width != resolution)
        {
            texture.Resize(resolution, resolution);
        }
        float stepSize = 1f / resolution;
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                texture.SetPixel(x, y, new Color((x + 0.5f) * stepSize, (y + 0.5f) * stepSize, 0f));
            }
        }
        texture.Apply();
    }

    public void VisualizeUVTiled()
    {
        if (texture.width != resolution)
        {
            texture.Resize(resolution, resolution);
        }
        float stepSize = 1f / resolution;
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                texture.SetPixel(x, y,
                    new Color((x + 0.5f) * stepSize % 0.1f, (y + 0.5f) * stepSize % 0.1f, 0f) * 10f);
            }
        }
        texture.Apply();
    }

    public void VisualizeWorldSpace()
    {
        if (texture.width != resolution)
        {
            texture.Resize(resolution, resolution);
        }

        Vector3 point00 = transform.TransformPoint(new Vector3(-0.5f, -0.5f));
        Vector3 point10 = transform.TransformPoint(new Vector3(0.5f, -0.5f));
        Vector3 point01 = transform.TransformPoint(new Vector3(-0.5f, 0.5f));
        Vector3 point11 = transform.TransformPoint(new Vector3(0.5f, 0.5f));

        float stepSize = 1f / resolution;
        Random.InitState(42);
        for (int y = 0; y < resolution; y++)
        {
            Vector3 point0 = Vector3.Lerp(point00, point01, (y + 0.5f) * stepSize);
            Vector3 point1 = Vector3.Lerp(point10, point11, (y + 0.5f) * stepSize);
            for (int x = 0; x < resolution; x++)
            {
                Vector3 point = Vector3.Lerp(point0, point1, (x + 0.5f) * stepSize);
                texture.SetPixel(x, y, new Color(point.x, point.y, point.z));
                
            }
        }
        texture.Apply();
    }

    public void VisualizeLocalSpace()
    {
        if (texture.width != resolution)
        {
            texture.Resize(resolution, resolution);
        }

        Vector3 point00 = new Vector3(-0.5f, -0.5f);
        Vector3 point10 = new Vector3(0.5f, -0.5f);
        Vector3 point01 = new Vector3(-0.5f, 0.5f);
        Vector3 point11 = new Vector3(0.5f, 0.5f);

        float stepSize = 1f / resolution;
        for (int y = 0; y < resolution; y++)
        {
            Vector3 point0 = Vector3.Lerp(point00, point01, (y + 0.5f) * stepSize);
            Vector3 point1 = Vector3.Lerp(point10, point11, (y + 0.5f) * stepSize);
            for (int x = 0; x < resolution; x++)
            {
                Vector3 point = Vector3.Lerp(point0, point1, (x + 0.5f) * stepSize);
                texture.SetPixel(x, y, new Color(point.x, point.y, point.z));

            }
        }
        texture.Apply();
    }

    public void RandomNoise()
    {
        if (texture.width != resolution)
        {
            texture.Resize(resolution, resolution);
        }

        Vector3 point00 = new Vector3(-0.5f, -0.5f);
        Vector3 point10 = new Vector3(0.5f, -0.5f);
        Vector3 point01 = new Vector3(-0.5f, 0.5f);
        Vector3 point11 = new Vector3(0.5f, 0.5f);

        float stepSize = 1f / resolution;
        Random.InitState(42);
        for (int y = 0; y < resolution; y++)
        {
            Vector3 point0 = Vector3.Lerp(point00, point01, (y + 0.5f) * stepSize);
            Vector3 point1 = Vector3.Lerp(point10, point11, (y + 0.5f) * stepSize);
            for (int x = 0; x < resolution; x++)
            {
                Vector3 point = Vector3.Lerp(point0, point1, (x + 0.5f) * stepSize);
                texture.SetPixel(x, y, Color.white * Random.value);
            }
        }
        texture.Apply();
    }

    public void Stripes()
    {
        if (texture.width != resolution)
        {
            texture.Resize(resolution, resolution);
        }

        Vector3 point00 = new Vector3(-0.5f, -0.5f);
        Vector3 point10 = new Vector3(0.5f, -0.5f);
        Vector3 point01 = new Vector3(-0.5f, 0.5f);
        Vector3 point11 = new Vector3(0.5f, 0.5f);

        float stepSize = 1f / resolution;

        for (int y = 0; y < resolution; y++)
        {
            Vector3 point0 = Vector3.Lerp(point00, point01, (y + 0.5f) * stepSize);
            Vector3 point1 = Vector3.Lerp(point10, point11, (y + 0.5f) * stepSize);
            for (int x = 0; x < resolution; x++)
            {
                Vector3 point = Vector3.Lerp(point0, point1, (x + 0.5f) * stepSize);
                texture.SetPixel(x, y, Color.white* Noise.Stripes(point, frequency));
            }
        }
        texture.Apply();
    }

    public void RepeatingStripes()
    {
        if (texture.width != resolution)
        {
            texture.Resize(resolution, resolution);
        }

        Vector3 point00 = new Vector3(-0.5f, -0.5f);
        Vector3 point10 = new Vector3(0.5f, -0.5f);
        Vector3 point01 = new Vector3(-0.5f, 0.5f);
        Vector3 point11 = new Vector3(0.5f, 0.5f);

        float stepSize = 1f / resolution;
       

        for (int y = 0; y < resolution; y++)
        {
            Vector3 point0 = Vector3.Lerp(point00, point01, (y + 0.5f) * stepSize);
            Vector3 point1 = Vector3.Lerp(point10, point11, (y + 0.5f) * stepSize);
            for (int x = 0; x < resolution; x++)
            {
                Vector3 point = Vector3.Lerp(point0, point1, (x + 0.5f) * stepSize);
                texture.SetPixel(x, y, Color.white * Noise.RepeatingStripes(point, frequency));
            }
        }
        texture.Apply();
    }

    public void RandomStripes()
    {
        if (texture.width != resolution)
        {
            texture.Resize(resolution, resolution);
        }

        Vector3 point00 = new Vector3(-0.5f, -0.5f);
        Vector3 point10 = new Vector3(0.5f, -0.5f);
        Vector3 point01 = new Vector3(-0.5f, 0.5f);
        Vector3 point11 = new Vector3(0.5f, 0.5f);

        float stepSize = 1f / resolution;


        for (int y = 0; y < resolution; y++)
        {
            Vector3 point0 = Vector3.Lerp(point00, point01, (y + 0.5f) * stepSize);
            Vector3 point1 = Vector3.Lerp(point10, point11, (y + 0.5f) * stepSize);
            for (int x = 0; x < resolution; x++)
            {
                Vector3 point = Vector3.Lerp(point0, point1, (x + 0.5f) * stepSize);
                texture.SetPixel(x, y, Color.white * Noise.RandomStripes(point, frequency));
            }
        }
        texture.Apply();
    }

    public void Random2DBoxes()
    {
        if(texture.width != resolution)
        {
            texture.Resize(resolution, resolution);
        }

        Vector3 point00 = transform.TransformPoint(new Vector3(-0.5f, -0.5f));
        Vector3 point10 = transform.TransformPoint(new Vector3(0.5f, -0.5f));
        Vector3 point01 = transform.TransformPoint(new Vector3(-0.5f, 0.5f));
        Vector3 point11 = transform.TransformPoint(new Vector3(0.5f, 0.5f));

        float stepSize = 1f / resolution;


        for (int y = 0; y < resolution; y++)
        {
            Vector3 point0 = Vector3.Lerp(point00, point01, (y + 0.5f) * stepSize);
            Vector3 point1 = Vector3.Lerp(point10, point11, (y + 0.5f) * stepSize);
            for (int x = 0; x < resolution; x++)
            {
                Vector3 point = Vector3.Lerp(point0, point1, (x + 0.5f) * stepSize);
                texture.SetPixel(x, y, Color.white * Noise.Random2DBoxes(point, frequency));
            }
        }
        texture.Apply();
        
    }

    public void Random3DBoxes()
    {
        if (texture.width != resolution)
        {
            texture.Resize(resolution, resolution);
        }

        Vector3 point00 = transform.TransformPoint(new Vector3(-0.5f, -0.5f));
        Vector3 point10 = transform.TransformPoint(new Vector3(0.5f, -0.5f));
        Vector3 point01 = transform.TransformPoint(new Vector3(-0.5f, 0.5f));
        Vector3 point11 = transform.TransformPoint(new Vector3(0.5f, 0.5f));

        float stepSize = 1f / resolution;


        for (int y = 0; y < resolution; y++)
        {
            Vector3 point0 = Vector3.Lerp(point00, point01, (y + 0.5f) * stepSize);
            Vector3 point1 = Vector3.Lerp(point10, point11, (y + 0.5f) * stepSize);
            for (int x = 0; x < resolution; x++)
            {
                Vector3 point = Vector3.Lerp(point0, point1, (x + 0.5f) * stepSize);
                texture.SetPixel(x, y, Color.white * Noise.Random3DBoxes(point, frequency));
            }
        }
        texture.Apply();

    }

    public void SineStripes()
    {
        float xPeriod = 5.0f; //defines repetition of marble lines in x direction
        float yPeriod = 10.0f;

        if (texture.width != resolution)
        {
            texture.Resize(resolution, resolution);
        }

        Vector3 point00 = transform.TransformPoint(new Vector3(-0.5f, -0.5f));
        Vector3 point10 = transform.TransformPoint(new Vector3(0.5f, -0.5f));
        Vector3 point01 = transform.TransformPoint(new Vector3(-0.5f, 0.5f));
        Vector3 point11 = transform.TransformPoint(new Vector3(0.5f, 0.5f));

        float stepSize = 1f / resolution;
        for (int y = 0; y < resolution; y++)
        {
            Vector3 point0 = Vector3.Lerp(point00, point01, (y + 0.5f) * stepSize);
            Vector3 point1 = Vector3.Lerp(point10, point11, (y + 0.5f) * stepSize);
            for (int x = 0; x < resolution; x++)
            {
                Vector3 point = Vector3.Lerp(point0, point1, (x + 0.5f) * stepSize);
                float xyValue = x * xPeriod / resolution + y * yPeriod / resolution;// + turbPower * turbulence(x, y, turbSize) / 256.0;
                float sineValue = 256 * Mathf.Abs(Mathf.Sin(xyValue * 3.14159f));
                int sinVal = (int)sineValue;
                Color Temp = new Color(sinVal, sinVal, sinVal);
                texture.SetPixel(x, y, Temp);
            }
        }
        texture.Apply();
    }

    private void Update()
    {
        if (transform.hasChanged)
        {
            transform.hasChanged = false;
            if (generationMethod != null)
            {
                generationMethod();
            }
        }
    }
}
