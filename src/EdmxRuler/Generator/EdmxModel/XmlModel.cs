﻿/*
 Autogenerated by http://xmltocsharp.azurewebsites.net/ and modified by Mark Randell

 Licensed under the Apache License, Version 2.0
 http://www.apache.org/licenses/LICENSE-2.0
 */

using System.Xml.Schema;
using System.Xml.Serialization;

namespace EdmxRuler.Generator.EdmxModel;

#pragma warning disable CA2227
#pragma warning disable SA1402
#pragma warning disable SA1124
#pragma warning disable CA1002
#pragma warning disable SA1649

public interface IConceptualProperty {
    string Name { get; set; }
}

#region storage elements

[XmlRoot(ElementName = "EntityType", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
public class StorageEntityType {
    [XmlElement(ElementName = "Key", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
    public StorageEntityKey Key { get; set; }

    [XmlElement(ElementName = "Property", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
    public List<StorageProperty> Properties { get; set; }

    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }
}

[XmlRoot(ElementName = "Key", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
public class StorageEntityKey {
    [XmlElement(ElementName = "PropertyRef", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
    public List<StoragePropertyRef> PropertyRefs { get; set; }
}

[XmlRoot(ElementName = "Property", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
public class StorageProperty {
    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "Type")]
    public string Type { get; set; }

    [XmlAttribute(AttributeName = "StoreGeneratedPattern")]
    public string StoreGeneratedPattern { get; set; }

    [XmlAttribute(AttributeName = "Nullable")]
    public string Nullable { get; set; }

    [XmlAttribute(AttributeName = "MaxLength")]
    public string MaxLength { get; set; }

    [XmlAttribute(AttributeName = "Precision")]
    public string Precision { get; set; }

    [XmlAttribute(AttributeName = "Scale")]
    public string Scale { get; set; }
}

[XmlRoot(ElementName = "End", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
public class StorageEnd {
    [XmlAttribute(AttributeName = "Role")]
    public string Role { get; set; }

    [XmlAttribute(AttributeName = "Type")]
    public string Type { get; set; }

    [XmlAttribute(AttributeName = "Multiplicity")]
    public string Multiplicity { get; set; }

    [XmlElement(ElementName = "OnDelete", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
    public List<StorageOnDelete> OnDelete { get; set; }

    [XmlAttribute(AttributeName = "EntitySet")]
    public string EntitySet { get; set; }
}

[XmlRoot(ElementName = "Principal", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
public class StoragePrincipal {
    [XmlElement(ElementName = "PropertyRef", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
    public List<StoragePropertyRef> PropertyRefs { get; set; }

    [XmlAttribute(AttributeName = "Role")]
    public string Role { get; set; }
}

[XmlRoot(ElementName = "Dependent", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
public class StorageDependent {
    [XmlElement(ElementName = "PropertyRef", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
    public List<StoragePropertyRef> PropertyRefs { get; set; }

    [XmlAttribute(AttributeName = "Role")]
    public string Role { get; set; }
}

[XmlRoot(ElementName = "ReferentialConstraint", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
public class StorageReferentialConstraint {
    [XmlElement(ElementName = "Principal", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
    public StoragePrincipal Principal { get; set; }

    [XmlElement(ElementName = "Dependent", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
    public StorageDependent Dependent { get; set; }
}

[XmlRoot(ElementName = "Association", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
public class StorageAssociation {
    [XmlElement(ElementName = "End", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
    public List<StorageEnd> Ends { get; set; }

    [XmlElement(ElementName = "ReferentialConstraint", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
    public StorageReferentialConstraint ReferentialConstraint { get; set; }

    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }
}

[XmlRoot(ElementName = "OnDelete", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
public class StorageOnDelete {
    [XmlAttribute(AttributeName = "Action")]
    public string Action { get; set; }
}

[XmlRoot(ElementName = "Parameter", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
public class StorageParameter {
    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "Type")]
    public string Type { get; set; }

    [XmlAttribute(AttributeName = "Mode")]
    public string Mode { get; set; }
}

[XmlRoot(ElementName = "PropertyRef", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
public class StoragePropertyRef {
    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }
}

[XmlRoot(ElementName = "Function", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
public class StorageFunction {
    [XmlElement(ElementName = "Parameter", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
    public List<StorageParameter> Parameter { get; set; }

    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "Aggregate")]
    public string Aggregate { get; set; }

    [XmlAttribute(AttributeName = "BuiltIn")]
    public string BuiltIn { get; set; }

    [XmlAttribute(AttributeName = "NiladicFunction")]
    public string NiladicFunction { get; set; }

    [XmlAttribute(AttributeName = "IsComposable")]
    public string IsComposable { get; set; }

    [XmlAttribute(AttributeName = "ParameterTypeSemantics")]
    public string ParameterTypeSemantics { get; set; }

    [XmlAttribute(AttributeName = "Schema")]
    public string Schema { get; set; }
}

[XmlRoot(ElementName = "EntitySet", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
public class StorageEntitySet {
    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "EntityType")]
    public string EntityType { get; set; }

    [XmlAttribute]
    public string Schema { get; set; }

    [XmlAttribute(
        "Schema",
        Form = XmlSchemaForm.Qualified,
        Namespace = "http://schemas.microsoft.com/ado/2007/12/edm/EntityStoreSchemaGenerator")]
    public string Schema1 { get; set; }

    [XmlAttribute(
        AttributeName = "Type",
        Namespace = "http://schemas.microsoft.com/ado/2007/12/edm/EntityStoreSchemaGenerator")]
    public string Type { get; set; }

    [XmlElement(ElementName = "DefiningQuery", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
    public string DefiningQuery { get; set; }
}

[XmlRoot(ElementName = "AssociationSet", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
public class StorageAssociationSet {
    [XmlElement(ElementName = "End", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
    public List<StorageEnd> End { get; set; }

    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "Association")]
    public string Association { get; set; }
}

[XmlRoot(ElementName = "EntityContainer", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
public class StorageEntityContainer {
    [XmlElement(ElementName = "EntitySet", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
    public List<StorageEntitySet> EntitySets { get; set; }

    [XmlElement(ElementName = "AssociationSet", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
    public List<StorageAssociationSet> AssociationSets { get; set; }

    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }
}

[XmlRoot(ElementName = "Schema", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
public class StorageSchema {
    [XmlElement(ElementName = "EntityType", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
    public List<StorageEntityType> EntityTypes { get; set; }

    [XmlElement(ElementName = "Association", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
    public List<StorageAssociation> Associations { get; set; }

    [XmlElement(ElementName = "Function", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
    public List<StorageFunction> Functions { get; set; }

    [XmlElement(ElementName = "EntityContainer", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
    public List<StorageEntityContainer> EntityContainers { get; set; }

    [XmlAttribute(AttributeName = "Namespace")]
    public string Namespace { get; set; }

    [XmlAttribute(AttributeName = "Provider")]
    public string Provider { get; set; }

    [XmlAttribute(AttributeName = "ProviderManifestToken")]
    public string ProviderManifestToken { get; set; }

    [XmlAttribute(AttributeName = "Alias")]
    public string Alias { get; set; }

    [XmlAttribute(AttributeName = "store", Namespace = "http://www.w3.org/2000/xmlns/")]
    public string Store { get; set; }

    [XmlAttribute(AttributeName = "customannotation", Namespace = "http://www.w3.org/2000/xmlns/")]
    public string Customannotation { get; set; }

    [XmlAttribute(AttributeName = "xmlns")]
    public string Xmlns { get; set; }
}

[XmlRoot(ElementName = "StorageModels", Namespace = "http://schemas.microsoft.com/ado/2009/11/edmx")]
public class StorageModels {
    [XmlElement(ElementName = "Schema", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")]
    public StorageSchema Schema { get; set; }
}

#endregion

#region conceptual elements

[XmlRoot(ElementName = "EntitySet", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
public class ConceptualEntitySet {
    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "EntityType")]
    public string EntityType { get; set; }
}

[XmlRoot(ElementName = "End", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
public class ConceptualEnd {
    [XmlAttribute(AttributeName = "Role")]
    public string Role { get; set; }

    [XmlAttribute(AttributeName = "EntitySet")]
    public string EntitySet { get; set; }

    [XmlAttribute(AttributeName = "Type")]
    public string Type { get; set; }

    [XmlAttribute(AttributeName = "Multiplicity")]
    public string Multiplicity { get; set; }

    [XmlElement(ElementName = "OnDelete", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public List<ConceptualOnDelete> OnDelete { get; set; }
}

[XmlRoot(ElementName = "AssociationSet", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
public class ConceptualAssociationSet {
    [XmlElement(ElementName = "End", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public List<ConceptualEnd> End2 { get; set; }

    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "Association")]
    public string Association { get; set; }
}

[XmlRoot(ElementName = "Parameter", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
public class ConceptualParameter {
    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "Mode")]
    public string Mode { get; set; }

    [XmlAttribute(AttributeName = "Type")]
    public string Type { get; set; }
}

[XmlRoot(ElementName = "FunctionImport", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
public class ConceptualFunctionImport {
    [XmlElement(ElementName = "Parameter", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public List<ConceptualParameter> Parameters { get; set; }

    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "ReturnType")]
    public string ReturnType { get; set; }
}

[XmlRoot(ElementName = "EntityContainer", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
public class ConceptualEntityContainer {
    [XmlElement(ElementName = "EntitySet", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public List<ConceptualEntitySet> EntitySets { get; set; }

    [XmlElement(ElementName = "AssociationSet", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public List<ConceptualAssociationSet> AssociationSets { get; set; }

    [XmlElement(ElementName = "FunctionImport", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public List<ConceptualFunctionImport> FunctionImport { get; set; }

    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }

    [XmlAttribute(
        AttributeName = "LazyLoadingEnabled",
        Namespace = "http://schemas.microsoft.com/ado/2009/02/edm/annotation")]
    public string LazyLoadingEnabled { get; set; }
}

[XmlRoot(ElementName = "Property", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
public class ConceptualProperty : IConceptualProperty {
    [XmlAttribute(AttributeName = "Type")]
    public string Type { get; set; }

    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "Nullable")]
    public string Nullable { get; set; }

    [XmlAttribute(AttributeName = "Precision")]
    public string Precision { get; set; }

    [XmlAttribute(AttributeName = "MaxLength")]
    public string MaxLength { get; set; }

    [XmlAttribute(AttributeName = "Scale")]
    public string Scale { get; set; }

    [XmlAttribute(
        AttributeName = "StoreGeneratedPattern",
        Namespace = "http://schemas.microsoft.com/ado/2009/02/edm/annotation")]
    public string StoreGeneratedPattern { get; set; }

    [XmlAttribute(AttributeName = "FixedLength")]
    public string FixedLength { get; set; }

    [XmlAttribute(AttributeName = "Unicode")]
    public string Unicode { get; set; }

    [XmlAttribute(AttributeName = "ConcurrencyMode")]
    public string ConcurrencyMode { get; set; }

    [XmlAttribute(AttributeName = "xmlns")]
    public string Xmlns { get; set; }

    [XmlAttribute(AttributeName = "annotation", Namespace = "http://www.w3.org/2000/xmlns/")]
    public string Annotation { get; set; }

    [XmlElement(ElementName = "Documentation", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public ConceptualDocumentation Documentation { get; set; }
}

[XmlRoot(ElementName = "ComplexType", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
public class ComplexType {
    [XmlElement(ElementName = "Property", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public List<ConceptualProperty> Property2 { get; set; }

    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }
}

[XmlRoot(ElementName = "Member", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
public class ConceptualEnumMember {
    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "Value")]
    public string Value { get; set; }
}

[XmlRoot(ElementName = "EnumType", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
public class ConceptualEnumType {
    [XmlElement(ElementName = "Member", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public List<ConceptualEnumMember> Members { get; set; }

    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }

    [XmlAttribute(
        AttributeName = "ExternalTypeName",
        Namespace = "http://schemas.microsoft.com/ado/2006/04/codegeneration")]
    public string ExternalTypeName { get; set; }

    [XmlAttribute(AttributeName = "a", Namespace = "http://www.w3.org/2000/xmlns/")]
    public string A { get; set; }

    [XmlAttribute(AttributeName = "UnderlyingType")]
    public string UnderlyingType { get; set; }

    [XmlAttribute(AttributeName = "IsFlags")]
    public string IsFlags { get; set; }
}

[XmlRoot(ElementName = "PropertyRef", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
public class ConceptualPropertyRef {
    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }
}

[XmlRoot(ElementName = "Key", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
public class ConceptualEntityKey {
    [XmlElement(ElementName = "PropertyRef", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public List<ConceptualPropertyRef> PropertyRefs { get; set; }
}

[XmlRoot(ElementName = "NavigationProperty", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
public class ConceptualNavigationProperty : IConceptualProperty {
    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "Relationship")]
    public string Relationship { get; set; }

    [XmlAttribute(AttributeName = "FromRole")]
    public string FromRole { get; set; }

    [XmlAttribute(AttributeName = "ToRole")]
    public string ToRole { get; set; }
}

[XmlRoot(ElementName = "EntityType", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
public class ConceptualEntityType {
    [XmlElement(ElementName = "Key", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public ConceptualEntityKey Key { get; set; }

    [XmlElement(ElementName = "Property", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public List<ConceptualProperty> Properties { get; set; }

    [XmlElement(ElementName = "NavigationProperty", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public List<ConceptualNavigationProperty> NavigationProperties { get; set; }

    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }
}

[XmlRoot(ElementName = "Documentation", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
public class ConceptualDocumentation {
    [XmlElement(ElementName = "Summary", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public string Summary { get; set; }
}

[XmlRoot(ElementName = "Principal", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
public class ConceptualPrincipal {
    [XmlElement(ElementName = "PropertyRef", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public List<ConceptualPropertyRef> PropertyRefs { get; set; }

    [XmlAttribute(AttributeName = "Role")]
    public string Role { get; set; }
}

[XmlRoot(ElementName = "Dependent", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
public class ConceptualDependent {
    [XmlElement(ElementName = "PropertyRef", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public List<ConceptualPropertyRef> PropertyRefs { get; set; }

    [XmlAttribute(AttributeName = "Role")]
    public string Role { get; set; }
}

[XmlRoot(ElementName = "ReferentialConstraint", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
public class ConceptualReferentialConstraint {
    [XmlElement(ElementName = "Principal", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public ConceptualPrincipal Principal { get; set; }

    [XmlElement(ElementName = "Dependent", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public ConceptualDependent Dependent { get; set; }
}

[XmlRoot(ElementName = "Association", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
public class ConceptualAssociation {
    [XmlElement(ElementName = "End", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public List<ConceptualEnd> Ends { get; set; }

    [XmlElement(ElementName = "ReferentialConstraint", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public ConceptualReferentialConstraint ReferentialConstraint { get; set; }

    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }
}

[XmlRoot(ElementName = "OnDelete", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
public class ConceptualOnDelete {
    [XmlAttribute(AttributeName = "Action")]
    public string Action { get; set; }
}

[XmlRoot(ElementName = "Schema", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
public class ConceptualSchema {
    [XmlElement(ElementName = "EntityContainer", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public ConceptualEntityContainer EntityContainers { get; set; }

    [XmlElement(ElementName = "ComplexType", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public List<ComplexType> ComplexTypes { get; set; }

    [XmlElement(ElementName = "EnumType", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public List<ConceptualEnumType> EnumTypes { get; set; }

    [XmlElement(ElementName = "EntityType", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public List<ConceptualEntityType> EntityTypes { get; set; }

    [XmlElement(ElementName = "Association", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public List<ConceptualAssociation> Associations { get; set; }

    [XmlAttribute(AttributeName = "Namespace")]
    public string Namespace { get; set; }

    [XmlAttribute(AttributeName = "Alias")]
    public string Alias { get; set; }

    [XmlAttribute(
        AttributeName = "UseStrongSpatialTypes",
        Namespace = "http://schemas.microsoft.com/ado/2009/02/edm/annotation")]
    public string UseStrongSpatialTypes { get; set; }

    [XmlAttribute(AttributeName = "xmlns")]
    public string Xmlns { get; set; }

    [XmlAttribute(AttributeName = "annotation", Namespace = "http://www.w3.org/2000/xmlns/")]
    public string Annotation { get; set; }
}

[XmlRoot(ElementName = "ConceptualModels", Namespace = "http://schemas.microsoft.com/ado/2009/11/edmx")]
public class ConceptualModels {
    [XmlElement(ElementName = "Schema", Namespace = "http://schemas.microsoft.com/ado/2009/11/edm")]
    public ConceptualSchema Schema { get; set; }
}

#endregion

#region mapping elements

[XmlRoot(ElementName = "ScalarProperty", Namespace = "http://schemas.microsoft.com/ado/2009/11/mapping/cs")]
public class ScalarPropertyMapping {
    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "ColumnName")]
    public string ColumnName { get; set; }
}

[XmlRoot(ElementName = "MappingFragment", Namespace = "http://schemas.microsoft.com/ado/2009/11/mapping/cs")]
public class MappingFragment {
    [XmlElement(ElementName = "ScalarProperty", Namespace = "http://schemas.microsoft.com/ado/2009/11/mapping/cs")]
    public List<ScalarPropertyMapping> ScalarProperties { get; set; }

    [XmlAttribute(AttributeName = "StoreEntitySet")]
    public string StoreEntitySet { get; set; }
}

[XmlRoot(ElementName = "EntityTypeMapping", Namespace = "http://schemas.microsoft.com/ado/2009/11/mapping/cs")]
public class EntityTypeMapping {
    [XmlElement(ElementName = "MappingFragment", Namespace = "http://schemas.microsoft.com/ado/2009/11/mapping/cs")]
    public List<MappingFragment> MappingFragments { get; set; }

    [XmlAttribute(AttributeName = "TypeName")]
    public string TypeName { get; set; }
}

[XmlRoot(ElementName = "EntitySetMapping", Namespace = "http://schemas.microsoft.com/ado/2009/11/mapping/cs")]
public class EntitySetMapping {
    [XmlElement(ElementName = "EntityTypeMapping", Namespace = "http://schemas.microsoft.com/ado/2009/11/mapping/cs")]
    public List<EntityTypeMapping> EntityTypeMappings { get; set; }

    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }
}

[XmlRoot(ElementName = "ComplexTypeMapping", Namespace = "http://schemas.microsoft.com/ado/2009/11/mapping/cs")]
public class ComplexTypeMapping {
    [XmlElement(ElementName = "ScalarProperty", Namespace = "http://schemas.microsoft.com/ado/2009/11/mapping/cs")]
    public List<ScalarPropertyMapping> ScalarProperty { get; set; }

    [XmlAttribute(AttributeName = "TypeName")]
    public string TypeName { get; set; }
}

[XmlRoot(ElementName = "ResultMapping", Namespace = "http://schemas.microsoft.com/ado/2009/11/mapping/cs")]
public class ResultMapping {
    [XmlElement(ElementName = "ComplexTypeMapping", Namespace = "http://schemas.microsoft.com/ado/2009/11/mapping/cs")]
    public ComplexTypeMapping ComplexTypeMapping { get; set; }
}

[XmlRoot(ElementName = "FunctionImportMapping", Namespace = "http://schemas.microsoft.com/ado/2009/11/mapping/cs")]
public class FunctionImportMapping {
    [XmlElement(ElementName = "ResultMapping", Namespace = "http://schemas.microsoft.com/ado/2009/11/mapping/cs")]
    public ResultMapping ResultMapping { get; set; }

    [XmlAttribute(AttributeName = "FunctionImportName")]
    public string FunctionImportName { get; set; }

    [XmlAttribute(AttributeName = "FunctionName")]
    public string FunctionName { get; set; }
}

[XmlRoot(ElementName = "EntityContainerMapping", Namespace = "http://schemas.microsoft.com/ado/2009/11/mapping/cs")]
public class EntityContainerMapping {
    [XmlElement(ElementName = "EntitySetMapping", Namespace = "http://schemas.microsoft.com/ado/2009/11/mapping/cs")]
    public List<EntitySetMapping> EntitySetMappings { get; set; }

    [XmlElement(
        ElementName = "FunctionImportMapping",
        Namespace = "http://schemas.microsoft.com/ado/2009/11/mapping/cs")]
    public List<FunctionImportMapping> FunctionImportMappings { get; set; }

    [XmlAttribute(AttributeName = "StorageEntityContainer")]
    public string StorageEntityContainer { get; set; }

    [XmlAttribute(AttributeName = "CdmEntityContainer")]
    public string CdmEntityContainer { get; set; }
}

[XmlRoot(ElementName = "Mapping", Namespace = "http://schemas.microsoft.com/ado/2009/11/mapping/cs")]
public class Mapping {
    [XmlElement(
        ElementName = "EntityContainerMapping",
        Namespace = "http://schemas.microsoft.com/ado/2009/11/mapping/cs")]
    public EntityContainerMapping EntityContainerMapping { get; set; }

    [XmlAttribute(AttributeName = "Space")]
    public string Space { get; set; }

    [XmlAttribute(AttributeName = "xmlns")]
    public string Xmlns { get; set; }
}

[XmlRoot(ElementName = "Mappings", Namespace = "http://schemas.microsoft.com/ado/2009/11/edmx")]
public class Mappings {
    [XmlElement(ElementName = "Mapping", Namespace = "http://schemas.microsoft.com/ado/2009/11/mapping/cs")]
    public Mapping Mapping { get; set; }
}

#endregion

#region designer elements (commented out as not needed yet)

/*
[XmlRoot(ElementName = "DesignerProperty", Namespace = "http://schemas.microsoft.com/ado/2009/11/edmx")]
public class DesignerProperty {
    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }
    [XmlAttribute(AttributeName = "Value")]
    public string Value { get; set; }
}

[XmlRoot(ElementName = "DesignerInfoPropertySet", Namespace = "http://schemas.microsoft.com/ado/2009/11/edmx")]
public class DesignerInfoPropertySet {
    [XmlElement(ElementName = "DesignerProperty", Namespace = "http://schemas.microsoft.com/ado/2009/11/edmx")]
    public List<DesignerProperty> DesignerProperty { get; set; }
}

[XmlRoot(ElementName = "Connection", Namespace = "http://schemas.microsoft.com/ado/2009/11/edmx")]
public class Connection {
    [XmlElement(ElementName = "DesignerInfoPropertySet", Namespace = "http://schemas.microsoft.com/ado/2009/11/edmx")]
    public DesignerInfoPropertySet DesignerInfoPropertySet { get; set; }
}

[XmlRoot(ElementName = "Options", Namespace = "http://schemas.microsoft.com/ado/2009/11/edmx")]
public class Options {
    [XmlElement(ElementName = "DesignerInfoPropertySet", Namespace = "http://schemas.microsoft.com/ado/2009/11/edmx")]
    public DesignerInfoPropertySet DesignerInfoPropertySet { get; set; }
}

[XmlRoot(ElementName = "Designer", Namespace = "http://schemas.microsoft.com/ado/2009/11/edmx")]
public class Designer {
    [XmlElement(ElementName = "Connection", Namespace = "http://schemas.microsoft.com/ado/2009/11/edmx")]
    public Connection Connection { get; set; }
    [XmlElement(ElementName = "Options", Namespace = "http://schemas.microsoft.com/ado/2009/11/edmx")]
    public Options Options { get; set; }
    [XmlElement(ElementName = "Diagrams", Namespace = "http://schemas.microsoft.com/ado/2009/11/edmx")]
    public string Diagrams { get; set; }
}
*/

#endregion

[XmlRoot(ElementName = "Runtime", Namespace = "http://schemas.microsoft.com/ado/2009/11/edmx")]
public class Runtime {
    [XmlElement(ElementName = "StorageModels", Namespace = "http://schemas.microsoft.com/ado/2009/11/edmx")]
    public StorageModels StorageModels { get; set; }

    [XmlElement(ElementName = "ConceptualModels", Namespace = "http://schemas.microsoft.com/ado/2009/11/edmx")]
    public ConceptualModels ConceptualModels { get; set; }

    [XmlElement(ElementName = "Mappings", Namespace = "http://schemas.microsoft.com/ado/2009/11/edmx")]
    public Mappings Mappings { get; set; }
}

[XmlRoot(ElementName = "Edmx", Namespace = "http://schemas.microsoft.com/ado/2009/11/edmx")]
public class EdmxRoot {
    [XmlElement(ElementName = "Runtime", Namespace = "http://schemas.microsoft.com/ado/2009/11/edmx")]
    public Runtime Runtime { get; set; }

    [XmlAttribute(AttributeName = "Version")]
    public string Version { get; set; }

    [XmlAttribute(AttributeName = "edmx", Namespace = "http://www.w3.org/2000/xmlns/")]
    public string Edmx { get; set; }

    #region designer property (commented out as not needed yet)

    /*
    [XmlElement(ElementName = "Designer", Namespace = "http://schemas.microsoft.com/ado/2009/11/edmx")]
    public Designer Designer { get; set; }
     */

    #endregion
}
#pragma warning restore SA1402
#pragma warning restore SA1124
#pragma warning restore CA2227
#pragma warning restore CA1002
#pragma warning restore SA1649