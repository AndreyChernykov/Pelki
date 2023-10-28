using System;

namespace Pelki.Gameplay.SaveSystem
{
    //sttrox: подумать название что бы отличать явно что это сохранения
    public class LevelSessionData
    {
        private ISaverData _saverData;
        private Action<object> _savingDataDelegat;

        public string SavePointId { get; set; }

        public event Action<object> Saved;

        public void Save()
        {
            //sttox: вариант через событие
            Saved?.Invoke(this);
            //sttrox: вариант через прямой вызов сохранения
            _saverData.SaveObject(this);
            //sttrox: вариант через делегат
            _savingDataDelegat?.Invoke(this);
        }

        public void Initialize(ISaverData savesStorage)
        {
            _saverData = savesStorage;
        }

        public void Initialize(Action<object> savingDataDelegat)
        {
            _savingDataDelegat = savingDataDelegat;
        }
    }
}