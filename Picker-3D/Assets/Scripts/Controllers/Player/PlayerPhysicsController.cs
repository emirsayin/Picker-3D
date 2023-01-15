using Controllers.Pool;
using DG.Tweening;
using Managers;
using Signals;
using System.Collections;
using UnityEngine;
using TMPro;

namespace Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public int TotalBalls;

        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private new Collider collider;
        [SerializeField] private new Rigidbody rigidbody;

        #endregion

        #region Private Variables

        private PoolController poolController;
        private int score;

        #endregion

        #endregion

        public void Awake()
        {
            poolController = GameObject.Find("PoolPhysics").GetComponent<PoolController>();
            TotalBalls = 0;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("StageArea"))
            {
                manager.ForceCommand.Execute();
                CoreGameSignals.Instance.onStageAreaEntered?.Invoke();
                InputSignals.Instance.onDisableInput?.Invoke();
                DOVirtual.DelayedCall(3, () =>
                {
                    var result = other.transform.parent.GetComponentInChildren<PoolController>()
                        .TakeStageResult(manager.StageValue);
                    if (result)
                    {
                        CoreGameSignals.Instance.onStageAreaSuccessful?.Invoke(manager.StageValue);
                        InputSignals.Instance.onEnableInput?.Invoke();
                    }
                    else CoreGameSignals.Instance.onLevelFailed?.Invoke();
                });
                return;
            }

            if (other.CompareTag("Finish"))
            {
                CoreGameSignals.Instance.onFinishAreaEntered?.Invoke();
                InputSignals.Instance.onDisableInput?.Invoke();
                CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
                return;
            }

            if (other.CompareTag("MiniGame"))
            {
                StartCoroutine(MinigameFinish());
            }
        }

        IEnumerator MinigameFinish()
        {
            int Collected = TotalBalls;

            Debug.Log("Time " + Collected / 10);

            yield return new WaitForSecondsRealtime(Collected / 10);

            InputSignals.Instance.onDisableInput?.Invoke();
            CoreGameSignals.Instance.onFinishAreaEntered?.Invoke();

            score = (CalculateScore() * TotalBalls);

            Debug.Log(CalculateScore());

            CoreGameSignals.Instance.onLevelSuccessful?.Invoke();

            GameObject.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = ("Score: " + score);
        }

        public int CalculateScore()
        {
            float location = gameObject.transform.position.z;

            if (location < 365) { return 3; }
            if (location < 375) { return 4; }
            if (location < 385) { return 5; }
            if (location < 395) { return 6; }
            if (location < 405) { return 7; }
            if (location < 415) { return 8; }
            if (location < 425) { return 9; }
            if (location < 435) { return 10; }
            if (location < 445) { return 11; }

            else { return 0; }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            var transform1 = manager.transform;
            var position = transform1.position;
            Gizmos.DrawSphere(new Vector3(position.x, position.y - 1.2f, position.z + 1f), 1.65f);
        }

        internal void OnReset()
        {
            Application.LoadLevel(0);
        }
    }
}