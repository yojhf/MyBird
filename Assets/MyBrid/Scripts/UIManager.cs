using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyBird
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        [Header("ReadyUI")]
        public GameObject readyUI;
        [Header("Score")]
        public TMP_Text score_text;
        [Header("GameOverUI")]
        public GameObject gameoverUI;
        public TMP_Text newUI_text;
        public TMP_Text gameover_BestScore;
        public TMP_Text gameover_Score;
        [SerializeField] private string title = "TitleScene";

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            score_text.text = GameManager.instance.Score.ToString();
            gameover_Score.text = GameManager.instance.Score.ToString();
            gameover_BestScore.text = GameManager.instance.BestScore.ToString();
        }



        public void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void Menu()
        {
            SceneManager.LoadScene(title);
        }

    }
}