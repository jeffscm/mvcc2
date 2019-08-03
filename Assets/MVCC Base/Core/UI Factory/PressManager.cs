using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PressManager : AppElement {

    public static PressManager instance;

    bool _canClick = true;
    


    public bool CanClick
    {
        get
        {
            if (!_canClick)
                return false;

            _canClick = false;
            return true;
        }
    }

    void Awake ()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);

	}

   

    void ResetClick ()
    {
        _canClick = true;
    }

	public void ProcessNotify(object n, MonoBehaviour obj, List<string> extraData = null)
	{
        app.Notify(n, obj, (extraData == null) ? null : extraData.ToArray());
		PointerExit();
	}

    public void PointerExit ()
    {
        CancelInvoke("ResetClick");
        Invoke("ResetClick", 0.5f);
    }

	public bool CheckClick ()
	{
		return _canClick;
	}

    public void UnlockClick()
    {
        _canClick = true;
    }

    public void HoldClickBackground()
    {
        _canClick = false;
        CancelInvoke("ResetClick");
        Invoke("ResetClick", 0.25f);
    }
}
