using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class BallGameLogic : MonoBehaviour
    {
        [Header("Referências")]
        public BallMoviment ballMoviment;

        [Header("UI")]
        public TextMeshProUGUI leftScoreText;
        public TextMeshProUGUI rightScoreText;
        public TextMeshProUGUI pointMessageText;

        private const string leftGoalTag = "leftGoal";
        private const string rightGoalTag = "rightGoal";

        private int leftScore = 0;
        private int rightScore = 0;

        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            UpdateScoreUI();
            pointMessageText.enabled = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            GameObject goal = collision.gameObject;

            if (goal.CompareTag(leftGoalTag))
            {
                rightScore++;
                pointMessageText.text = "Jogador 2 fez ponto!";
                HandlePoint();
            }
            else if (goal.CompareTag(rightGoalTag))
            {
                leftScore++;
                pointMessageText.text = "Jogador 1 fez ponto!";
                HandlePoint();
            }
        }

        private void HandlePoint()
        {
            UpdateScoreUI();
            pointMessageText.enabled = true;

            ballMoviment.StopBall();
            ballMoviment.SetPosition(Vector3.zero);

            StartCoroutine(ShowPointAndRestart());
        }

        private IEnumerator ShowPointAndRestart()
        {
            yield return new WaitForSeconds(2f);
            pointMessageText.enabled = false;
            yield return new WaitForSeconds(0.5f);

            ballMoviment.ResetSpeed();
            ballMoviment.LaunchBall(); // Internamente usa linearVelocity
        }

        private void UpdateScoreUI()
        {
            leftScoreText.text = leftScore.ToString();
            rightScoreText.text = rightScore.ToString();
        }
    }
}