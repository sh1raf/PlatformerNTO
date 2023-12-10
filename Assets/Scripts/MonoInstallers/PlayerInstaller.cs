using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Player player;
    public override void InstallBindings()
    {
        Container.Bind<Player>().FromInstance(player).AsSingle().NonLazy();
        Container.Bind<PlayerMovement>().FromInstance(player.gameObject.GetComponent<PlayerMovement>()).AsSingle().NonLazy();
        Container.Bind<PlayerPCInput>().FromInstance(player.gameObject.GetComponent<PlayerPCInput>()).AsSingle().NonLazy();
        Container.Bind<PlayerHealthLogic>().FromInstance(player.gameObject.GetComponent<PlayerHealthLogic>()).AsSingle().NonLazy();
    }
}
