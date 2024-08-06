using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

namespace com
{
    public class SceneService : MonoBehaviour
    {
        public List<string> sceneNames;

        private string _currentSceneName;
        private int _index;

        public static SceneService instance { get; private set; }

        private void Awake()
        {
            instance = this;
            SyncSceneName(0);
        }

        public void StartNextScene()
        {
            switch (_index)
            {

                default:
                    StartScene(_index + 1);
                    return;
            }

        }

        public void StartScene(int index)
        {
            SyncSceneName(index);
            LoadCurrentScene(IsNormalLevelIndex(index));
        }

        bool IsNormalLevelIndex(int index)
        {
            if (index == 0 || index == 4)
            {
                return false;
            }

            return true;
        }

        public void RestartScene()
        {
            LoadCurrentScene(true);
        }

        string GetSceneName(int index = 0)
        {
            index = Mathf.Clamp(index, 0, sceneNames.Count - 1);
            return sceneNames[index];
        }

        void SyncSceneName(int index)
        {
            _currentSceneName = GetSceneName(index);
            _index = index;
        }

        void LoadCurrentScene(bool needTransition)
        {
            //Debug.Log("LoadCurrentScene " + _currentSceneName);
            StartCoroutine(LoadYourAsyncScene(needTransition));
        }

        IEnumerator LoadYourAsyncScene(bool needTransition)
        {
            // The Application loads the Scene in the background as the current Scene runs.
            // This is particularly good for creating loading screens.
            // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
            // a sceneBuildIndex of 1 as shown in Build Settings.

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_currentSceneName);

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            //Debug.Log("load scene done " + _currentSceneName);
            if (needTransition)
            {
                TransitionBehaviour.instance.ShowSmaller();
            }
            else
            {
                TransitionBehaviour.instance.Hide();
            }
        }
    }
}