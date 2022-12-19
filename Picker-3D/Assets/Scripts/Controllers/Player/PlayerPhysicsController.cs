using UnityEngine;
using Managers;
using Signals;

namespace Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self variables

        #region Serialized Variables
        

        [SerializeField] public new Collider collider;
        [SerializeField] public new Rigidbody rigidbody;
        [SerializeField] private PlayerManager manager;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("StageArea"))
            {
                CoreGameSignals.Instance.onStageAreaEntered?.Invoke();
                InputSignal.Instance.onDisableInput?.Invoke();
                
            }
        }

        public void OnReset()
        {
            
        }
    }
}