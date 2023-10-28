namespace Pelki.Gameplay.SaveSystem
{
    public interface ISaverData
    {
        void SaveObject<TObj>(TObj t);
    }
}