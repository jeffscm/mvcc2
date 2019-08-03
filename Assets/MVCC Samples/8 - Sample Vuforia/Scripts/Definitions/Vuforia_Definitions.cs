using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuforiaSample
{

    public enum NOTIFYEVENT
    {
        NONE,
        TEST_START,
        TEST_BUTTON
    };

    public enum NOTIFYSYSTEM
    {
        PLAYSOUND,
        ERROR_MESSAGE
    }

    public enum NOTIFYNAV
    {
        NONE,
        SHOW_TOPMENU,
        HIDE_TOPMENU,
        GOTO_LOGIN
    };

    public enum NOTIFYUI
    {
        NONE,
        UI_CLICK_LOGIN,
        UI_CLICK_CLOSE,
        UI_CLICK_CHANGE_AR,
        UI_CLICK_CLEAR_ROOM

    };

    public enum NOTIFYJS
    {
        NONE,
    };

    public enum NOTIFYVUFORIA
    {
        NONE,
        VUFORIA_START_PLACEMENT,
        VUFORIA_PLACE_ON_GROUND,

        VUFORIA_ADD_NEW_GROUND,
        VUFORIA_CLEAR_CURRENT_GROUND,
        VUFORIA_DELETE_CURRENT_GROUND,
        VUFORIA_CANCEL_PLACEMENT,
        VUFORIA_HIT_TEST,


        VUFORIA_UI_CLICK_BACK,
        VUFORIA_UI_CLICK_CANCEL,
        VUFORIA_UI_CLICK_DRAWER,
        VUFORIA_UI_CLICK_DRAWER_CLOSE,
    };
}