namespace DemeoPlus.ToggleSystem.Controllers
{
    public interface IToggleController
    {
        void Enable();
        void Disable();

        ToggleDescriptor GetDescriptor();
        bool ShouldEnable();
    }
}