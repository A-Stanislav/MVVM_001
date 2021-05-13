using MVVM_001.Command;
using MVVM_001.Model;
using MVVM_001.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_001.ViewModel
{
    public class MainWindow_ViewModel : ViewModelBase
    {
        #region Title
        private string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                //if (_Title == value)
                //{
                //    return;
                //}
                //_Title = value;
                //OnPropertyChanged(nameof(Title));
                Set(ref _Title, value, nameof(Title));
            }
        }
        #endregion

        #region Album
        private ObservableCollection<album> _Album;
        public ObservableCollection<album> Album
        {
            get { return _Album; }
            set
            {
                //if (_Album == value)
                //{
                //    return;
                //}
                //_Album = value;
                //OnPropertyChanged(nameof(Album));
                Set(ref _Album, value, nameof(Album));
            }
        }
        #endregion




        #region Factory
        private ObservableCollection<factory> _Factory;
        public ObservableCollection<factory> Factory
        {
            get { return _Factory; }
            set
            {
                Set(ref _Factory, value, nameof(Factory));
            }
        }
        #endregion




        #region выбранный альбом
        private album _curAlbum; //  выбранный альбом
        public album CurAlbum
        {
            get { return _curAlbum; }
            set { Set(ref _curAlbum, value, nameof(CurAlbum)); }
        }
        #endregion

        public MainWindow_ViewModel()
        {
            Title = "Started " + (DateTime.Now).ToString();

            using (NPConEntities context = new NPConEntities())
            {
                Album = new ObservableCollection<album>(from p in context.albums select p);//context.albums;
                Factory = new ObservableCollection<factory>(from p in context.factories select p);
            }
        }

        public DelegateCommand Click_Delete
        {
            get
            {
                return new DelegateCommand((obj) =>
               {
                   using (NPConEntities context = new NPConEntities())
                   {
                       
                       album del_alb = context.albums.FirstOrDefault(a=> a.id_album == CurAlbum.id_album);
                       context.albums.Remove(del_alb);
                       context.SaveChanges();
                       Album = new ObservableCollection<album>(from p in context.albums select p);
                   }
               });
            }
        }

    }
}
