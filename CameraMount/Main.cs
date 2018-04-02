using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Native;
using GTA.Math;
using System.Windows.Forms;
using VAutodrive;
using System.Reflection;

namespace GTAVScripts2
{

    public class Main : Script
    {
        // camera used on the vehicle
        Camera camera = null;
        Vehicle vehicle;
        private KeyHandling kh = new KeyHandling();
        bool flag = false;
        //bool flag2 = false;

        public Main() {
            UI.Notify("Loaded CameraMount.cs");

            // create a new camera 
            World.DestroyAllCameras();
            camera = World.CreateCamera(new Vector3(), new Vector3(), 50);
            camera.IsActive = true;
            GTA.Native.Function.Call(Hash.RENDER_SCRIPT_CAMS, false, true, camera.Handle, true, true);

            // attach time methods 
            Tick += OnTick;
            KeyUp += onKeyUp;
        }

        // Function used to take control of the world rendering camera.
        public void mountCameraOnVehicle()
        {
            if (Game.Player.Character.IsInVehicle())
            {
                GTA.Native.Function.Call(Hash.RENDER_SCRIPT_CAMS, true, true, camera.Handle, true, true);
            }
        }

        // Function used to keep camera on vehicle and facing forward on each tick step.
        public void keepCameraOnVehicle()
        {
            if (Game.Player.Character.IsInVehicle())
            {
                // keep the camera in the same position relative to the car
                camera.AttachTo(Game.Player.Character.CurrentVehicle, new Vector3(0f, 2f, 0.4f));

                // rotate the camera to face the same direction as the car 
                camera.Rotation = Game.Player.Character.CurrentVehicle.Rotation;
            }
        }


        
        private void onKeyUp(object sender, KeyEventArgs e)
        {

        }

        void OnTick(object sender, EventArgs e)
        {
            if (Game.Player.Character.IsInVehicle())
            {
                mountCameraOnVehicle();
                flag = true;
            }
           

            if(flag)
                keepCameraOnVehicle();
           
        }

    }
}
