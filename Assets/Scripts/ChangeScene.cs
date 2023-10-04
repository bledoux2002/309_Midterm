using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TMPro.Fitts
{
    public class ChangeScene : MonoBehaviour
    {
        public void SSScene()
        {
            SceneManager.LoadScene("SPIDERSHOT");
        }

        public void GSScene()
        {
            SceneManager.LoadScene("GRIDSHOT");
        }

        public void STScene()
        {
            SceneManager.LoadScene("STRAFETRACK");
        }
    }
}
