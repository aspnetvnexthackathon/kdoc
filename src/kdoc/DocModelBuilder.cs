using System;
using System.Xml.Linq;
using kdoc.Model;
using Microsoft.CodeAnalysis;

namespace kdoc
{
    public class DocModelBuilder : SymbolVisitor
    {
        private DocPackage _package;
        private DocAssembly _asm;
        private DocNamespace _ns;
        private DocType _typ;
        private DocMethod _met;

        public DocModelBuilder(DocPackage package)
        {
            _package = package;
        }

        public override void VisitAssembly(IAssemblySymbol symbol)
        {
            var docId = "A:" + symbol.Name;
            var asm = new DocAssembly(docId, symbol.Name);

            _package.Assemblies.Add(asm);

            var oldAsm = _asm;
            _asm = asm;
            symbol.GlobalNamespace.Accept(this);
            _asm = oldAsm;
        }

        public override void VisitNamespace(INamespaceSymbol symbol)
        {
            var docId = "N:" + symbol.Name;
            var ns = new DocNamespace(docId, symbol.Name);

            _asm.Namespaces.Add(ns);

            // Visit types
            var oldNs = _ns;
            _ns = ns;
            foreach (var member in symbol.GetMembers())
            {
                member.Accept(this);
            }
            _ns = oldNs;
        }

        public override void VisitNamedType(INamedTypeSymbol symbol)
        {
            DocTypeKind kind;
            if (!TryMapTypeKind(symbol.TypeKind, out kind)) {
                return;
            }
            var typ = new DocType(
                symbol.GetDocumentationCommentId(),
                symbol.Name,
                kind)
            {
                DocXml = TryLoadDocXml(symbol)
            };
            _ns.Types.Add(typ);

            // Visit members
            var oldTyp = _typ;
            _typ = typ;
            foreach (var member in symbol.GetMembers())
            {
                member.Accept(this);
            }
            _typ = oldTyp;
        }

        public override void VisitMethod(IMethodSymbol symbol)
        {
            if (symbol.MethodKind != MethodKind.Constructor &&
                symbol.MethodKind != MethodKind.DeclareMethod &&
                symbol.MethodKind != MethodKind.Destructor &&
                symbol.MethodKind != MethodKind.SharedConstructor &&
                symbol.MethodKind != MethodKind.StaticConstructor)
            {
                return;
            }

            var method = new DocMethod(symbol.GetDocumentationCommentId(), symbol.Name)
            {
                DocXml = TryLoadDocXml(symbol)
            };
            _typ.Members.Add(method);

            var oldMet = _met;
            _met = method;
            foreach (var parameter in symbol.Parameters)
            {
                parameter.Accept(this);
            }
            _met = oldMet;
        }

        public override void VisitEvent(IEventSymbol symbol)
        {
            _typ.Members.Add(new DocEvent(
                symbol.GetDocumentationCommentId(),
                symbol.Name,
                symbol.Type.GetDocumentationCommentId())
            {
                DocXml = TryLoadDocXml(symbol)
            });
        }

        public override void VisitField(IFieldSymbol symbol)
        {
            _typ.Members.Add(new DocField(
                symbol.GetDocumentationCommentId(),
                symbol.Name,
                symbol.Type.GetDocumentationCommentId())
            {
                DocXml = TryLoadDocXml(symbol)
            });
        }

        public override void VisitProperty(IPropertySymbol symbol)
        {
            _typ.Members.Add(new DocProperty(
                symbol.GetDocumentationCommentId(),
                symbol.Name,
                symbol.Type.GetDocumentationCommentId())
            {
                DocXml = TryLoadDocXml(symbol)
            });
        }

        public override void VisitParameter(IParameterSymbol symbol)
        {
            var docId = "Pa:" + symbol.ContainingSymbol.GetDocumentationCommentId().Substring(2) + "#" + symbol.Ordinal;
            _met.Parameters.Add(new DocParameter(
                docId,
                symbol.Name,
                symbol.Type.GetDocumentationCommentId())
            {
                DocXml = TryLoadDocXml(symbol)
            });
        }

        private XElement TryLoadDocXml(ISymbol symbol)
        {
            string xml = symbol.GetDocumentationCommentXml();
            return String.IsNullOrEmpty(xml) ?
                (XElement)null :
                XElement.Parse(xml);
        }

        private bool TryMapTypeKind(TypeKind typeKind, out DocTypeKind kind)
        {
            switch (typeKind)
            {
                case TypeKind.Class:
                    kind = DocTypeKind.Class;
                    break;
                case TypeKind.Delegate:
                    kind = DocTypeKind.Delegate;
                    break;
                case TypeKind.Enum:
                    kind = DocTypeKind.Enum;
                    break;
                case TypeKind.Interface:
                    kind = DocTypeKind.Interface;
                    break;
                case TypeKind.Struct:
                    kind = DocTypeKind.Struct;
                    break;
                default:
                    kind = DocTypeKind.Class;
                    return false;
            }
            return true;
        }
    }
}