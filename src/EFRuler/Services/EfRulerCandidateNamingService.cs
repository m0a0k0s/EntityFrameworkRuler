using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using EdmxRuler.Extensions;
using EdmxRuler.Rules.NavigationNaming;
using EdmxRuler.Rules.PrimitiveNaming;
using EntityFrameworkRuler.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable ClassCanBeSealed.Global

namespace EntityFrameworkRuler.Services;

/// <summary> Naming service override to be used by Ef scaffold process.
/// This will apply custom table, column, and navigation names. </summary>
[SuppressMessage("Usage", "EF1001:Internal EF Core API usage.")]
public class EfRulerCandidateNamingService : CandidateNamingService {
    private readonly IRuleLoader ruleLoader;
    private readonly IOperationReporter reporter;
    private PrimitiveNamingRules primitiveNamingRules;
    private NavigationNamingRules navigationRules;

    /// <inheritdoc />
    public EfRulerCandidateNamingService(IRuleLoader ruleLoader, IOperationReporter reporter) {
        this.ruleLoader = ruleLoader;
        this.reporter = reporter;
    }

    /// <summary> Name that table </summary>
    public override string GenerateCandidateIdentifier(DatabaseTable table) {
        if (table == null) throw new ArgumentException("Argument is empty", nameof(table));
        primitiveNamingRules ??= ruleLoader?.GetPrimitiveNamingRules() ?? new PrimitiveNamingRules();


        if (!primitiveNamingRules.TryResolveRuleFor(table.Schema, table.Name, out var schema, out var tableRule))
            return base.GenerateCandidateIdentifier(table);

        if (tableRule?.NewName.HasNonWhiteSpace() == true) {
            WriteVerbose($"Table rule applied: {tableRule.Name} to {tableRule.NewName}");
            return tableRule.NewName;
        }

        var candidateStringBuilder = new StringBuilder();
        if (schema.UseSchemaName) candidateStringBuilder.Append(GenerateIdentifier(table.Schema));

        string newTableName;
        if (!string.IsNullOrEmpty(schema.TableRegexPattern) && schema.TablePatternReplaceWith != null) {
            if (primitiveNamingRules.PreserveCasingUsingRegex)
                newTableName = RegexNameReplace(schema.TableRegexPattern, table.Name,
                    schema.TablePatternReplaceWith);
            else
                newTableName = GenerateIdentifier(RegexNameReplace(schema.TableRegexPattern, table.Name,
                    schema.TablePatternReplaceWith));
        } else
            newTableName = base.GenerateCandidateIdentifier(table);

        if (string.IsNullOrWhiteSpace(newTableName)) {
            candidateStringBuilder.Append(GenerateIdentifier(table.Name));

            return candidateStringBuilder.ToString();
        }

        candidateStringBuilder.Append(newTableName);

        return candidateStringBuilder.ToString();
    }

    /// <summary> Name that column </summary>
    [SuppressMessage("ReSharper", "InvertIf")]
    public override string GenerateCandidateIdentifier(DatabaseColumn column) {
        if (column is null) throw new ArgumentNullException(nameof(column));
        primitiveNamingRules ??= ruleLoader?.GetPrimitiveNamingRules() ?? new PrimitiveNamingRules();

        if (!primitiveNamingRules.TryResolveRuleFor(column?.Table?.Schema, column?.Table?.Name, out var schema, out var tableRule))
            return base.GenerateCandidateIdentifier(column);
        if (!tableRule.TryResolveRuleFor(column?.Name, out var columnRule))
            return base.GenerateCandidateIdentifier(column);

        if (columnRule?.NewName.HasNonWhiteSpace() == true) {
            WriteVerbose($"Column rule applied: {columnRule.Name} to {columnRule.NewName}");
            return columnRule.NewName;
        }

        if (!string.IsNullOrEmpty(schema.ColumnRegexPattern) && schema.ColumnPatternReplaceWith != null) {
            var candidateStringBuilder = new StringBuilder();
            string newColumnName;
            if (primitiveNamingRules.PreserveCasingUsingRegex)
                newColumnName = RegexNameReplace(schema.ColumnRegexPattern, column.Name,
                    schema.ColumnPatternReplaceWith);
            else
                newColumnName = GenerateIdentifier(RegexNameReplace(schema.ColumnRegexPattern, column.Name,
                    schema.ColumnPatternReplaceWith));

            if (!string.IsNullOrWhiteSpace(newColumnName)) {
                candidateStringBuilder.Append(newColumnName);
                return candidateStringBuilder.ToString();
            }
        }

        return base.GenerateCandidateIdentifier(column);
    }

