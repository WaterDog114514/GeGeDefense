using UnityEngine;

public class TestSceneDemo : MonoBehaviour
{
    public static void enterPhase()
    {
        LifeCycleManager.Instance.SwtichState(new MainMenu_LifeCycle());
    }
    public static void enterPhase22()
    {
        LifeCycleManager.Instance.SwtichState(new Game_LifeCycle());
    }
    public static void enterPhase33()
    {
        LifeCycleManager.Instance.SwtichState(new Shutdown_LifeCycleState());
    }
}
