using UnityEngine;
using System.Collections;
//-----------------------------------------------------------
//Enum defining all possible game events
//More events should be added to the list
public enum EVENT_TYPE {
    ON_GRABBED_BRANCH,
    ON_CLIMBED_BRANCH
};
//-----------------------------------------------------------
//Listener interface to be implemented on Listener classes
public interface IListener
{
    //Notification function invoked when events happen
    void OnEvent(EVENT_TYPE Event_Type, Object Param = null);
}
//-----------------------------------------------------------