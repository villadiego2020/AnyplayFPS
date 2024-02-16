namespace AFPS.Cores
{
    public interface IInitalize
    {
        void Initialize(params object[] objects);
        void Register();
        void Unregister();
    }
}