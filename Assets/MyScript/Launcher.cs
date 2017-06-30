using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour {

    public string loadScene;
    void Awake()
    {
        Debug.Log("Launcher Awake");
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        Debug.Log("Launcher Start");
        StartCoroutine(LoadSence());
    }
    IEnumerator LoadSence()
    {
        if (!string.IsNullOrEmpty(loadScene))
        {
            Application.LoadLevelAsync(loadScene);
        }
        else
        {
            int levelCount = Application.levelCount;
            int curLevel = Application.loadedLevel;
            if (curLevel + 1 < levelCount)
                Application.LoadLevelAsync(curLevel + 1);
        }
        yield return 0;
        Invoke("OnFinish", 0.5f);
        //OnFinish();  
    }
    void OnFinish()
    {
        if (Application.platform.Equals(RuntimePlatform.Android))
        {
            using (AndroidJavaClass jc = new AndroidJavaClass("com.qiyi.openapi.demo.activity.ARVideoActivity"))
            {
                using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("mActivity"))
                {
                    jo.Call("hideSplash");
                }

            }
        }
        //Destroy(gameObject);
    }
}  
