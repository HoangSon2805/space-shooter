using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;

    Health _player;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        if(gameOverPanel) gameOverPanel.SetActive(false);

        var go = GameObject.FindGameObjectWithTag("Player");
        if (go) {
            _player = go.GetComponent<Health>();
            if (_player) _player.OnDead += OnPlayerDead;
        }
    }

    void OnDestroy() {
        if (_player) _player.OnDead -= OnPlayerDead;
    }

    void OnPlayerDead() {
        ShowGameOver();
    }
    public void ShowGameOver() {
        Time.timeScale = 0f;
        if (finalScoreText) finalScoreText.text = $"Score: {ScoreManager.Score}";
        if (gameOverPanel) gameOverPanel.SetActive(true);
    }

    public void Reload() {
        Time.timeScale = 1f;
        var scn = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scn.buildIndex);
    }

    public void Quit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
