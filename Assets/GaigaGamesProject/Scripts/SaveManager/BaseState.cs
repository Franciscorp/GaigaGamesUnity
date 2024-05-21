using UnityEngine;

public class BaseState : MonoBehaviour
{
    public bool saveOnQuit = false;

    // a virtual method is a method that can be overridden in a derived class
    // return json info about this state 
    public virtual string SaveState()
    {
        return null;
    }


    // read json and load info into state
    public virtual void LoadState(string json) {   }


    // each save file while have its unique UID that holds the information about the state of the game
    public virtual string GetUID()
    {
        //return (gameObject.scene.name + "_" + gameObject.name + "_" + (this.GetType()));
        return (gameObject.name + "_" + (this.GetType()));
    }


    // each state will have a directory in which all related files will be saved 
    public virtual string GetSaveDir()
    {
        return null;
    }


    // check if any changes happened to the state: should it be saved/loaded again
    public virtual bool ShouldSave()
    {
        return true;
    }


    public virtual bool ShouldLoad()
    {
        return true;
    }
}
