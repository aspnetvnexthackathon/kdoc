using System;
using System.Linq;
using System.Xml.Linq;
using kdoc.Model;
using Microsoft.CodeAnalysis;

namespace kdoc
{
    public class DocModelBuilder : SymbolVisitor
    {
        private DocModel _model;
        private DocPackage _package;
        private DocAssembly _asm;
        private DocNamespace _ns;
        private DocType _typ;
        private DocMethod _met;

        public DocModelBuilder(DocModel model, DocPackage package)
        {
            _model = model;
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
            _model.Add(ns);

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
                kind);
            typ.MergeXml(TryLoadDocXml(symbol));
            _model.Add(typ);
            _ns.Types.Add(typ);

            // Visit members
            var oldTyp = _typ;
            _typ = typ;
            foreach (var member in symbol.GetMembers())
            {
                member.Accept(this);
            }
            _typ = oldTyp;

            // Group up overloaded methods
            var methodGroups = typ
                .Members
                .Where(m => m.MemberKind == DocMemberKind.Method)
                .Cast<DocMethod>()
                .GroupBy(m => m.Name);
            foreach (var methodGroup in methodGroups)
            {
                var overloads = methodGroup.ToList();
                if (overloads.Count > 1)
                {
                    var overloadId = overloads.First().DocId;
                    int parenStart = overloadId.IndexOf('(');
                    var setId = parenStart < 0 ?
                        overloadId.Substring(2) :
                        overloadId.Substring(2, parenStart - 2);
                    // Build a method set
                    var set = new DocMethodSet(
                        "Ms:" + setId,
                        methodGroup.Key);
                    foreach (var method in overloads)
                    {
                        set.Overloads.Add(method);
                        typ.Members.Remove(method);
                    }

                    _model.Add(set);
                    typ.Members.Add(set);
                }
            }
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

            var method = new DocMethod(symbol.GetDocumentationCommentId(), symbol.Name);
            method.MergeXml(TryLoadDocXml(symbol));
            _model.Add(method);
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
            var evt = new DocEvent(
                symbol.GetDocumentationCommentId(),
                symbol.Name,
                symbol.Type.GetDocumentationCommentId());
            evt.MergeXml(TryLoadDocXml(symbol));
            _typ.Members.Add(evt);
            _model.Add(evt);
        }

        public override void VisitField(IFieldSymbol symbol)
        {
            var field = new DocField(
                symbol.GetDocumentationCommentId(),
                symbol.Name,
                symbol.Type.GetDocumentationCommentId());
            field.MergeXml(TryLoadDocXml(symbol));
            _typ.Members.Add(field);
            _model.Add(field);
        }

        public override void VisitProperty(IPropertySymbol symbol)
        {
            var prop = new DocProperty(
                symbol.GetDocumentationCommentId(),
                symbol.Name,
                symbol.Type.GetDocumentationCommentId());
            prop.MergeXml(TryLoadDocXml(symbol));
            _typ.Members.Add(prop);
            _model.Add(prop);
        }

        public override void VisitParameter(IParameterSymbol symbol)
        {
            var docId = "Pa:" + symbol.ContainingSymbol.GetDocumentationCommentId().Substring(2) + "#" + symbol.Ordinal;
            var parm = new DocParameter(
                docId,
                symbol.Name,
                symbol.Type.GetDocumentationCommentId());
            parm.MergeXml(TryLoadDocXml(symbol));
            _met.Parameters.Add(parm);
            _model.Add(parm);
        }

        private XElement TryLoadDocXml(ISymbol symbol)
        {
            string xml = symbol.GetDocumentationCommentXml();
            return String.IsNullOrEmpty(xml) ?
                null :
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