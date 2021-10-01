using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class ViewModel : ViewModelBase

    {
        TestTaskEntities testTaskDBContext;
        public ViewModel()
        {
            if (IsInDesignMode)
            {
                Valutes = new ObservableCollection<Valute>();
            }
            else
            {
                testTaskDBContext = new TestTaskEntities();

                testTaskDBContext.Valute.Load();
                Valutes = testTaskDBContext.Valute.Local;
            }          
        }
        public ObservableCollection<Valute> Valutes
        {
            get => GetValue<ObservableCollection<Valute>>();
            private set => SetValue(value);
        }
        
    }
}
