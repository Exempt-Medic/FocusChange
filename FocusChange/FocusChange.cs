using Modding;
using System;
using HutongGames.PlayMaker.Actions;

namespace FocusChange
{
    public class FocusChangeMod : Mod
    {
        #region Boilerplate
        private static FocusChangeMod? _instance;
        internal static FocusChangeMod Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException($"An instance of {nameof(FocusChangeMod)} was never constructed");
                }
                return _instance;
            }
        }
        public override string GetVersion() => GetType().Assembly.GetName().Version.ToString();
        public FocusChangeMod() : base("FocusChange")
        {
            _instance = this;
        }
        #endregion

        #region Init
        public override void Initialize()
        {
            Log("Initializing");

            On.HutongGames.PlayMaker.Actions.IntCompare.OnEnter += FocusChanges;

            Log("Initialized");
        }
        #endregion

        #region Changes
        private void FocusChanges(On.HutongGames.PlayMaker.Actions.IntCompare.orig_OnEnter orig, IntCompare self)
        {
            if (self.Fsm.GameObject.name == "Knight" && self.Fsm.Name == "Spell Control" && self.State.Name.StartsWith("Full HP?"))
            {
                self.equal = null;
                self.greaterThan = null;
            }

            orig(self);
        }
        #endregion
    }
}
