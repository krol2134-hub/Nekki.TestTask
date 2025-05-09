using AI;
using Core.Lose;
using Core.UpdateServices;
using Players;
using UI;
using UnityEngine;

namespace Core
{
    public class LevelBootstrapper : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private LoseView loseView;
        [SerializeField] private TimerUiView _timerUiView;
        [SerializeField] private PauseUiView _pauseUiView;
        
        [Space(10)] [Header("Settings")]
        [SerializeField] private EnemySettings enemySettings;

        private EnemyFactory _enemyFactory;
        private EnemyPool _enemyPool;
        private AIDirector _aiDirector;
        
        private PauseController _pauseController;
        private LoseController _loseController;
        private TimerController _timerController;
        private PauseUiMediator _pauseUiMediator;
        
        //TODO Use DI/VContanier
        private void Awake()
        {
            InstallAi();
            InstallGameplay();
            InstallUpdateService();
            InstallUI();
        }

        private void OnDestroy()
        {
            _pauseController.Dispose();
            _loseController.Dispose();
            _pauseUiMediator.Dispose();
        }

        private void InstallAi()
        {
            _enemyFactory = new EnemyFactory(player, enemySettings);
            _enemyPool = new EnemyPool(_enemyFactory, enemySettings);
            _aiDirector = new AIDirector(_enemyPool, enemySettings);
        }

        private void InstallGameplay()
        {
            _pauseController = new PauseController(player);
            _loseController = new LoseController(player, loseView);
            _timerController = new TimerController(_timerUiView);
        }

        private void InstallUpdateService()
        {
            var updateServiceGameObject = new GameObject(nameof(UpdateService));
            var updateService = updateServiceGameObject.AddComponent<UpdateService>();
            
            updateService.RegisterUpdateable(_timerController);
        }

        private void InstallUI()
        {
            _pauseUiMediator = new PauseUiMediator(_pauseController, _pauseUiView);
        }
    }
}