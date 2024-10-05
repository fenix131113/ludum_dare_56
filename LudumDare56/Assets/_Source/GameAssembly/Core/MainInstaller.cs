using Core.Game;
using Fireflies;
using Fireflies.Data;
using Knight;
using Player;
using Player.Data;
using UnityEngine;
using Zenject;
using UnityEngine.Rendering;

namespace Core
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private FirefliesConfig firefliesConfig;
        
        [SerializeField] private VolumeProfile postProcessVolume;
        
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
            
            Container.Bind<VolumeProfile>()
                .FromInstance(postProcessVolume)
                .AsSingle()
                .NonLazy();
        }

        private void BindFireflies()
        {
            Container.Bind<FirefliesContainer>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();

            Container.Bind<FirefliesHealth>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<HealthLightChecker>()
                .AsSingle()
                .NonLazy();

            Container.Bind<FirefliesConfig>()
                .FromInstance(firefliesConfig)
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesTo<FirefliesHealthIndicator>()
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