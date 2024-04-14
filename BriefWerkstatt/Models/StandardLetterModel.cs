using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BriefWerkstatt.Models
{
    public class StandardLetterModel
    {
        public SenderModel Sender { get; set; }
        public RecipientModel Recipient { get; set; }
        public LetterContentModel LetterContent { get; set; }
        public FileInfoModel FileInfo { get; set; }

        public StandardLetterModel() 
        {
            Sender = new SenderModel();
            Recipient = new RecipientModel();
            
            LetterContent = new LetterContentModel()
            { 
                Intro = "Sehr geehrte Damen und Herren,", 
                Outro = "Mit freundlichen Grüßen," 
            };

            FileInfo = new FileInfoModel();
        }
    }
}
