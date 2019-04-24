using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamEssentials
{
	public partial class BatteryInfo : ContentPage
	{
		public BatteryInfo ()
		{
			InitializeComponent ();
        }

        private void ShowBatteryInfo(object sender, EventArgs e)
        {
            var level = Battery.ChargeLevel;
            var state = Battery.State;
            var source = Battery.PowerSource;

            SetBackground(Battery.ChargeLevel,
            Battery.State == BatteryState.Charging);

            LevelLbl.Text = "Level: " + level;
            StateLbl.Text = "State: " + state;
            SourceLbl.Text = "Source: " + source;
        }

        void SetBackground(double level, bool charging)
        {
            Color? color = null;
            var status = charging ? "Charging" : "Not charging";

            if (level > .5f)
            {
                color = Color.Green.MultiplyAlpha(level);
            }
            else if (level > .1f)
            {
                color = Color.Yellow.MultiplyAlpha(1d - level);
            }
            else
            {
                color = Color.Red.MultiplyAlpha(1d - level);
            }

            BackgroundColor = color.Value;
        }
    }

    public class BatteryInfoChangedEventArgs : EventArgs
    {
        public BatteryInfoChangedEventArgs(double level, BatteryState state, BatteryPowerSource source)
        {
            ChargeLevel = level;
            State = state;
            PowerSource = source;
        }

        public double ChargeLevel { get; }

        public BatteryState State { get; }

        public BatteryPowerSource PowerSource { get; }

        public override string ToString() =>
            $"{nameof(ChargeLevel)}: {ChargeLevel}, " +
            $"{nameof(State)}: {State}, " +
            $"{nameof(PowerSource)}: {PowerSource}";
    }
}