    /// <summary> Name that navigation dependent </summary>
    public override string GetDependentEndCandidateNavigationPropertyName(IReadOnlyForeignKey foreignKey) {
        if (foreignKey is null) throw new ArgumentNullException(nameof(foreignKey));
        string DefaultEfName() => base.GetDependentEndCandidateNavigationPropertyName(foreignKey);
        return GetCandidateNavigationPropertyName(foreignKey, false, DefaultEfName);
    }

    /// <summary> Name that navigation principal </summary>
    public override string GetPrincipalEndCandidateNavigationPropertyName(IReadOnlyForeignKey foreignKey,
        string dependentEndNavigationPropertyName) {
        if (foreignKey is null) throw new ArgumentNullException(nameof(foreignKey));
        string DefaultEfName() => base.GetPrincipalEndCandidateNavigationPropertyName(foreignKey, dependentEndNavigationPropertyName);
        return GetCandidateNavigationPropertyName(foreignKey, true, DefaultEfName);
    }


    #region internal members

    internal void WriteWarning(string msg) {
        reporter?.WriteWarning(msg);
        DebugLog(msg);
    }

    internal void WriteVerbose(string msg) {
        reporter?.WriteVerbose(msg);
        DebugLog(msg);
    }

    [Conditional("DEBUG")]
    internal static void DebugLog(string msg) => Console.WriteLine(msg);

    /// <summary> Name that navigation dependent </summary>
    protected virtual string GetCandidateNavigationPropertyName(IReadOnlyForeignKey foreignKey, bool thisIsPrincipal,
        Func<string> defaultEfName) {
        if (foreignKey is null) throw new ArgumentNullException(nameof(foreignKey));
        if (defaultEfName == null) throw new ArgumentNullException(nameof(defaultEfName));
        navigationRules ??= ruleLoader?.GetNavigationNamingRules() ?? new NavigationNamingRules();

        var fkName = foreignKey.GetConstraintName();
        var entity = thisIsPrincipal ? foreignKey.PrincipalEntityType : foreignKey.DeclaringEntityType;
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        // ReSharper disable once HeuristicUnreachableCode
        if (entity == null) return defaultEfName();

        string tableName;
        try {
            tableName = entity.GetTableName();
        } catch {
            tableName = null;
        }

        string schemaName;
        try {
            schemaName = entity.GetSchema();
        } catch {
            schemaName = null;
        }

        var classRef = navigationRules.TryResolveClassRuleFor(entity.Name, schemaName, tableName);
        if (classRef?.Properties.IsNullOrEmpty() != false) return defaultEfName();

        var rename = classRef.TryResolveNavigationRuleFor(fkName, defaultEfName, thisIsPrincipal, foreignKey.IsManyToMany());
        if (rename?.NewName.IsNullOrWhiteSpace() != false) return defaultEfName();

        WriteVerbose($"Navigation rule applied: {entity.Name}.{defaultEfName} to {rename.NewName}");
        return rename.NewName.Trim();
    }

    /// <summary>
    /// Convert DB element name to entity identifier. This is the EF Core standard.
    /// Borrowed from Microsoft.EntityFrameworkCore.Scaffolding.Internal.CandidateNamingService.GenerateCandidateIdentifier()
    /// </summary>
    protected virtual string GenerateIdentifier(string value) {
#if NET6
        return value.GenerateCandidateIdentifier(); // use the EF Ruler emulation of the GenerateCandidateIdentifier
#elif NET7
        return base.GenerateCandidateIdentifier(value); // use the actual EF process
#endif
    }

    /// <summary> Apply regex replace rule to name. </summary>
    protected virtual string RegexNameReplace(string pattern, string originalName, string replacement,
        int timeout = 100) {
        var newName = string.Empty;

        try {
            newName = Regex.Replace(originalName, pattern, replacement, RegexOptions.None,
                TimeSpan.FromMilliseconds(timeout));
        } catch (RegexMatchTimeoutException) {
            Console.WriteLine(
                $"Regex pattern {pattern} time out when trying to match {originalName}, name won't be replaced");
        }

        return newName;
    }

    #endregion
}