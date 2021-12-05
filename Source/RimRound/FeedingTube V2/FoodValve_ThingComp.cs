﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RimRound
{
    public class FoodValve_ThingComp : FoodTransmitter_ThingComp
    {
        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            flickComp = parent.GetComp<CompFlickable>();
        }

        public override bool TransmitsFoodNow => flickComp.SwitchIsOn;

		public override void ReceiveCompSignal(string signal)
		{
			base.ReceiveCompSignal(signal);
			if (signal == "FlickedOff")
			{
				FoodTransmitter_NetManager.For(this.parent.Map).Notify_ConnectorRemoved();
			}
			if (signal == "FlickedOn")
			{
				FoodTransmitter_NetManager.For(this.parent.Map).Notify_ConnectorAdded(this);
			}

			this.parent.Map.mapDrawer.MapMeshDirty(this.parent.Position, MapMeshFlag.PowerGrid, true, false);
		}


		CompFlickable flickComp;
    }
}