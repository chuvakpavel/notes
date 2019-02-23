using Notes.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Notes.Pages.DataTemplates
{
    public class NoteTypeFileTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TextDataTemplate { get; set; }
        public DataTemplate LinkDataTemplate { get; set; }
        public DataTemplate DocumentDataTemplate { get; set; }
        public DataTemplate PhotoDataTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var template = new DataTemplate();
            var noteItem = (NoteFile)item;
            var extension = Path.GetExtension(noteItem.FilePath);
            switch (noteItem.FileType)
            {
                case FilesTypes.Text: template = TextDataTemplate; break;
                case FilesTypes.Link: template = LinkDataTemplate; break;
                case FilesTypes.Document:
                    if (Path.GetExtension(noteItem.FilePath) == ".jpg" || Path.GetExtension(noteItem.FilePath) == ".jpeg" || Path.GetExtension(noteItem.FilePath) == ".png")
                    {
                        template = PhotoDataTemplate;
                    }
                    else
                    {
                        template = DocumentDataTemplate;
                    }
                    break;
                case FilesTypes.Photo: template = PhotoDataTemplate; break;
            }
            return template;
        }
    }
}
