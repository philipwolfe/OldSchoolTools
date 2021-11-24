using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace OldSchoolTools.QuickInfoHider
{
	[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
	[Guid(QuickInfoHiderPackage.PackageGuidString)]
	[ProvideOptionPage(typeof(Options), "Old School Tools", "Quick Info Hider", 0, 0, true, ProvidesLocalizedCategoryName = false, SupportsProfiles = true)]
	[ProvideAutoLoad(VSConstants.UICONTEXT.ShellInitialized_string, PackageAutoLoadFlags.BackgroundLoad)] //Autoloads the package so that the InitializeAsync is called
	public sealed class QuickInfoHiderPackage : AsyncPackage
	{
		/// <summary>
		/// OldSchoolTools.QuickInfoHiderPackage GUID string.
		/// </summary>
		public const string PackageGuidString = "42eab05e-187c-4f81-9ff9-1de032fd14da";

		public static Options Options { get; private set; }

		#region Package Members

		/// <summary>
		/// Initialization of the package; this method is called right after the package is sited, so this is the place
		/// where you can put all the initialization code that rely on services provided by VisualStudio.
		/// </summary>
		/// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
		/// <param name="progress">A provider for progress updates.</param>
		/// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
		protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
		{
			Options = (Options)GetDialogPage(typeof(Options));

			// When initialized asynchronously, the current thread may be a background thread at this point.
			// Do any initialization that requires the UI thread after switching to the UI thread.
			await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
		}

		#endregion
	}
}
