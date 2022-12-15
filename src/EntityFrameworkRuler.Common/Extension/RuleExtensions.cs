﻿using System.Diagnostics.CodeAnalysis;
using EntityFrameworkRuler.Common.Annotations;
using EntityFrameworkRuler.Rules;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeInternal

namespace EntityFrameworkRuler.Extension;

/// <summary> This is an internal API and is subject to change or removal without notice. </summary>
public static class RuleExtensions {
    /// <summary> Get the primitive schema rule for the given target schema. Used during scaffolding phase. </summary>
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public static bool TryResolveRuleFor(this DbContextRule rules, string schema, out SchemaRule schemaRule) =>
        TryResolveRuleFor(rules?.Schemas, schema, out schemaRule);

    /// <summary> Get the primitive schema rule for the given target schema. Used during scaffolding phase. </summary>
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public static SchemaRule TryResolveRuleFor(this DbContextRule rules, string schema) =>
        TryResolveRuleFor(rules?.Schemas, schema, out var schemaRule) ? schemaRule : null;

    /// <summary> Get the primitive schema rule for the given target schema. Used during scaffolding phase. </summary>
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public static bool TryResolveRuleFor(this IList<SchemaRule> rules, string schema, out SchemaRule schemaRule) {
        schemaRule = null;
        if (rules.IsNullOrEmpty()) return false;

        if (schema.IsNullOrWhiteSpace()) {
            // default to dbo, otherwise fail
            if (rules.Count == 1 && (rules[0].SchemaName.IsNullOrWhiteSpace() || rules[0].SchemaName == "dbo")) schemaRule = rules[0];
            else return false;
        } else {
            schemaRule = rules.FirstOrDefault(x => x.SchemaName == schema);
            // if there is only one schema, and the name is not defined, default to that
            if (schemaRule == null && rules.Count == 1 && rules[0].SchemaName.IsNullOrWhiteSpace())
                schemaRule = rules[0];
        }

        return schemaRule != null;
    }

    /// <summary> Get the primitive table rule for the given target table. Used during scaffolding phase. </summary>
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public static EntityRule TryResolveRuleFor(this SchemaRule rules, string table)
        => TryResolveRuleFor(rules, table, out var entityRule) ? entityRule : null;

    /// <summary> Get the primitive table rule for the given target table. Used during scaffolding phase. </summary>
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public static bool TryResolveRuleFor(this SchemaRule rules, string table, out EntityRule entityRule) {
        entityRule = null;
        if (rules?.Entities == null || rules.Entities.Count == 0 || table.IsNullOrWhiteSpace()) return false;

        entityRule =
            rules.Entities?.FirstOrDefault(o => o.Name == table) ??
            rules.Entities?.FirstOrDefault(o => o.Name.IsNullOrWhiteSpace() && o.EntityName == table);

        return entityRule != null;
    }

    /// <summary> Get the primitive column rule for the given target column. Used during scaffolding phase. </summary>
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public static PropertyRule TryResolveRuleFor(this EntityRule rules, string column)
        => TryResolveRuleFor(rules, column, out var propertyRule) ? propertyRule : null;

    /// <summary> Get the primitive column rule for the given target column. Used during scaffolding phase. </summary>
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public static bool TryResolveRuleFor(this EntityRule rules, string column, out PropertyRule propertyRule) {
        propertyRule = null;
        if (rules?.Properties == null || rules.Properties.Count == 0 || column.IsNullOrWhiteSpace()) return false;

        propertyRule =
            rules.Properties.FirstOrDefault(o => o.Name == column) ??
            rules.Properties.FirstOrDefault(o => o.Name.IsNullOrWhiteSpace() && o.PropertyName == column);

        return propertyRule != null;
    }

    /// <summary> Get the primitive schema and table rules for the given target table. Used during scaffolding phase. </summary>
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public static bool TryResolveRuleFor(this DbContextRule rules,
        string schema,
        string table,
        out SchemaRule schemaRule,
        out EntityRule entityRule) {
        entityRule = null;
        if (!rules.TryResolveRuleFor(schema, out schemaRule)) return false;
        if (!schemaRule.TryResolveRuleFor(table, out entityRule)) return false;
        return entityRule != null;
    }

