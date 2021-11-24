using Microsoft.VisualStudio.Language.Intellisense;
using System.Threading;
using System.Threading.Tasks;

namespace OldSchoolTools.QuickInfoHider
{
    internal class QuickInfoHiderSource : IAsyncQuickInfoSource
    {
        public Task<QuickInfoItem> GetQuickInfoItemAsync(IAsyncQuickInfoSession session, CancellationToken token)
        {
            if(QuickInfoHiderPackage.Options.DisableAllQuickInfo)
            {
                session.DismissAsync();  //this should dismiss without any flash 
            }
			return Task.FromResult<QuickInfoItem>(null);
        }

        public void Dispose()
        {
        }
    }
}
