using PlayerMovement;
using PlayerMovement.Data;
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