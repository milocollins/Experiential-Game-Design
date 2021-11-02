using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectiveMode : MonoBehaviour
{
    public Shader defaultShader;
    public Shader grayscaleShader;
    private Material myMaterial;
    private bool isOn = false;

    void Awake()
    {
        Debug.Log("RUNNING");
        myMaterial = new Material(grayscaleShader);
        //ToggleDetectiveMode();
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Debug.Log("BOOM");
        //myMaterial = new Material(grayscaleShader);
        Graphics.Blit(source, destination, myMaterial);
        isOn = !isOn;
    }
    private void ToggleDetectiveMode(RenderTexture source, RenderTexture destination)
    {
        //if (isOn)
        //{
        //    myMaterial = new Material(defaultShader);
        //}
        //else
        //{
        //    myMaterial = new Material(grayscaleShader);
        //}
        myMaterial = new Material(grayscaleShader);
        Graphics.Blit(source, destination, myMaterial);
        isOn = !isOn;
    }
}
