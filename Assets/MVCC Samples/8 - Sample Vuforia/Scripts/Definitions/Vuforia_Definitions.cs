/*
Jefferson Scomacao
https://www.jscomacao.com

GitHub - Source Code
Project: MVCC2.0

Copyright (c) 2015 Jefferson Raulino Scomação

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
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
        NONE,
        SHOW_TOAST
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
        VUFORIA_UI_SHOW_FIRST_OPTIONS,
        VUFORIA_UI_SHOW_COLORS,
        VUFORIA_UI_CLICK_RESET,
        VUFORIA_UI_CLICK_SELECT_COLOR,
        VUFORIA_UI_OPTIONS_BACK_MAIN,

        VUFORIA_UI_CLICK_PHOTO,
        VUFORIA_UI_CLICK_START_REC,
        VUFORIA_UI_CLICK_STOP_REC,
    };

   
}