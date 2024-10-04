using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBird
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public bool IsStart { get; set; }
        public bool IsDeath { get; set; }
        public int Score { get; set; }
        public int BestScore { get; set; }

        private void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            IsStart = false;
            IsDeath = false;
            Score = 0;
            //PlayerPrefs.DeleteAll();


            BestScore = PlayerPrefs.GetInt("BestScore");
        }

        public void GetBestScore()
        {
            if (Score > BestScore)
            {
                BestScore = Score;

                PlayerPrefs.SetInt("BestScore", BestScore);
                UIManager.instance.newUI_text.text = "NEW";
            }
            else if (Score == BestScore)
            {
                UIManager.instance.newUI_text.text = "SAME";
            }
            else
            {
                UIManager.instance.newUI_text.text = "";
            }
        }
    }
}