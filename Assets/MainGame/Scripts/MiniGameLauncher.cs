using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniGameLauncher : MonoBehaviour
{
    public Image Blank;
    private string SceneID;
    public Image[] LoadingImage;
    public Text ProgressText;
    public Button ActivityButton;

    public void LoadMiniGame(string ID)
    {        
        if (ID != "")
        {
            SaveLoadController.SaveEvent();
            Blank.gameObject.SetActive(true);
            for (int i = 0; i < LoadingImage.Length; i++)
            {
                LoadingImage[i].gameObject.SetActive(true);
            }
            SceneID = ID;
            StartCoroutine(SyncLoad());
        }
    }

    IEnumerator SyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneID);
        while (operation.progress < 0.9f)
        {
            float progress = operation.progress / 0.9f;
            for (int i = 0; i < LoadingImage.Length; i++)
            {
                LoadingImage[i].fillAmount = progress;
            }
            ProgressText.text = string.Format("{0:0}%", progress);
            yield return null;
        }
    }
}
