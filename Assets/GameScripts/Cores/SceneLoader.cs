using AFPS.Commons;
using AFPS.Utils;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AFPS.Cores
{
    [DefaultExecutionOrder(ScriptOrder.SCENE_LOADER)]
    public class SceneLoader : IJobState
    {
        [SerializeField] private string[] m_Scenes;

        public override void Initialize(params object[] objects)
        {
            StartCoroutine(nameof(LoadScenes));
        }

        public override void Register()
        {

        }

        public override void Unregister()
        {

        }

        public void SwitchScene(string sceneName, bool unloadCurrentScene = true)
        {
            if (unloadCurrentScene)
            {
                UnloadScenes();
            }

            StartCoroutine(_LoadScene(sceneName));
        }

        private IEnumerator LoadScenes()
        {
            IsDone = false;

            foreach (var scene in m_Scenes)
            {
                CoroutineWithData coroutine = new CoroutineWithData(_LoadScene(scene), this);
                yield return coroutine.Coroutine;
                yield return new WaitForEndOfFrame();
            }

            IsDone = true;
        }

        private IEnumerator _LoadScene(string scene)
        {
            var checkExisted = SceneManager.GetSceneByName(scene);

            if (checkExisted.IsValid())
                yield break;

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return true;
            }
        }

        private void UnloadScenes()
        {
            int c = SceneManager.sceneCount;

            for (int i = 0; i < c; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                SceneManager.UnloadSceneAsync(scene);
            }
        }
    }
}
