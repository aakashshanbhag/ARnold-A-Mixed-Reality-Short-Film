using UnityEngine;
using System.Collections;
using GlobalObject.SceneTransition;

namespace GlobalObject.SceneTransition.Example
{
    public class GoScript : MonoBehaviour
    {
        public string ToScene;

        public void GoToNextScene()
        {
            SceneLoader.LoadScene(ToScene);
        }
    }
}
