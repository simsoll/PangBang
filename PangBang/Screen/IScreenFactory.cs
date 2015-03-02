namespace PangBang.Screen
{
    public interface IScreenFactory
    {
        IScreen CreateStartScreen();
        IScreen CreateGameScreen();
    }
}
