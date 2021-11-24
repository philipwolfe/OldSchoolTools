using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Adornments;
using Microsoft.VisualStudio.Text.Editor;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OldSchoolTools.QuickInfoHider
{
	internal class QuickInfoHiderIntellisenseController : IIntellisenseController
	{
		private ITextView m_textView;
		private IList<ITextBuffer> m_subjectBuffers;
		private QuickInfoHiderIntellisenseControllerProvider m_provider;
		private Task<QuickInfoItemsCollection> m_session;

		internal QuickInfoHiderIntellisenseController(ITextView textView, IList<ITextBuffer> subjectBuffers, QuickInfoHiderIntellisenseControllerProvider provider)
		{
			m_textView = textView;
			m_subjectBuffers = subjectBuffers;
			m_provider = provider;

			m_textView.MouseHover += this.OnTextViewMouseHover;
		}

		private void OnTextViewMouseHover(object sender, MouseHoverEventArgs e)
		{
			//find the mouse position by mapping down to the subject buffer
			SnapshotPoint? point = m_textView.BufferGraph.MapDownToFirstMatch(new SnapshotPoint(m_textView.TextSnapshot, e.Position), PointTrackingMode.Positive, snapshot => m_subjectBuffers.Contains(snapshot.TextBuffer), PositionAffinity.Predecessor);

			if (point != null)
			{
				ITrackingPoint triggerPoint = point.Value.Snapshot.CreateTrackingPoint(point.Value.Position, PointTrackingMode.Positive);

				if (!m_provider.QuickInfoBroker.IsQuickInfoActive(m_textView))
				{
					CancellationToken token = new CancellationToken();
					m_session = m_provider.QuickInfoBroker.GetQuickInfoItemsAsync(m_textView, triggerPoint, token);
					m_session.ContinueWith(t => cont(t));
				}
			}
		}

		private void cont(Task<QuickInfoItemsCollection> t)
		{
			try
			{
				if (t != null && !t.IsFaulted && t.Result != null)
				{
					QuickInfoItemsCollection result = t.Result;
					if (result != null)
					{
						var outerContainer = result.Items.Cast<ContainerElement>().ToList();
						var innerContainer = outerContainer[0].Elements.Cast<ContainerElement>().ToList();
						var elements = innerContainer[0].Elements.ToList();
						if (elements[1] is ClassifiedTextElement)
						{
							var text = (ClassifiedTextElement)elements[1];
							var runs = text.Runs.Cast<ClassifiedTextRun>().ToList();

							//check declarations first
							var first = runs.First().Text;
							foreach(var delclaration in QuickInfoHiderPackage.Options.Declarations)
							{
								if(first == delclaration)
								{
									m_provider.QuickInfoBroker.GetSession(m_textView)?.DismissAsync();  //sometimes the popup will flash because multiple IIntellisenseControllers are run asychronously 
									return;
								}
							}

							//types don't end with these chars
							var last = runs.Last().Text;
							var lastChar = last.Last();
							switch(lastChar)
							{
								case ')':
								case '}':
									return;
							}

							//must be a type
							var lastSpace = runs.Last(_ => _.Text == " ");
							var lastSpaceIndex = runs.LastIndexOf(lastSpace);

							var parts = runs.Skip(lastSpaceIndex + 1).Select(_ => _.Text);
							var name = string.Concat(parts);

							foreach (var typeName in QuickInfoHiderPackage.Options.Types)
							{
								if (name == typeName)
								{
									m_provider.QuickInfoBroker.GetSession(m_textView)?.DismissAsync();  //sometimes the popup will flash because multiple IIntellisenseControllers are run asychronously 
									return;
								}
							}
						}
					}
				}
			}
			catch
			{ }
		}

		public void Detach(ITextView textView)
		{
			if (m_textView == textView)
			{
				m_textView.MouseHover -= this.OnTextViewMouseHover;
				m_textView = null;
			}
		}

		public void ConnectSubjectBuffer(ITextBuffer subjectBuffer)
		{
		}

		public void DisconnectSubjectBuffer(ITextBuffer subjectBuffer)
		{
		}
	}
}
