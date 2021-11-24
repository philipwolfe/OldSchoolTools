using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace OldSchoolTools.QuickInfoHider
{
    [Export(typeof(IAsyncQuickInfoSourceProvider))]
    [Name("QuickInfo Hider Source Provider")]
    [ContentType("csharp")]
    internal class QuickInfoHiderSourceProvider : IAsyncQuickInfoSourceProvider
    {
        public IAsyncQuickInfoSource TryCreateQuickInfoSource(ITextBuffer textBuffer)
        {
			return textBuffer.Properties.GetOrCreateSingletonProperty(() => new QuickInfoHiderSource());
        }
    }
}
