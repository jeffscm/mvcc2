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

namespace MVCSamples.HelloWorld
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
        TRACKING_FOUND,
        TRACKING_LOST
    };
}