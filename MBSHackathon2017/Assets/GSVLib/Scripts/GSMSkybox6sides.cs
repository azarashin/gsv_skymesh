using UnityEngine;
using System.Collections;

public class GSMSkybox6sides : MonoBehaviour
{

    private int width = 640;
    private int height = 480;

    private double longitude = 135.5932361;
    private double latitude = 34.7156198;

    private bool update_trigger = false; 

    private float rot_x, rot_y; 

    public Material cube_map_material = null;
    public Camera camera; 

    // Use this for initialization
    void Start()
    {
        rot_x = 0.0f;
        rot_y = 0.0f; 
        StartCoroutine(GetStreetViewImage());
        update_trigger = true; 
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            rot_x -= Input.GetAxis("Mouse X");
            if (rot_x < 0)
            {
                rot_x += 360;
            }
            if (rot_x > 360)
            {
                rot_x -= 360;
            }

            rot_y += Input.GetAxis("Mouse Y");
            if (rot_y < -90)
            {
                rot_y = -90;
            }
            if (rot_y > 90)
            {
                rot_y = 90;
            }
        }


        camera.transform.localRotation = Quaternion.Euler(rot_y, rot_x, 0);
    }

    private IEnumerator GetStreetViewImage()
    {
        while(true)
        {
            if(!update_trigger)
            {
                yield return new WaitForSeconds(0.1f); 
                continue; 
            }
            update_trigger = false; 

            WWW www_front = LoadSkyboxTexture(0.0, 0.0);
            yield return www_front;

            WWW www_left = LoadSkyboxTexture(90.0, 0.0);
            yield return www_left;

            WWW www_back = LoadSkyboxTexture(180.0, 0.0);
            yield return www_back;

            WWW www_right = LoadSkyboxTexture(270.0, 0.0);
            yield return www_right;

            WWW www_up = LoadSkyboxTexture(0.0, 90.0);
            yield return www_up;

            WWW www_down = LoadSkyboxTexture(0.0, -90.0);
            yield return www_down;


            cube_map_material.SetTexture(Shader.PropertyToID("_FrontTex"), www_front.texture);
            cube_map_material.SetTexture(Shader.PropertyToID("_LeftTex"), www_left.texture);
            cube_map_material.SetTexture(Shader.PropertyToID("_BackTex"), www_back.texture);
            cube_map_material.SetTexture(Shader.PropertyToID("_RightTex"), www_right.texture);
            cube_map_material.SetTexture(Shader.PropertyToID("_UpTex"), www_up.texture);
            cube_map_material.SetTexture(Shader.PropertyToID("_DownTex"), www_down.texture);

        }



    }

    private WWW LoadSkyboxTexture(double heading, double pitch)
    {
        string url = "http://maps.googleapis.com/maps/api/streetview?" + "size=" + width + "x" + height + "&location=" + latitude + "," + longitude + "&heading=" + heading + "&pitch=" + pitch + "&fov=90&sensor=false";

        WWW www = new WWW(url);
        return www;
    }

    public void UpdateData(double lat, double lng)
    {
        longitude = lng;
        latitude = lat;
        update_trigger = true; 
    }
}
