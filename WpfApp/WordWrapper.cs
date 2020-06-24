using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    public class WordWrapper : IDisposable
    {
        Application _word = null;

        public WordWrapper()
        {
            _word = new Application() { Visible = false };
        }

        public void CreateDocument()
        {
            var doc = _word.Documents.Add();
            doc.Activate();
        }

        public void AppendTest(string text)
        {
            var location = _word.ActiveDocument.Range(_word.ActiveDocument.Content.End - 1);
            location.Bold = 1;
            location.InsertAfter(text);
        }



        public void SaveAs(string location)
        {
            var document = _word.ActiveDocument;
            document.SaveAs(location);
            document.Close();
        }

        private bool isDisposed = false;

        ~WordWrapper()
        {
            Dispose(false);
        }

        public void Dispose(bool disposing)
        {
            if(!isDisposed)
            {
                if(disposing)
                {
                    if(_word != null)
                        _word.Quit();
                }
            }

            if (_word != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(_word);

            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
    }
}
