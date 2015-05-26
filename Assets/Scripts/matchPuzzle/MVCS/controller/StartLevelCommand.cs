using UnityEngine;
using matchPuzzle.MVCS.model;
using matchPuzzle.MVCS.model.level;
using matchPuzzle.MVCS.model.level.chain;
using matchPuzzle.MVCS.model.level.provider;
using matchPuzzle.utils;
using strange.extensions.command.impl;

namespace matchPuzzle.MVCS.controller
{
    public class StartLevelCommand : Command
    {
        [Inject(EntryPoint.Container.World)]
        public GameObject world {
            get;
            set;
        }

        [Inject]
        public int levelIndex {
            get;
            set;
        }

        [Inject]
        public ILevelListModel levelList {
            get;
            set;
        }

        public override void Execute()
        {
            var provider = levelList.LevelsAvailable[levelIndex];
            injectionBinder.Bind<ILevelProvider>().To(provider);

            var level = (GameObject) Resources.Load("level/level");
            InstantiateUtil.InstantiateAt(level, world);

            var chain = injectionBinder.GetInstance<IChainModel>() as ChainModel;
            injectionBinder.Bind<ILevelModel>().To(chain.level).ToName(GameState.Current);
            injectionBinder.Bind<IChainModel>().To(chain).ToName(GameState.Current);
        }
    }
}