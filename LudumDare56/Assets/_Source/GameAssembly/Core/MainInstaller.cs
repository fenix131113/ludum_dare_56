using Core.Game;
using Core.Menu;
using Fireflies;
using Fireflies.Data;
using Knight;
using Player;
using Player.Data;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;
using UnityEngine.Rendering;

namespace Core
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private FirefliesConfig firefliesConfig;
        [SerializeField] private AudioSource oneShotAudioSource;
        
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

            Container.Bind<GamePauseMenu>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();

            Container.Bind<AudioSource>()
                .FromInstance(oneShotAudioSource)
                .AsSingle()
                .NonLazy();
        }

        private void BindFireflies()
        {
            Container.Bind<FirefliesContainer>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();

            Container.Bind<PlayerHealth>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<HealthLightChecker>()
                .AsSingle()
                .NonLazy();

            Container.Bind<FirefliesConfig>()
                .FromInstance(firefliesConfig)
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesTo<HealthIndicator>()
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