    /// <summary> Get the primitive schema, table and column rules for the given target column. Used during scaffolding phase. </summary>
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public static bool TryResolveRuleFor(this DbContextRule rules,
        string schema,
        string table, string column,
        out SchemaRule schemaRule,
        out EntityRule entityRule,
        out PropertyRule propertyRule) {
        entityRule = null;
        propertyRule = null;
        if (!rules.TryResolveRuleFor(schema, out schemaRule)) return false;
        if (!schemaRule.TryResolveRuleFor(table, out entityRule)) return false;
        if (!entityRule.TryResolveRuleFor(column, out propertyRule)) return false;
        return propertyRule != null;
    }

    /// <summary> Return the navigation naming rules EntityRule object for the given entity </summary>
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public static EntityRule TryResolveRuleForEntity(this DbContextRule rules,
        string entity,
        string schema,
        string table) {
        if (rules?.Schemas == null || rules.Schemas.Count == 0 || entity.IsNullOrWhiteSpace()) return null;

        var schemas = rules.Schemas.Where(o => o.Entities?.Count > 0);
        // use optional schema filter
        if (schema.HasNonWhiteSpace())
            schemas = schemas.Where(o => o.SchemaName.IsNullOrEmpty() || o.SchemaName.EqualsIgnoreCase(schema));

        var tables = schemas.SelectMany(o => o.Entities);

        // use optional table filter
        // ReSharper disable once InvertIf
        if (table.HasNonWhiteSpace()) {
            tables = tables.Where(o => o.Name.IsNullOrEmpty() || o.Name.EqualsIgnoreCase(table));

            // this should be enough IF the rule had the DBName defined.
            // query now because this is more reliable than using the expected entity name.
            var entityRule = tables.FirstOrDefault(o => o.Name.HasNonWhiteSpace());
            if (entityRule != null) return entityRule;
        }

        return tables.FirstOrDefault(o => o.EntityName == entity);
    }

    /// <summary> Return the navigation naming rule for the given navigation info </summary>
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public static NavigationRule TryResolveNavigationRuleFor(this EntityRule entityRule,
        string fkName,
        Func<string> defaultEfName,
        bool thisIsPrincipal,
        bool isManyToMany) {
        if (entityRule?.Navigations == null || entityRule.Navigations.Count == 0) return null;

        // locate by fkName first, which is most reliable.
        var navigationRenames = fkName.HasNonWhiteSpace()
            ? entityRule.Navigations
                .Where(t => t.FkName == fkName)
                .ToArray()
            : Array.Empty<NavigationRule>();

        if (navigationRenames.Length == 0) {
            // Maybe FkName is not defined?  if not, try to locate by expected target name instead
            if (fkName.HasNonWhiteSpace()) {
                var someFkNamesEmpty = entityRule.Navigations.Any(o => o.FkName.IsNullOrWhiteSpace());
                if (!someFkNamesEmpty) {
                    // Fk names ARE defined, this property is just not found. Use default.
                    return null;
                }
            }

            var efName = defaultEfName();
            navigationRenames = entityRule.Navigations.Where(o => o.Name.HasNonWhiteSpace() && o.Name.EqualsIgnoreCase(efName))
                .ToArray();
            if (navigationRenames.Length == 0) return null; // expected EF name resolution failed to
        }

        // we have candidate matches (by fk name or expected target name). we may need to narrow further.
        // ReSharper disable once InvertIf
        if (navigationRenames.Length > 1) {
            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (isManyToMany)
                // many-to-many relationships always set IsPrincipal=true for both ends in the rule file.
                navigationRenames = navigationRenames.Where(o => o.IsPrincipal).ToArray();
            else
                // filter for this end only
                navigationRenames = navigationRenames.Where(o => o.IsPrincipal == thisIsPrincipal).ToArray();
        }

        return navigationRenames.Length != 1 ? null : navigationRenames[0];
    }

    /// <summary> Get the rule elements within this element </summary>
    public static IEnumerable<IRuleItem> GetChildren(this IRuleItem item) {
        return item switch {
            IRuleModelRoot r => r.GetSchemas(),
            ISchemaRule sr => sr.GetClasses(),
            IEntityRule cr => cr.GetProperties(),
            _ => Enumerable.Empty<IRuleItem>()
        };
    }

    /// <summary> True if the rule element can contain child elements </summary>
    public static bool CanHaveChildren(this IRuleItem item) {
        return item switch {
            IRuleModelRoot => true,
            ISchemaRule => true,
            IEntityRule => true,
            _ => false
        };
    }

