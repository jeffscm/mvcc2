using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    TRACKING_FOUND,
    TRACKING_LOST
};
