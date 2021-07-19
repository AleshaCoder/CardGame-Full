using UnityEngine;

public class MetalCollector : MonoBehaviour
{
    public MiniGameLauncher GameLauncher;
    private int _counter = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Metal")
        {
            Destroy(other.gameObject);
            _counter++;
            Debug.Log(_counter);

            if (_counter > 9)
            {
                GameLauncher.LoadMiniGame("MainGameScene");
            }
        }
    }
}
