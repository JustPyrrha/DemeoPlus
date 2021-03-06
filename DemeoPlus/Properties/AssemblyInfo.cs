using System.Reflection;
using System.Runtime.InteropServices;
using DemeoPlus;
using MelonLoader;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("DemeoPlus")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("DemeoPlus")]
[assembly: AssemblyCopyright("Copyright ©  2021")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Melon
[assembly: MelonInfo(typeof(DemeoPlusMelon), "DemeoPlus", "0.4.0", "JoeZwet")]
[assembly: MelonGame("Resolution Games", "Demeo")]
[assembly: MelonPlatform(MelonPlatformAttribute.CompatiblePlatforms.UNIVERSAL)]
[assembly: MelonGameVersion("1.4.118643")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("20B9132B-EC86-44DE-8A36-939FDD61D852")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]