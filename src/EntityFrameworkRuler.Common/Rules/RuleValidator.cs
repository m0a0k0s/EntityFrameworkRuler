﻿using System.Text.RegularExpressions;
using EntityFrameworkRuler.Common;

// ReSharper disable MemberCanBeInternal

namespace EntityFrameworkRuler.Rules;

/// <summary> This is an internal API and is subject to change or removal without notice. </summary>
public class RuleValidator : IRuleValidator {
    private const string invalidSymbolName = "Invalid symbol name";
    private const string tooLong = "Too long";

    /// <summary> This is an internal API and is subject to change or removal without notice. </summary>
    public RuleValidator() { }

    private IValidator dbContextRule;
    private IValidator schemaRule;
    private IValidator tableRule;
    private IValidator columnRule;
    private IValidator navigationRule;

    /// <summary> This is an internal API and is subject to change or removal without notice. </summary>
    public IEnumerable<EvaluationFailure> Validate(IRuleItem rule, bool withChildren = true) {
        IValidator validator;
        switch (rule) {
            case DbContextRule:
                validator = dbContextRule ??= InitializeDbContextValidator();
                break;
            case SchemaRule:
                validator = schemaRule ??= InitializeSchemaRuleValidator();
                break;
            case TableRule:
                validator = tableRule ??= InitializeTableRuleValidator();
                break;
            case ColumnRule:
                validator = columnRule ??= InitializeColumnRuleValidator();
                break;
            case NavigationRule:
                validator = navigationRule ??= InitializeNavigationRuleValidator();
                break;
            default: yield break;
        }

        foreach (var failure in validator.Evaluate(rule)) yield return failure;

        if (!withChildren) yield break;
        foreach (var child in rule.GetChildren()) {
            foreach (var childFailure in Validate(child, true)) {
                yield return childFailure;
            }
        }
    }


    /// <summary> This is an internal API and is subject to change or removal without notice. </summary>
    protected virtual Validator<DbContextRule> InitializeDbContextValidator() {
        return new Validator<DbContextRule>()
                .For(o => o.Name)
                .Assert(s => s.IsValidSymbolName(), invalidSymbolName)
                .Assert(s => s.Length < 200, tooLong)
            ;
    }

    /// <summary> This is an internal API and is subject to change or removal without notice. </summary>
    protected virtual Validator<SchemaRule> InitializeSchemaRuleValidator() {
        return new Validator<SchemaRule>()
                .For(o => o.Name)
                .Assert(s => s.IsValidDbIdentifier(), invalidSymbolName)
                .Assert(s => s.Length < 200, tooLong)
                .For(o => o.Namespace).Assert(o => o.IsNullOrWhiteSpace() || o.Split('.').All(p => p.IsValidSymbolName()), "Invalid namespace")
                .For(o => o.ColumnRegexPattern).Assert(o => VerifyRegEx(o))
                .For(o => o.TableRegexPattern).Assert(o => VerifyRegEx(o))
            ;
    }

    /// <summary> This is an internal API and is subject to change or removal without notice. </summary>
    protected virtual Validator<TableRule> InitializeTableRuleValidator() {
        return new Validator<TableRule>()
                .For(o => o.Name)
                .Assert(s => s.IsValidDbIdentifier(), invalidSymbolName)
                .Assert(s => s.Length < 200, tooLong)
                .For(o => o.NewName).Assert(o => o.IsNullOrWhiteSpace() || (o.IsValidSymbolName() && o.Length < 300), invalidSymbolName)
                .For(o => o.EntityName).Assert(o => o.IsNullOrWhiteSpace() || (o.IsValidSymbolName() && o.Length < 300), invalidSymbolName)
            ;
    }

    /// <summary> This is an internal API and is subject to change or removal without notice. </summary>
    protected virtual Validator<ColumnRule> InitializeColumnRuleValidator() {
        return new Validator<ColumnRule>()
                .For(o => o.Name)
                .Assert(s => s.IsValidDbIdentifier(), invalidSymbolName)
                .Assert(s => s.Length < 200, tooLong)
                .For(o => o.NewName).Assert(o => o.IsNullOrWhiteSpace() || (o.IsValidSymbolName() && o.Length < 300), invalidSymbolName)
                .For(o => o.PropertyName)
                .Assert(o => o.IsNullOrWhiteSpace() || (o.IsValidSymbolName() && o.Length < 300), invalidSymbolName)
                .For(o => o.NewType).Assert(o => o.IsNullOrWhiteSpace() || o.Split('.').All(p => p.IsValidSymbolName()), "Invalid type name")
            ;
    }

    /// <summary> This is an internal API and is subject to change or removal without notice. </summary>
    protected virtual Validator<NavigationRule> InitializeNavigationRuleValidator() {
        return new Validator<NavigationRule>()
                .For(o => o.Name)
                .Assert(s => s.All(p => p.IsValidSymbolName() && p.Length < 300), invalidSymbolName)
                .For(o => o.NewName).Assert(o => o.IsNullOrWhiteSpace() || (o.IsValidSymbolName() && o.Length < 300), invalidSymbolName)
                .For(o => o.ToEntity).Assert(o => o.IsNullOrWhiteSpace() || (o.IsValidSymbolName() && o.Length < 300), invalidSymbolName)
                .For(o => o.FkName).Assert(o => o.IsNullOrWhiteSpace() || (o.IsValidSymbolName() && o.Length < 300), invalidSymbolName)
                .For(o => o.Multiplicity).Assert(o => o.IsNullOrEmpty() || o.ParseMultiplicity() != Multiplicity.Unknown)
            ;
    }

    /// <summary> This is an internal API and is subject to change or removal without notice. </summary>
    protected static bool VerifyRegEx(string testPattern, bool allowBlank = true) {
        var isValid = true;
        if (testPattern.IsNullOrWhiteSpace()) return allowBlank;
        try {
            _ = Regex.Match("", testPattern);
        } catch (ArgumentException) {
            // BAD PATTERN: Syntax error
            isValid = false;
        }

        return isValid;
    }
}

/// <summary> This is an internal API and is subject to change or removal without notice. </summary>
public interface IRuleValidator {
    /// <summary> This is an internal API and is subject to change or removal without notice. </summary>
    IEnumerable<EvaluationFailure> Validate(IRuleItem rule, bool withChildren = true);
}