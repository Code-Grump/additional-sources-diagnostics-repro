using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace DiagnosticSourceGenerator
{
    [Generator(LanguageNames.CSharp)]
    public class TextSourceGenerator : IIncrementalGenerator
    {
        public static readonly DiagnosticDescriptor TextFileEmpty = new DiagnosticDescriptor(
            id: "DG100",
            title: "Text file empty",
            messageFormat: "The input has no content",
            "DiagnosticSourceGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var diagnostics = context.AdditionalTextsProvider.Select((text, cancellationToken) =>
            {
                return Diagnostic.Create(
                    TextFileEmpty,
                    Location.Create(text.Path, TextSpan.FromBounds(0, 0), new LinePositionSpan(LinePosition.Zero, LinePosition.Zero)));
            });

            context.RegisterSourceOutput(diagnostics, (sourceProduction, diagnostic) => sourceProduction.ReportDiagnostic(diagnostic));
        }
    }
}
