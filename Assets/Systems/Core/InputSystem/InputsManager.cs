namespace Systems.Core.InputSystem
{
    public static class InputsManager
    {
        static Inputs inputs;

        public static Inputs Inputs => inputs;

        static InputsManager()
        {
            inputs = new Inputs();
            inputs.Player.Enable();
        }
    }
}
