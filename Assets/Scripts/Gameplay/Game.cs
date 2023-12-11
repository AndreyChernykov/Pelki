using System.Collections;
using System.Linq;
using Cinemachine;
using Pelki.Configs;
using Pelki.Gameplay.Camera;
using Pelki.Gameplay.Characters;
using Pelki.Gameplay.Input;
using Pelki.Gameplay.SaveSystem;
using Pelki.UI;
using Pelki.UI.Screens;
using UnityEngine;

namespace Pelki.Gameplay
{
    public class Game
    {
        private readonly LevelsConfig _levelsConfig;
        private readonly ScreenSwitcher _screenSwitcher;
        private readonly IInput _input;
        private readonly CharactersConfig _charactersConfig;

        private Level _level;
        private LevelProgress _levelProgress;
        private CameraDistributor _cameraDistributor;
        private CinemachineVirtualCamera _virtualCamera;
        private PlayerCharacter _playerCharacter;
        private CinemachineFramingTransposer _framingTransposer;

        public Game(LevelsConfig levelsConfig, CharactersConfig charactersConfig, ScreenSwitcher screenSwitcher,
            IInput input, LevelProgress progress, CameraDistributor cameraDistributor)
        {
            _charactersConfig = charactersConfig;
            _input = input;
            _screenSwitcher = screenSwitcher;
            _levelsConfig = levelsConfig;
            _levelProgress = progress;
            _cameraDistributor = cameraDistributor;
            _virtualCamera = _cameraDistributor.VirtualCamera;
            _framingTransposer = _virtualCamera.GetComponentInChildren<CinemachineFramingTransposer>();
        }

        public void ThisUpdate()
        {
            if (_playerCharacter.isFacingRight)
            {
                _framingTransposer.m_TrackedObjectOffset.x = Mathf.Lerp(
                    _framingTransposer.m_TrackedObjectOffset.x, 
                    _cameraDistributor.CameraCenterOffsetX, 
                    _cameraDistributor.FlipRotationTime
                );
            }
            else
            {
                _framingTransposer.m_TrackedObjectOffset.x = Mathf.Lerp(
                    _framingTransposer.m_TrackedObjectOffset.x, 
                    -1 * _cameraDistributor.CameraCenterOffsetX,  
                    _cameraDistributor.FlipRotationTime
                );
            }
        }

        public void StartGame()
        {
            Level levelPrefab = _levelsConfig.DebugLevelPrefab;
            _level = Object.Instantiate(levelPrefab);
            Vector3 spawnPosition = _level.CharacterSpawnPosition;
            SavePoint spawnSavePoint = _level.SavePointsRegister[_levelProgress.LastSavePointId];
            spawnPosition = spawnSavePoint.transform.position;
            foreach (var savePointItem in _level.SavePointIdsRegister)
            {
                if (savePointItem.Key.Equals(spawnSavePoint)
                    //sttrox: ActivatedSavePoints является list, что не супер оптимизированно, но она вроде как тут и не нужна
                    && _levelProgress.ActivatedSavePoints.Contains(savePointItem.Value))
                {
                    spawnSavePoint.ActivateState();
                }
                else
                {
                    savePointItem.Key.NotActivateState();
                    savePointItem.Key.Saved += OnSaved;
                }
            }
            
            _playerCharacter = Object.Instantiate(_charactersConfig.PlayerCharacterPrefab,
                spawnPosition,
                Quaternion.identity, _level.transform);
            _playerCharacter.Construct(_input);
            
            _virtualCamera.Follow = _playerCharacter.transform;

            _screenSwitcher.ShowScreen<GameScreen>();
        }

        private void OnSaved(SavePoint savePoint)
        {
            var savePointId = _level.SavePointIdsRegister[savePoint];
            _levelProgress.AddActivatedSavePoint(savePointId);
            _levelProgress.Save();
            Debug.Log("Save on savepoint with ID: " + savePointId);
        }
    }
}