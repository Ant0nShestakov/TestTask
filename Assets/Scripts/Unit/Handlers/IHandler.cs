public abstract class Handler
{
    protected readonly InputManager inputManager;

    public abstract void Update();

    public Handler(InputManager inputManager)
    {
        this.inputManager = inputManager;
    }
}