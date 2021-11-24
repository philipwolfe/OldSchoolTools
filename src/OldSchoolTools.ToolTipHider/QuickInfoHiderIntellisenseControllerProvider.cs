using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace OldSchoolTools.QuickInfoHider
{
    [Export(typeof(IIntellisenseControllerProvider))]
    [Name("QuickInfo Hider Controller Provider")]
    [ContentType("csharp")]
    internal class QuickInfoHiderIntellisenseControllerProvider : IIntellisenseControllerProvider
    {
        [Import]
        internal IAsyncQuickInfoBroker QuickInfoBroker { get; set; }

        public IIntellisenseController TryCreateIntellisenseController(ITextView textView, IList<ITextBuffer> subjectBuffers)
        {
            return textView.Properties.GetOrCreateSingletonProperty(() => new QuickInfoHiderIntellisenseController(textView, subjectBuffers, this));
        }
    }
}
