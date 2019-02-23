using Notes.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Notes.Pages.DataTemplates
{
    public class NoteTypeFileTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TextDataTemplate { get; set; }
        public DataTemplate LinkDataTemplate { get; set; }
        public DataTemplate DocumentDataTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var template = new DataTemplate();
            switch (((NoteFile)item).FileType)
            {
                case FilesTypes.Text:template = TextDataTemplate;break;
                case FilesTypes.Link: template = LinkDataTemplate; break;
                case FilesTypes.Document: template = DocumentDataTemplate; break;
            }
            return template;
        }
    }
}