    /// <summary> This is an internal API and is subject to change or removal without notice. </summary>
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public static bool CanIncludeTable(this DbContextRule dbContextRule,
        SchemaRule schemaRule, EntityRule entityRule, bool isView, out bool schemaIncluded) {
        schemaIncluded = true;
#if DEBUG
        return true;
#endif
        if (schemaRule == null) {
            // schema is unknown to rule file
            if (dbContextRule == null || dbContextRule.IncludeUnknownSchemas) {
                schemaIncluded = true;
                return true; // nothing to go on. include it.
            }

            schemaIncluded = false;
            return false; // alien schema. do not generate unknown
        }


        schemaIncluded = schemaRule.Mapped;
        if (!schemaIncluded) return false;
        if (entityRule == null) {
            // table is unknown to rule file
            return isView ? schemaRule.IncludeUnknownViews : schemaRule.IncludeUnknownTables;
        }

        if (entityRule.NotMapped) return false;

        // drop the table if all columns are not mapped
        if (entityRule.IncludeUnknownColumns) return true;
        return entityRule.Properties.Count > 0 && entityRule.Properties.Any(o => o.Mapped);
    }

    #region Annotations

    /// <summary> If value is null, annotation is removed. If value is non-null, annotation is added or updated. </summary>
    public static T SetOrRemoveAnnotation<T>(this T rule, string name, object value) where T : RuleBase {
        rule[name] = value;
        return rule;
    }

    /// <summary> Sets the mapping strategy for the derived types. </summary>
    public static T UseTphMappingStrategy<T>(this T rule) where T : RuleBase => SetMappingStrategy(rule, "TPH");

    /// <summary> Sets the mapping strategy for the derived types. </summary>
    public static T UseTptMappingStrategy<T>(this T rule) where T : RuleBase => SetMappingStrategy(rule, "TPT");

    /// <summary> Sets the mapping strategy for the derived types. </summary>
    public static T UseTpcMappingStrategy<T>(this T rule) where T : RuleBase => SetMappingStrategy(rule, "TPC");

    /// <summary> Sets the mapping strategy for the derived types. </summary>
    public static T SetMappingStrategy<T>(this T rule, string strategy) where T : RuleBase
        => rule.SetOrRemoveAnnotation(RelationalAnnotationNames.MappingStrategy, strategy);

    /// <summary> Gets the mapping strategy for the derived types. </summary>
    public static string GetMappingStrategy<T>(this T rule) where T : RuleBase
        => rule.FindAnnotation(RelationalAnnotationNames.MappingStrategy) as string;

    /// <summary> Sets the discriminator column name. </summary>
    public static EntityRule SetDiscriminatorColumn(this EntityRule rule, string columnName)
        => rule.SetOrRemoveAnnotation(CoreAnnotationNames.DiscriminatorProperty, columnName);

    /// <summary> Gets the discriminator column name. </summary>
    public static string GetDiscriminatorColumn(this EntityRule rule)
        => rule.FindAnnotation(CoreAnnotationNames.DiscriminatorProperty) as string;

    /// <summary> Sets the type as abstract. </summary>
    public static EntityRule IsAbstract(this EntityRule rule, bool isAbstract)
        => rule.SetOrRemoveAnnotation(RulerAnnotations.Abstract, isAbstract ? true : null);

    /// <summary> Gets whether the type is abstract. </summary>
    public static bool IsAbstract(this EntityRule rule) => rule.FindAnnotation(RulerAnnotations.Abstract) is bool b && b;

    /// <summary> Sets the property discriminator value. </summary>
    public static PropertyRule SetDiscriminatorValue(this PropertyRule rule, string value, string toEntity)
        => rule.SetOrRemoveAnnotation(CoreAnnotationNames.DiscriminatorValue, value.HasNonWhiteSpace() ? $"{value}>{toEntity}" : null);

    /// <summary> Gets the property discriminator value. </summary>
    public static (string value, string toEntity) GetDiscriminatorValue(this PropertyRule rule, string value, string toEntity)
        => rule.FindAnnotation(CoreAnnotationNames.DiscriminatorValue) is string s ? ParseDiscriminatorValue(s) : default;

    private static (string value, string toEntity) ParseDiscriminatorValue(string s) {
        var parts = s.Split('>');
        if (parts.Length < 2) return default;
        if (parts.Length == 1) return (parts[0], parts[1]);
        return (parts.Take(parts.Length - 1).Join(">"), parts[^1]);
    }

    #endregion
}