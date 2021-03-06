using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components.Settings;
using BeatSaberMarkupLanguage.ViewControllers;
using Zenject;

namespace MultiplayerExtensions.UI
{
    class HostLobbySetupPanel : BSMLResourceViewController
    {

        public override string ResourceName => "MultiplayerExtensions.UI.HostLobbySetupPanel.bsml";
        private IMultiplayerSessionManager sessionManager;

        [Inject]
        internal void Inject(IMultiplayerSessionManager sessionManager, HostLobbySetupViewController hostViewController)
        {
            this.sessionManager = sessionManager;
            base.DidActivate(true, false, true);
        }

        #region UIComponents
        [UIComponent("CustomSongsToggle")]
        public ToggleSetting customSongsToggle = null!;

        [UIComponent("EnforceModsToggle")]
        public ToggleSetting enforceModsToggle = null!;

        [UIComponent("VerticalHUDToggle")]
        public ToggleSetting verticalHUDToggle = null!;

        [UIComponent("DefaultHUDToggle")]
        public ToggleSetting defaultHUDToggle = null!;

        [UIComponent("HologramToggle")]
        public ToggleSetting hologramToggle = null!;
        #endregion

        #region UIValues
        [UIValue("CustomSongs")]
        public bool CustomSongs
        {
            get => Plugin.Config.CustomSongs;
            set { 
                Plugin.Config.CustomSongs = value;
                MPState.CustomSongsEnabled = value;
            }
        }

        [UIValue("EnforceMods")]
        public bool EnforceMods
        {
            get => Plugin.Config.EnforceMods;
            set { Plugin.Config.EnforceMods = value; }
        }

        [UIValue("VerticalHUD")]
        public bool VerticalHUD
        {
            get => Plugin.Config.VerticalHUD;
            set { Plugin.Config.VerticalHUD = value; }
        }

        [UIValue("DefaultHUD")]
        public bool DefaultHUD
        {
            get => Plugin.Config.SingleplayerHUD;
            set { Plugin.Config.SingleplayerHUD = value; }
        }

        [UIValue("Hologram")]
        public bool Hologram
        {
            get => Plugin.Config.Hologram;
            set { Plugin.Config.Hologram = value; }
        }
        #endregion

        #region UIActions
        [UIAction("SetCustomSongs")]
        public void SetCustomSongs(bool value)
        {
            CustomSongs = value;
            customSongsToggle.Value = value;

            EnforceMods = EnforceMods || value;
            enforceModsToggle.Value = EnforceMods || value;

            UpdateStates();
        }

        [UIAction("SetEnforceMods")]
        public void SetEnforceMods(bool value)
        {
            EnforceMods = value;
            enforceModsToggle.Value = value;

            CustomSongs = !(!CustomSongs || !value);
            customSongsToggle.Value = !(!CustomSongs || !value);

            UpdateStates();
        }

        [UIAction("SetVerticalHUD")]
        public void SetVerticalHUD(bool value)
        {
            VerticalHUD = value;
            verticalHUDToggle.Value = value;

            DefaultHUD = !(!DefaultHUD || !value);
            defaultHUDToggle.Value = !(!DefaultHUD || !value);
        }

        [UIAction("SetDefaultHUD")]
        public void SetDefaultHUD(bool value)
        {
            DefaultHUD = value;
            defaultHUDToggle.Value = value;

            VerticalHUD = VerticalHUD || value;
            verticalHUDToggle.Value = VerticalHUD || value;
        }

        [UIAction("SetHologram")]
        public void SetHologram(bool value)
        {
            Hologram = value;
            hologramToggle.Value = value;
        }
        #endregion

        private void UpdateStates()
        {
            sessionManager?.SetLocalPlayerState("customsongs", CustomSongs);
            sessionManager?.SetLocalPlayerState("enforcemods", EnforceMods);
        }
    }
}
