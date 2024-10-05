using Core.Game;
using Fireflies;
using Knight;
using Player;
using Player.Data;
using UnityEngine;
using Zenject;

namespace Core
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig playerConfig;
        
        public override void InstallBindings()
        {
            BindPlayer();
            BindFireflies();
            BindCore();
            BindKnight();
        }

        private void BindKnight()
        {
            Container.Bind<KnightMovement>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
        }

        private void BindCore()
        {
            Container.Bind<GameStates>()
                .AsSingle()
                .NonLazy();
        }

        private void BindFireflies()
        {
            Container.Bind<FirefliesContainer>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindPlayer()
        {
            Container.BindInterfacesAndSelfTo<PlayerInputListener>()
                .AsSingle()
                .NonLazy();

            Container.Bind<FirefliesMovement>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();

            Container.Bind<PlayerConfig>()
                .FromInstance(playerConfig)
                .AsSingle()
                .NonLazy();
        }
    }
}