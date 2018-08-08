using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
    public RectTransform mainPanel;

    public TextureGenerationManager generationManager;

    public GameObject buttonObj;

	// Use this for initialization
	void Start () 
    {
        mainPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width * 0.3f);
        mainPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height);

        if(generationManager != null && buttonObj != null)
        {
            foreach (TextureGenerationType type in generationManager.typesList)
            {
                GameObject temp = GameObject.Instantiate(buttonObj);
                Button btn = temp.GetComponent<Button>();
                if(btn != null)
                {
                   
                    btn.onClick.AddListener(delegate { generationManager.SetTextureGenerationType(type.mainType, type.subType); });
                }
                Text buttonLabel = temp.GetComponentInChildren<Text>();
                if(buttonLabel != null)
                {
                    buttonLabel.text = type.subType;

                }
                temp.transform.parent = mainPanel.transform;
                temp.transform.SetAsLastSibling();
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
