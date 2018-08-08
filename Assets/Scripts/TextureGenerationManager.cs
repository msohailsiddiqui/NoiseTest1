using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum MainGenerationType
//{
//    Test = 0,
//    Noise,
//    Pattern
//}

//public enum TestSubGenerationType
//{
//    Test = 0,
//    Noise,
//    Pattern
//}

//public class SubGenerationTypeObj
//{
//    public string subTypeName;
//    ProceduralTextureGenerator generationMethod;
//}

//public class MainGenerationTypeObj
//{
//    public Dictionary<string, SubGenerationTypeObj> subGenerationTypes;

//    public void Initialize()
//    {
//        subGenerationTypes = new Dictionary<string, SubGenerationTypeObj>();
//    }
//}

//public class GenerationSettings
//{
//    private Dictionary<string, MainGenerationTypeObj> mainGenerationTypes;

//    public void Initialize()
//    {
//        mainGenerationTypes = new Dictionary<string, MainGenerationTypeObj>();
//    }

//    public void AddMainGenerationType(string mainGenerationTypeName, SubGenerationTypeObj subObj )
//    {
//        if(mainGenerationTypes != null)
//        {
//            mainGenerationTypes.Add(mainGenerationTypeName, subObj);
//        }
        
//    }

//}

public class TextureGenerationData
{
    
}

public class TextureGenerationType
{
    public string mainType;
    public string subType;
    public ProceduralTextureGenerator.GenerationMethod generationMethod;

    public TextureGenerationType(string _mainType, string _subType, ProceduralTextureGenerator.GenerationMethod _generationMethod)
    {
        mainType = _mainType;
        subType = _subType;
        generationMethod = _generationMethod;
    }
}

public class TextureGenerationManager : MonoBehaviour 
{
    public List<TextureGenerationType> typesList;

    public ProceduralTextureGenerator generator;
    

	// Use this for initialization
	void Awake () 
    {
        typesList = new List<TextureGenerationType>();
        AddTextureGenerationType("Test", "VisualizeUV", generator.VisualizeUV);
        AddTextureGenerationType("Test", "VisualizeUVTiled", generator.VisualizeUVTiled);
        AddTextureGenerationType("Test", "VisualizeLS", generator.VisualizeLocalSpace);
        AddTextureGenerationType("Test", "VisualizeWS", generator.VisualizeWorldSpace);
        AddTextureGenerationType("Noise", "RandomNoise", generator.RandomNoise);
        AddTextureGenerationType("Pattern", "Stripes", generator.Stripes);
        AddTextureGenerationType("Pattern", "RepeatingStripes", generator.RepeatingStripes);
        AddTextureGenerationType("Pattern", "RandomStripes", generator.RandomStripes);
        AddTextureGenerationType("Pattern", "Random2DBoxes", generator.Random2DBoxes);
        AddTextureGenerationType("Pattern", "Random3DBoxes", generator.Random3DBoxes);
        AddTextureGenerationType("Pattern", "SineStripes", generator.SineStripes);
	}
	
    private void AddTextureGenerationType(string mainGenerationType, string subGenerationType, ProceduralTextureGenerator.GenerationMethod _generationMethod)
    {
        if(typesList != null)
        {
            TextureGenerationType temp = new TextureGenerationType(mainGenerationType, subGenerationType, _generationMethod);

            typesList.Add(temp);    
        }

    }

    public void SetTextureGenerationType(string mainGenerationType, string subGenerationType)
    {
        foreach(TextureGenerationType type in typesList)
        {
            if(type.mainType == mainGenerationType && type.subType == subGenerationType)
            {
                generator.generationMethod = type.generationMethod;
                generator.generationMethod();
            }
        }
    }
}
