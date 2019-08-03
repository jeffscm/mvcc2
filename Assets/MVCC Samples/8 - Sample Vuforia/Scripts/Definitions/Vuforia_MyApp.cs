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
                app.SwitchNavController<VuforiaSample.SelectionController>(CONTROLLER_TYPE.ALL);
            };
        }
    }
}