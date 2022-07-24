namespace StarterAssets
{
    public interface IPlayerInputHandler
    {
        public void Register(IPlayerInputListener listener);
        public void Unregister(IPlayerInputListener listener);
    }
}