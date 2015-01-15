using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using NUnit.Framework;
using System.Linq;

namespace XmlSchemaImporterTest
{
    [TestFixture]
    public class XsdToClassTests
    {
        // Test for XmlSchemaImporter
        [Test]
        public void XsdToClassTest()
        {
            // identify the path to the xsd
            //string xsdFileName = "mailxml_base.xsd";
            string xsdFileName = "dvd.xsd";
            string path = "C:\\Users\\luke\\Documents\\visual studio 2013\\Projects\\XsdToPoco\\XsdToCode.Tests";
            string xsdPath = Path.Combine(path, xsdFileName);

            // load the xsd
            XmlSchema xsd;
            using (FileStream stream = new FileStream(xsdPath, FileMode.Open, FileAccess.Read))
            {
                xsd = XmlSchema.Read(stream, null);
            }
            Console.WriteLine("xsd.IsCompiled {0}", xsd.IsCompiled);

            XmlSchemas xsds = new XmlSchemas();
            xsds.Add(xsd);
            xsds.Compile(null, true);
            XmlSchemaImporter schemaImporter = new XmlSchemaImporter(xsds);

            // create the codedom
            CodeNamespace codeNamespace = new CodeNamespace("Generated");
            XmlCodeExporter codeExporter = new XmlCodeExporter(codeNamespace);

            var maps = new List<XmlTypeMapping>();
            foreach (XmlSchemaType schemaType in xsd.SchemaTypes.Values)
            {
                maps.Add(schemaImporter.ImportSchemaType(schemaType.QualifiedName));
            }
            foreach (XmlSchemaElement schemaElement in xsd.Elements.Values)
            {
                maps.Add(schemaImporter.ImportTypeMapping(schemaElement.QualifiedName));
            }
            foreach (XmlTypeMapping map in maps)
            {
                codeExporter.ExportTypeMapping(map);
            }

            SetupAutoProps(codeNamespace);
            RemoveAttributes(codeNamespace);

            // Check for invalid characters in identifiers
            CodeGenerator.ValidateIdentifiers(codeNamespace);

            // output the C# code
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();

            CodeCompileUnit cu = new CodeCompileUnit();
            var globalNamespace = new CodeNamespace();
            globalNamespace.Imports.Add(new CodeNamespaceImport("System"));
            
            cu.Namespaces.Add(globalNamespace);
            cu.Namespaces.Add(codeNamespace);




            string output;
            using (StringWriter writer = new StringWriter())
            {
                codeProvider.GenerateCodeFromCompileUnit(cu, writer, new CodeGeneratorOptions());
                output = writer.GetStringBuilder().ToString();
                Console.WriteLine(output);
            }

            using (StreamWriter outfile = new StreamWriter("C:\\Users\\luke\\Documents\\visual studio 2013\\Projects\\XsdToPoco\\XsdToCode.Tests\\test.cs"))
            {
                outfile.Write(output);
            }
        }

        // Remove all the attributes from each type in the CodeNamespace, except
        // System.Xml.Serialization.XmlTypeAttribute
        private void RemoveAttributes(CodeNamespace codeNamespace)
        {
            foreach (CodeTypeDeclaration codeType in codeNamespace.Types)
            {
                codeType.CustomAttributes.Clear();

                //CodeAttributeDeclaration xmlTypeAttribute = null;
                //foreach (CodeAttributeDeclaration codeAttribute in codeType.CustomAttributes)
                //{
                //    Console.WriteLine(codeAttribute.Name);
                //    if (codeAttribute.Name == "System.Xml.Serialization.XmlTypeAttribute")
                //    {
                //        xmlTypeAttribute = codeAttribute;
                //    }
                //}
                //codeType.CustomAttributes.Clear();
                //if (xmlTypeAttribute != null)
                //{
                //    codeType.CustomAttributes.Add(xmlTypeAttribute);
                //}
            }
        }

        private void SetupAutoProps(CodeNamespace codeNamespace)
        {
            var names = new List<string>();
            var membersToKeep = new CodeTypeMemberCollection();
            var membersAsAutos = new CodeTypeMemberCollection();
            foreach (CodeTypeDeclaration type in codeNamespace.Types)
            {
                foreach (CodeTypeMember member in type.Members)
                {
                    names.Add(member.Name);
                    // get rid of privates
                    if ((member.Attributes & MemberAttributes.Public) == MemberAttributes.Public && member.Name != ".ctor")
                    {
                        membersToKeep.Add(member);

                        string typestring = "";
                        if (member is CodeMemberProperty)
                        {
                            var prop = member as CodeMemberProperty;
                            typestring = prop.Type.BaseType.Replace("System.", "");
                        }

                        CodeSnippetTypeMember snippet = new CodeSnippetTypeMember();
                        snippet.Text = String.Format("\t\tpublic {1} {0} {{ get; set; }}", member.Name, typestring);
                        membersAsAutos.Add(snippet);
                    }

                    //membersToKeep.Add(member);

                }


                

                type.Members.Clear();
                //type.Members.AddRange(membersToKeep);
                type.Members.AddRange(membersAsAutos);
            }

        }
    }
}