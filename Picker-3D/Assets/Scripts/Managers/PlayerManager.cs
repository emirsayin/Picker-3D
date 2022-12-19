using Controllers.Player;
using Data.UnityObjects;
using Data.ValueObjects;
using Sirenix.OdinInspector;
using UnityEngine;
using Signals;
using System;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerPhysicsController physicsController;
        [SerializeField] private PlayerMeshController meshController;

        #endregion

        #region Private Variables

        [ShowInInspector] private PlayerData _data;

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetPlayerData();
            SendDataToControllers();
        }

        private PlayerData GetPlayerData()
        {
            return Resources.Load<CD_Player>("Data/CD_Player").Data;
        }

        private void SendDataToControllers()
        {
            movementController.GetMovementData(_data.MovementData);
            meshController.GetMeshData(_data.ScaleData);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignal.Instance.onInputTaken += OnInputTaken;
            InputSignal.Instance.onInputReleased += OnInputReleased;
            InputSignal.Instance.onInputDragged += OnInputDragged;
            CoreGameSignals.Instance.onPlay += onPlay;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.onStageAreaEntered += OnStageAreaEntered;
            CoreGameSignals.Instance.onStageAreaSuccessful += OnStageAreaSuccessful;
            CoreGameSignals.Instance.onReset += OnReset;

        }

        private void UnSubscribeEvents()
        {
            InputSignal.Instance.onInputTaken -= OnInputTaken;
            InputSignal.Instance.onInputReleased -= OnInputReleased;
            InputSignal.Instance.onInputDragged -= OnInputDragged;
            CoreGameSignals.Instance.onPlay -= onPlay;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.onStageAreaEntered -= OnStageAreaEntered;
            CoreGameSignals.Instance.onStageAreaSuccessful -= OnStageAreaSuccessful;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void onPlay()
        {
            movementController.IsReadyToPlay(true);
        }

        private void OnInputDragged(HorizontalInputParams inputParams)
        {
            movementController.UpdateInputParams(inputParams);
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void OnInputTaken()
        {
            movementController.IsReadyToMove(true);
        }

        private void OnInputReleased()
        {
            movementController.IsReadyToMove(false);
        }

        private void OnReset()
        {
            movementController.OnReset();
            meshController.OnReset();
            physicsController.OnReset();
        }

        private void OnLevelFailed()
        {
            movementController.IsReadyToPlay(false);
        }

        private void OnLevelSuccessful()
        {
            movementController.IsReadyToPlay(false);
        }

        private void OnStageAreaEntered()
        {
            movementController.IsReadyToPlay(false);
        }

        private void OnStageAreaSuccessful()
        {
            movementController.IsReadyToPlay(true);
        }
    }
}
