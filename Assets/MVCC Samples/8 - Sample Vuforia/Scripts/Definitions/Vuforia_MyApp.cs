using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuforiaSample
{
    public class Vuforia_MyApp : AppMonoController
    {
        public override void OnEnable()
        {
            base.OnEnable();
            MVCC.RegisterAnim(new Vuforia_MyAnim());

            MVCC.OnStart += () =>
            {
                Invoke(nameof(DelayedStart), 2f);
            };
        }

        void DelayedStart()
        {
            app.SwitchNavController<VuforiaSample.SelectionController>(CONTROLLER_TYPE.ALL);
        }
    }
}