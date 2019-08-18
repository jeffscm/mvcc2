using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVCSamples.HelloWorld.Controller
{
    public class ARController : View3DController
    {

        public override void OnNavigationEnter()
        {
            base.OnNavigationEnter();
            app.Get3DView<View.ARPlacement>().parent3D.ClearTransform();
        }

        public override void OnNavigationExit()
        {
            base.OnNavigationExit();
        }

        public override bool OnNotification(object p_event_path, Object p_target, params object[] p_data)
        {
            base.OnNotification(p_event_path, p_target, p_data);

            var path = NotifyArg.ConvertTo<NOTIFYUI>(p_event_path);
            switch (path)
            {
                case NOTIFYUI.UI_CLICK_CHANGE_AR:
                    var sender = p_target as View.ProductItemSubView;

                    app.model.GetModel<Model.DownloadModel>().Download3DAsset(
                        sender.submodel.url,
                        (result) =>
                        {

                            if (result != null)
                            {
                                app.Get3DView<View.ARPlacement>().parent3D.ClearTransform();
                                Instantiate(result, app.Get3DView<View.ARPlacement>().parent3D);
                            }
                            else
                            {
                                app.Notify(NOTIFYSYSTEM.ERROR_MESSAGE, null, "Invalid 3D Asset");
                            }
                        }
                    );
                    break;
                case NOTIFYUI.UI_CLICK_CLEAR_ROOM:
                    app.Get3DView<View.ARPlacement>().parent3D.ClearTransform();
                    break;
            }
            return true;
        }
    }
}