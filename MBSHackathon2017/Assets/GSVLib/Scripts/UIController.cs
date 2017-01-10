using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Text lat;
    public Text lng;
    public GSMSkybox6sides skybox; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Trigger()
    {
        double vlat, vlng;
        if(double.TryParse(lat.text, out vlat) && double.TryParse(lng.text, out vlng))
        {
            skybox.UpdateData(vlat, vlng);
        }
    }
}
