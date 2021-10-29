using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    public static void NavTo(string s)
    {
        SceneManager.LoadScene(s);
    }
}
