using BriefWerkstatt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BriefWerkstatt.ViewModels
{
    public class StandardLetterViewModel : ViewModelBase
    {
        private SenderModel? _sender;
        private RecipientModel? _recipient;
        private LetterContentModel? _letterContent;
        private FileInfoModel? _fileInfo;

        public string SenderName
        {
            get { return _sender.Name; }
            set
            {
                _sender.Name = value;
                OnPropertyChanged(nameof(SenderName));
            }
        }

        public string SenderStreetName
        {
            get { return _sender.Name; }
            set
            {
                _sender.Name = value;
                OnPropertyChanged(nameof(SenderName));
            }
        }

        /* TODO: Do not continue like this. Maybe make a ViewModel for each model and combine it into a StandardLetterViewModel
         *       which will be instantiated in here...
        */
    }
}
